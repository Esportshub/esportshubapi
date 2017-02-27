using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestfulApi.App;
using RestfulApi.App.Dtos.ActivitiesDtos;
using Xunit;

namespace IntegrationTest.RestfulApi.ControllerIntegrationTest
{
    public class ActivityControllerIntegrationTest
    {
        protected static readonly string ActivityEndpoint = "/api/activities";

        public static List<Activity> InsertActivities(IActivityRepository activityRepository, int numberOfActivitiesWithTitle, int numberOfActivitiesWithoutTitle, string title)
        {
            var activities = new List<Activity>();
            var totalNumberOfActivities = numberOfActivitiesWithoutTitle + numberOfActivitiesWithTitle;
            for (var i = 0; i < totalNumberOfActivities; i++)
                activities.Add(i < numberOfActivitiesWithTitle
                    ? Activity.Builder().SetActivityGuid(Guid.NewGuid()).SetTitle(title).Build()
                    : Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build());
            activities.ForEach(activityRepository.Insert);
            activityRepository.Save();
            return activities;
        }

        public class GetActivities : IDisposable
        {
            private readonly SqliteConnection _connection;
            private readonly WebHostBuilder _webHostBuilder;
            private readonly EsportshubContext _context;
            private HttpClient _client;

            public GetActivities()
            {
                _context = new EsportshubContext(new DbContextOptionsBuilder<EsportshubContext>()
                    .UseSqlite(_connection = new SqliteConnection("DataSource=:memory:"))
                    .Options);

                _webHostBuilder = (WebHostBuilder) new WebHostBuilder().UseStartup<Startup>()
                    .ConfigureServices(
                        services => { services.AddScoped(provider => _context); });
            }

            [Fact]
            public async void ActivitiesExist_ExpectingStatusCode200()
            {
                const string title = "testTitle";

                _connection.Open();
                _context.Database.EnsureCreated();

                var repo = new ActivityRepository(new InternalRepository<Activity>(_context));

                InsertActivities(repo, 7, 3, title);


                _client = new TestServer(_webHostBuilder).CreateClient();
                var result = await _client.GetAsync(ActivityEndpoint);

                //assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }

            [Fact]
            public async void InsertingSpecificAmountOfActivities_ExpectingSameAmountOfActivities()
            {
                const string title = "testTitle";

                _connection.Open();
                _context.Database.EnsureCreated();

                var repo = new ActivityRepository(new InternalRepository<Activity>(_context));

                var amountOfActivities = 7;
                InsertActivities(repo, amountOfActivities, 0, title);

                _client = new TestServer(_webHostBuilder).CreateClient();
                var result = await _client.GetAsync(ActivityEndpoint).Result.Content.ReadAsStringAsync();
                var activitiesDtos = JsonConvert.DeserializeObject<List<ActivityDto>>(result);

                //assert
                Assert.Equal(amountOfActivities, activitiesDtos.Count);
            }

            public void Dispose()
            {
                _connection.Dispose();
                _client.Dispose();
                _context.Dispose();
                GC.SuppressFinalize(this);
                Console.WriteLine("Disposing context");
            }
        }

        public class GetActivityIntegrationTest : IDisposable
        {

            public void Dispose()
            {

            }
        }

        public class InsertActivityIntegrationTest : IDisposable
        {
            public void Dispose()
            {

            }
        }

        public class DeleteActivityIntegrationTest : IDisposable
        {

            public void Dispose()
            {

            }
        }

        public class UpdateActivityIntegrationTest : IDisposable
        {
            public void Dispose()
            {

            }
        }
    }
}
