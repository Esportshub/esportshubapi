//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Data.App.Models;
//using Data.App.Models.Entities;
//using Data.App.Models.Repositories;
//using Data.App.Models.Repositories.Activities;
//using Microsoft.EntityFrameworkCore;
//using Xunit;
//using static Xunit.Assert;
//
//namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
//{
//    public class ActivityRepositoryIntegrationTest
//    {
//        public static List<Activity> GetActivities(int amount, string description)
//        {
//            var activitys = new List<Activity>();
//            for (var i = 0; i < amount; i++)
//                activitys.Add(Activity.Builder().SetActivityGuid(Guid.NewGuid()).SetDescription(description).Build());
//            return activitys;
//        }
//
//        public class Insert
//        {
//            [Fact]
//            public void Add7ActivitysToDatabase_ExpectsIEnumerableCountEquals7()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_activitys_to_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = GetActivitys(7);
//                    activitys.ForEach(activityRepository.Insert);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = activityRepository.FindBy(null, string.Empty);
//                    Equal(7, activitys.Count());
//                }
//            }
//
//            [Fact]
//            public void AddSpecificAmountOfActivitysToDatabase_ExpectsActivitysCountNotEqualBorderValueCounts()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_activitys_to_database").Options;
//                var activitys = GetActivitys(3);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(null, string.Empty);
//                    var count = foundActivitys.Count();
//                    NotEqual(activitys.Count-1, count);
//                    NotEqual(activitys.Count+1, count);
//                }
//            }
//
//            [Fact]
//            public void AddActivityToDatabase_ExpectsCorrectValues()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_activity_to_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Insert(activity);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivity = activityRepository.Find(activity.ActivityGuid);
//                    NotNull(foundActivity);
//                    Equal(activity.ActivityGuid, foundActivity.ActivityGuid);
//                }
//            }
//
//            [Fact]
//            public void AddNullToDatabase_ExpectsArgumentNullException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<ArgumentNullException>(() => activityRepository.Insert(null));
//                }
//            }
//        }
//
//        public class Update
//        {
//            [Fact]
//            public void UpdateActivityInDatabase_ExpectsCorrectUpdatedValues()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_activity_and_update_activity_in_database").Options;
//                const string testTitle = "test_title";
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).SetTitle(testTitle).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Insert(activity);
//                    context.SaveChanges();
//                }
//
//                const string newTestTitle = "new_test_title";
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivity = activityRepository.Find(activity.ActivityGuid);
//                    Equal(foundActivity.Title, testTitle);
//                    foundActivity.Title = newTestTitle;
//                    activityRepository.Update(foundActivity);
//                    activityRepository.Save();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivity = activityRepository.Find(activity.ActivityGuid);
//                    Equal(foundActivity.Title, newTestTitle);
//                }
//            }
//
//            [Fact]
//            public void UpdateWithNull_ExpectsArgumentNullException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_activity_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<ArgumentNullException>(() => activityRepository.Update(null));
//                }
//            }
//
//            [Fact]
//            public void UpdateNonExistentActivity_ExpectsDbUpdateConcurrencyException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_activity_database").Options;
//                const string testTitle = "test_title";
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).SetTitle(testTitle).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Update(activity);
//                    Throws<DbUpdateConcurrencyException>(() => activityRepository.Save());
//                }
//            }
//        }
//
//        public class Delete
//        {
//            [Fact]
//            public void DeleteNonExistentActivity_ExpectsInvalidOperationException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_activity_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<InvalidOperationException>(() => activityRepository.Delete(activity.ActivityGuid));
//                }
//            }
//
//            [Fact]
//            public void DeleteWithEmptyGuid_ExpectsArgumentException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_activity_with_empty_guid_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<ArgumentException>(() => activityRepository.Delete(Guid.Empty));
//                }
//            }
//
//            [Fact]
//            public void DeleteActivityInDatabase_ExpectsActivityNonExistentAfterDeletion()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Insert(activity);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Delete(activity.ActivityGuid);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<InvalidOperationException>(() => activityRepository.Find(activity.ActivityGuid));
//                }
//            }
//        }
//
//        public class Find
//        {
//            [Fact]
//            public void FindActivityInDatabase_ExpectsToFindCorrectActivity()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Insert(activity);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivity = activityRepository.Find(activity.ActivityGuid);
//                    Equal(foundActivity.Guid, activity.Guid);
//                }
//            }
//
//            [Fact]
//            public void FindActivityWithEmptyGuidInDatabase_ExpectsArgumentException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_activity_with_empty_guid_in_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<ArgumentException>(() => activityRepository.Find(Guid.Empty));
//                }
//            }
//
//            [Fact]
//            public void FindNonExistentActivityInDatabase_ExpectsInvalidOperationException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    Throws<InvalidOperationException>(() => activityRepository.Find(activity.Guid));
//                }
//            }
//        }
//
//        public class FindAsync
//        {
//            [Fact]
//            public async void FindAsyncActivityInDatabase_ExpectsToFindCorrectActivity()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activityRepository.Insert(activity);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivity = await activityRepository.FindAsync(activity.ActivityGuid);
//                    Equal(foundActivity.Guid, activity.Guid);
//                }
//            }
//
//            [Fact]
//            public async void FindAsyncActivityWithEmptyGuidInDatabase_ExpectsArgumentException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_activity_with_empty_guid_in_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    await ThrowsAsync<ArgumentException>(async () => await activityRepository.FindAsync(Guid.Empty));
//                }
//            }
//
//            [Fact]
//            public async void FindAsyncNonExistentActivityInDatabase_ExpectsInvalidOperationException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    await ThrowsAsync<InvalidOperationException>(async () => await activityRepository.FindAsync(activity.Guid));
//                }
//            }
//        }
//
//        public class FindBy
//        {
//            [Fact]
//            public void FindActivitiesByFilterInDatabase_ExpectsToFindCorrectActivities()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_activitys_to_database_and_use_findby").Options;
//                const string testDescription = "testDescription";
//                var activitys = GetActivities(amountOfActivitys: 7, 3, testDescription );
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(activity => activity.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)), string.Empty);
//                    Equal(expected: activitys.Count, actual: foundActivitys.Count());
//                }
//            }
//
//            [Fact]
//            public void FindByActivityWithEmptyGuidInDatabase_ExpectsFindByReturnsNoActivitys()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_activity_with_empty_guid_in_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = GetActivitys(5);
//                    activitys.ForEach(activityRepository.Insert);
//                    activityRepository.Save();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = activityRepository.FindBy(activity => activity.Guid == Guid.Empty, string.Empty);
//                    Equal(0, activitys.Count());
//                }
//            }
//
//            [Fact]
//            public void FindNonExistentActivitysByFilterInDatabase_ExpectsInvalidOperationException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = activityRepository.FindBy(x => x.ActivityGuid == activity.Guid, string.Empty);
//                    Equal(0, activitys.Count());
//                }
//            }
//        }
//
//        public class FindByAsync
//        {
//            [Fact]
//            public async void InsertsSpecificAmountOfActivitysWithEsportshubEvents_ExpectsToFindByAsyncSameAmountOfActivitysWithThoseEsportshubEvents()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_specific_amount_of_activitys_to_database_and_use_findbyasync_to_get_the_same_amount").Options;
//                var esportshubEvents = GetEsportshubEvents(7);
//                var activitys = GetActivitysWithEsportshubEvents(amountOfActivitys: 7, amountOfActivitysWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    context.SaveChanges();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = await activityRepository.FindByAsync(activity => activity.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)) , string.Empty);
//                    Equal(activitys.Count, foundActivitys.Count());
//                }
//            }
//
//            [Fact]
//            public async void FindByAsyncActivityWithEmptyGuidInDatabase_ExpectsNoActivitys()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_activity_with_empty_guid_in_database").Options;
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = GetActivitys(5);
//                    activitys.ForEach(activityRepository.Insert);
//                    activityRepository.Save();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var activitys = await activityRepository.FindByAsync(activity => activity.Guid == Guid.Empty, string.Empty);
//                    Equal(0, activitys.Count());
//                }
//            }
//
//            [Fact]
//            public async void FindByAsyncNonExistentActivityByFilterInDatabase_ExpectsInvalidOperationException()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_activity_in_database").Options;
//                var activity = Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build();
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = await activityRepository.FindByAsync(x => x.ActivityGuid == activity.Guid, string.Empty);
//                    Equal(0, foundActivitys.Count());
//                }
//            }
//        }
//
//        public class Save
//        {
//            [Fact]
//            public void SaveActivitys_ExpectsToFindExactAmountOfActivitys()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_activitys_in_database").Options;
//                var activitys = GetActivitys(7);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    activityRepository.Save();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(null, string.Empty);
//                    Equal(activitys.Count, foundActivitys.Count());
//                }
//            }
//
//            [Fact]
//            public void SaveSpecificAmountOfActivitys_ExpectsToNotFindDifferentAmount()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_activitys_in_database").Options;
//                var activitys = GetActivitys(7);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    activityRepository.Save();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(null, string.Empty);
//                    var foundActivitysCount = foundActivitys.Count();
//                    NotEqual(activitys.Count - 1, foundActivitysCount);
//                    NotEqual(activitys.Count + 1, foundActivitysCount);
//                }
//            }
//        }
//
//        public class SaveAsync
//        {
//            [Fact]
//            public async void SaveAsyncActivitys_ExpectsToFindExactAmountOfActivitys()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_activitys_in_database_expects_to_find_exact_amount").Options;
//                var activitys = GetActivitys(7);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    await activityRepository.SaveAsync();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(null, string.Empty);
//                    Equal(activitys.Count, foundActivitys.Count());
//                }
//            }
//
//            [Fact]
//            public async void SaveAsyncSpecificAmountOfActivitys_ExpectsToNotFindDifferentAmountOfActivitys()
//            {
//                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_activitys_in_database_not_to_find_different_amount").Options;
//                var activitys = GetActivitys(7);
//                using (var context = new EsportshubContext(options))
//                {
//                    context.Database.EnsureDeleted();
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    activitys.ForEach(activityRepository.Insert);
//                    await activityRepository.SaveAsync();
//                }
//
//                using (var context = new EsportshubContext(options))
//                {
//                    var internalRepository = new InternalRepository<Activity>(context);
//                    var activityRepository = new ActivityRepository(internalRepository);
//                    var foundActivitys = activityRepository.FindBy(null, string.Empty);
//                    var foundActivitysCount = foundActivitys.Count();
//                    NotEqual(activitys.Count - 1, foundActivitysCount);
//                    NotEqual(activitys.Count + 1, foundActivitysCount);
//                }
//            }
//        }
//    }
//}
