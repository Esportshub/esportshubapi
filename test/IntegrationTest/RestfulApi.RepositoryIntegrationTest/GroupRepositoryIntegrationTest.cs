using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Groups;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class GroupRepositoryIntegrationTest
    {
        public static List<Group> GetGroups(int amount)
        {
            var groups = new List<Group>();
            for (var i = 0; i < amount; i++)
                groups.Add(Group.Builder().SetGroupGuid(Guid.NewGuid()).Build());
            return groups;
        }

        public static List<Group> GetGroupsWithEsportshubEvents(int amountOfGroups, int amountOfGroupsWithoutEsportshubEvents, List<EsportshubEvent> esportshubEvents)
        {
            var groups = new List<Group>();
            for (var i = 0; i < amountOfGroups; i++)
                groups.Add(Group.Builder().SetGroupGuid(Guid.NewGuid()).SetEsportshubEvents(esportshubEvents.ToList()).Build());
            for (var i = 0; i < amountOfGroupsWithoutEsportshubEvents; i++)
                groups.Add(Group.Builder().SetGroupGuid(Guid.NewGuid()).Build());
            return groups;
        }

        public static List<EsportshubEvent> GetEsportshubEvents(int amount)
        {
            var esportshubEvents = new List<EsportshubEvent>();
            for (var i = 0; i < amount; i++)
                esportshubEvents.Add(EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build());
            return esportshubEvents;
        }

        public class Insert
        {
            [Fact]
            public void Add7GroupsToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_groups_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = GetGroups(7);
                    groups.ForEach(groupRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = groupRepository.FindBy(null, string.Empty);
                    Equal(7, groups.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfGroupsToDatabase_ExpectsGroupsCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_groups_to_database").Options;
                var groups = GetGroups(3);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(null, string.Empty);
                    var count = foundGroups.Count();
                    NotEqual(groups.Count-1, count);
                    NotEqual(groups.Count+1, count);
                }
            }

            [Fact]
            public void AddGroupToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_group_to_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Insert(group);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroup = groupRepository.Find(group.GroupGuid);
                    NotNull(foundGroup);
                    Equal(group.GroupGuid, foundGroup.GroupGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<ArgumentNullException>(() => groupRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdateGroupInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_group_and_update_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Insert(group);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroup = groupRepository.Find(group.GroupGuid);
                    Equal(foundGroup.Name, "Sjuften");
                    foundGroup.Name = "DenLilleMand";
                    groupRepository.Update(foundGroup);
                    groupRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroup = groupRepository.Find(group.GroupGuid);
                    Equal(foundGroup.Name, "DenLilleMand");
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_group_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<ArgumentNullException>(() => groupRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentGroup_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_group_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Update(group);
                    Throws<DbUpdateConcurrencyException>(() => groupRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentGroup_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_group_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<InvalidOperationException>(() => groupRepository.Delete(group.GroupGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_group_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<ArgumentException>(() => groupRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeleteGroupInDatabase_ExpectsGroupNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Insert(group);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Delete(group.GroupGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<InvalidOperationException>(() => groupRepository.Find(group.GroupGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindGroupInDatabase_ExpectsToFindCorrectGroup()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Insert(group);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroup = groupRepository.Find(group.GroupGuid);
                    Equal(foundGroup.Guid, group.Guid);
                }
            }

            [Fact]
            public void FindGroupWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_group_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<ArgumentException>(() => groupRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentGroupInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    Throws<InvalidOperationException>(() => groupRepository.Find(group.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncGroupInDatabase_ExpectsToFindCorrectGroup()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groupRepository.Insert(group);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroup = await groupRepository.FindAsync(group.GroupGuid);
                    Equal(foundGroup.Guid, group.Guid);
                }
            }

            [Fact]
            public async void FindAsyncGroupWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_group_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await groupRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentGroupInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await groupRepository.FindAsync(group.Guid));
                }
            }
        }

        public class FindBy
        {
            [Fact]
            public void FindGroupsByFilterInDatabase_ExpectsToFindCorrectGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_groups_to_database_and_use_findby").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var groups = GetGroupsWithEsportshubEvents(amountOfGroups: 7, amountOfGroupsWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(group => group.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)), string.Empty);
                    Equal(expected: groups.Count, actual: foundGroups.Count());
                }
            }

            [Fact]
            public void FindByGroupWithEmptyGuidInDatabase_ExpectsFindByReturnsNoGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_group_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = GetGroups(5);
                    groups.ForEach(groupRepository.Insert);
                    groupRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = groupRepository.FindBy(group => group.Guid == Guid.Empty, string.Empty);
                    Equal(0, groups.Count());
                }
            }

            [Fact]
            public void FindNonExistentGroupsByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = groupRepository.FindBy(x => x.GroupGuid == group.Guid, string.Empty);
                    Equal(0, groups.Count());
                }
            }
        }

        public class FindByAsync
        {
            [Fact]
            public async void InsertsSpecificAmountOfGroupsWithEsportshubEvents_ExpectsToFindByAsyncSameAmountOfGroupsWithThoseEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_specific_amount_of_groups_to_database_and_use_findbyasync_to_get_the_same_amount").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var groups = GetGroupsWithEsportshubEvents(amountOfGroups: 7, amountOfGroupsWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = await groupRepository.FindByAsync(group => group.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)) , string.Empty);
                    Equal(groups.Count, foundGroups.Count());
                }
            }

            [Fact]
            public async void FindByAsyncGroupWithEmptyGuidInDatabase_ExpectsNoGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_group_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = GetGroups(5);
                    groups.ForEach(groupRepository.Insert);
                    groupRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var groups = await groupRepository.FindByAsync(group => group.Guid == Guid.Empty, string.Empty);
                    Equal(0, groups.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentGroupByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_group_in_database").Options;
                var group = Group.Builder().SetGroupGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = await groupRepository.FindByAsync(x => x.GroupGuid == group.Guid, string.Empty);
                    Equal(0, foundGroups.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SaveGroups_ExpectsToFindExactAmountOfGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_groups_in_database").Options;
                var groups = GetGroups(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    groupRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(null, string.Empty);
                    Equal(groups.Count, foundGroups.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfGroups_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_groups_in_database").Options;
                var groups = GetGroups(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    groupRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(null, string.Empty);
                    var foundGroupsCount = foundGroups.Count();
                    NotEqual(groups.Count - 1, foundGroupsCount);
                    NotEqual(groups.Count + 1, foundGroupsCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncGroups_ExpectsToFindExactAmountOfGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_groups_in_database_expects_to_find_exact_amount").Options;
                var groups = GetGroups(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    await groupRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(null, string.Empty);
                    Equal(groups.Count, foundGroups.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfGroups_ExpectsToNotFindDifferentAmountOfGroups()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_groups_in_database_not_to_find_different_amount").Options;
                var groups = GetGroups(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    groups.ForEach(groupRepository.Insert);
                    await groupRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Group>(context);
                    var groupRepository = new GroupRepository(internalRepository);
                    var foundGroups = groupRepository.FindBy(null, string.Empty);
                    var foundGroupsCount = foundGroups.Count();
                    NotEqual(groups.Count - 1, foundGroupsCount);
                    NotEqual(groups.Count + 1, foundGroupsCount);
                }
            }
        }
    }
}
