using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.EsportshubEvents;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class EsportshubEventRepositoryIntegrationTest
    {
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
            public void Add7EsportshubEventsToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_esportshubEvents_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = GetEsportshubEvents(7);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    Equal(7, esportshubEvents.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfEsportshubEventsToDatabase_ExpectsEsportshubEventsCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_esportshubEvents_to_database").Options;
                var esportshubEvents = GetEsportshubEvents(3);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    var count = foundEsportshubEvents.Count();
                    NotEqual(esportshubEvents.Count-1, count);
                    NotEqual(esportshubEvents.Count+1, count);
                }
            }

            [Fact]
            public void AddEsportshubEventToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_esportshubEvent_to_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Insert(esportshubEvent);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvent = esportshubEventRepository.Find(esportshubEvent.EsportshubEventGuid);
                    NotNull(foundEsportshubEvent);
                    Equal(esportshubEvent.EsportshubEventGuid, foundEsportshubEvent.EsportshubEventGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<ArgumentNullException>(() => esportshubEventRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdateEsportshubEventInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_esportshubEvent_and_update_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Insert(esportshubEvent);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvent = esportshubEventRepository.Find(esportshubEvent.EsportshubEventGuid);
                    Equal(foundEsportshubEvent.Name, "Sjuften");
                    foundEsportshubEvent.Name = "DenLilleMand";
                    esportshubEventRepository.Update(foundEsportshubEvent);
                    esportshubEventRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvent = esportshubEventRepository.Find(esportshubEvent.EsportshubEventGuid);
                    Equal(foundEsportshubEvent.Name, "DenLilleMand");
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_esportshubEvent_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<ArgumentNullException>(() => esportshubEventRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentEsportshubEvent_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_esportshubEvent_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Update(esportshubEvent);
                    Throws<DbUpdateConcurrencyException>(() => esportshubEventRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentEsportshubEvent_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_esportshubEvent_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<InvalidOperationException>(() => esportshubEventRepository.Delete(esportshubEvent.EsportshubEventGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_esportshubEvent_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<ArgumentException>(() => esportshubEventRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeleteEsportshubEventInDatabase_ExpectsEsportshubEventNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Insert(esportshubEvent);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Delete(esportshubEvent.EsportshubEventGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<InvalidOperationException>(() => esportshubEventRepository.Find(esportshubEvent.EsportshubEventGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindEsportshubEventInDatabase_ExpectsToFindCorrectEsportshubEvent()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Insert(esportshubEvent);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvent = esportshubEventRepository.Find(esportshubEvent.EsportshubEventGuid);
                    Equal(foundEsportshubEvent.Guid, esportshubEvent.Guid);
                }
            }

            [Fact]
            public void FindEsportshubEventWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_esportshubEvent_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<ArgumentException>(() => esportshubEventRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentEsportshubEventInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    Throws<InvalidOperationException>(() => esportshubEventRepository.Find(esportshubEvent.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncEsportshubEventInDatabase_ExpectsToFindCorrectEsportshubEvent()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEventRepository.Insert(esportshubEvent);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvent = await esportshubEventRepository.FindAsync(esportshubEvent.EsportshubEventGuid);
                    Equal(foundEsportshubEvent.Guid, esportshubEvent.Guid);
                }
            }

            [Fact]
            public async void FindAsyncEsportshubEventWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_esportshubEvent_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await esportshubEventRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentEsportshubEventInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await esportshubEventRepository.FindAsync(esportshubEvent.Guid));
                }
            }
        }

        public class FindBy
        {

            [Fact]
            public void FindByEsportshubEventWithEmptyGuidInDatabase_ExpectsFindByReturnsNoEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_esportshubEvent_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = GetEsportshubEvents(5);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    esportshubEventRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = esportshubEventRepository.FindBy(esportshubEvent => esportshubEvent.Guid == Guid.Empty, string.Empty);
                    Equal(0, esportshubEvents.Count());
                }
            }

            [Fact]
            public void FindNonExistentEsportshubEventsByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = esportshubEventRepository.FindBy(x => x.EsportshubEventGuid == esportshubEvent.Guid, string.Empty);
                    Equal(0, esportshubEvents.Count());
                }
            }
        }

        public class FindByAsync
        {

            [Fact]
            public async void FindByAsyncEsportshubEventWithEmptyGuidInDatabase_ExpectsNoEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_esportshubEvent_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = GetEsportshubEvents(5);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    esportshubEventRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var esportshubEvents = await esportshubEventRepository.FindByAsync(esportshubEvent => esportshubEvent.Guid == Guid.Empty, string.Empty);
                    Equal(0, esportshubEvents.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentEsportshubEventByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_esportshubEvent_in_database").Options;
                var esportshubEvent = EsportshubEvent.Builder().SetEsportshubEventGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = await esportshubEventRepository.FindByAsync(x => x.EsportshubEventGuid == esportshubEvent.Guid, string.Empty);
                    Equal(0, foundEsportshubEvents.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SaveEsportshubEvents_ExpectsToFindExactAmountOfEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_esportshubEvents_in_database").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    esportshubEventRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    Equal(esportshubEvents.Count, foundEsportshubEvents.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfEsportshubEvents_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_esportshubEvents_in_database").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    esportshubEventRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    var foundEsportshubEventsCount = foundEsportshubEvents.Count();
                    NotEqual(esportshubEvents.Count - 1, foundEsportshubEventsCount);
                    NotEqual(esportshubEvents.Count + 1, foundEsportshubEventsCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncEsportshubEvents_ExpectsToFindExactAmountOfEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_esportshubEvents_in_database_expects_to_find_exact_amount").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    await esportshubEventRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    Equal(esportshubEvents.Count, foundEsportshubEvents.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfEsportshubEvents_ExpectsToNotFindDifferentAmountOfEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_esportshubEvents_in_database_not_to_find_different_amount").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    esportshubEvents.ForEach(esportshubEventRepository.Insert);
                    await esportshubEventRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<EsportshubEvent>(context);
                    var esportshubEventRepository = new EsportshubEventRepository(internalRepository);
                    var foundEsportshubEvents = esportshubEventRepository.FindBy(null, string.Empty);
                    var foundEsportshubEventsCount = foundEsportshubEvents.Count();
                    NotEqual(esportshubEvents.Count - 1, foundEsportshubEventsCount);
                    NotEqual(esportshubEvents.Count + 1, foundEsportshubEventsCount);
                }
            }
        }
    }
}
