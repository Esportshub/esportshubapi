using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Teams;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class TeamRepositoryIntegrationTest
    {
        public static List<Team> GetTeams(int amount)
        {
            var teams = new List<Team>();
            for (var i = 0; i < amount; i++)
                teams.Add(Team.Builder().SetTeamGuid(Guid.NewGuid()).Build());
            return teams;
        }

        public static List<Team> GetTeamsWithEsportshubEvents(int amountOfTeams, int amountOfTeamsWithoutEsportshubEvents, List<EsportshubEvent> esportshubEvents)
        {
            var teams = new List<Team>();
            for (var i = 0; i < amountOfTeams; i++)
                teams.Add(Team.Builder().SetTeamGuid(Guid.NewGuid()).SetEsportshubEvents(esportshubEvents.ToList()).Build());
            for (var i = 0; i < amountOfTeamsWithoutEsportshubEvents; i++)
                teams.Add(Team.Builder().SetTeamGuid(Guid.NewGuid()).Build());
            return teams;
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
            public void Add7TeamsToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_teams_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = GetTeams(7);
                    teams.ForEach(teamRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = teamRepository.FindBy(null, string.Empty);
                    Equal(7, teams.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfTeamsToDatabase_ExpectsTeamsCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_teams_to_database").Options;
                var teams = GetTeams(3);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(null, string.Empty);
                    var count = foundTeams.Count();
                    NotEqual(teams.Count-1, count);
                    NotEqual(teams.Count+1, count);
                }
            }

            [Fact]
            public void AddTeamToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_team_to_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Insert(team);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeam = teamRepository.Find(team.TeamGuid);
                    NotNull(foundTeam);
                    Equal(team.TeamGuid, foundTeam.TeamGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<ArgumentNullException>(() => teamRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdateTeamInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_team_and_update_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Insert(team);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeam = teamRepository.Find(team.TeamGuid);
                    Equal(foundTeam.Name, "Sjuften");
                    foundTeam.Name = "DenLilleMand";
                    teamRepository.Update(foundTeam);
                    teamRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeam = teamRepository.Find(team.TeamGuid);
                    Equal(foundTeam.Name, "DenLilleMand");
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_team_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<ArgumentNullException>(() => teamRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentTeam_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_team_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Update(team);
                    Throws<DbUpdateConcurrencyException>(() => teamRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentTeam_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_team_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<InvalidOperationException>(() => teamRepository.Delete(team.TeamGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_team_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<ArgumentException>(() => teamRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeleteTeamInDatabase_ExpectsTeamNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Insert(team);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Delete(team.TeamGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<InvalidOperationException>(() => teamRepository.Find(team.TeamGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindTeamInDatabase_ExpectsToFindCorrectTeam()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Insert(team);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeam = teamRepository.Find(team.TeamGuid);
                    Equal(foundTeam.Guid, team.Guid);
                }
            }

            [Fact]
            public void FindTeamWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_team_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<ArgumentException>(() => teamRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentTeamInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    Throws<InvalidOperationException>(() => teamRepository.Find(team.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncTeamInDatabase_ExpectsToFindCorrectTeam()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teamRepository.Insert(team);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeam = await teamRepository.FindAsync(team.TeamGuid);
                    Equal(foundTeam.Guid, team.Guid);
                }
            }

            [Fact]
            public async void FindAsyncTeamWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_team_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await teamRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentTeamInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await teamRepository.FindAsync(team.Guid));
                }
            }
        }

        public class FindBy
        {
            [Fact]
            public void FindTeamsByFilterInDatabase_ExpectsToFindCorrectTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_teams_to_database_and_use_findby").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var teams = GetTeamsWithEsportshubEvents(amountOfTeams: 7, amountOfTeamsWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(team => team.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)), string.Empty);
                    Equal(expected: teams.Count, actual: foundTeams.Count());
                }
            }

            [Fact]
            public void FindByTeamWithEmptyGuidInDatabase_ExpectsFindByReturnsNoTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_team_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = GetTeams(5);
                    teams.ForEach(teamRepository.Insert);
                    teamRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = teamRepository.FindBy(team => team.Guid == Guid.Empty, string.Empty);
                    Equal(0, teams.Count());
                }
            }

            [Fact]
            public void FindNonExistentTeamsByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = teamRepository.FindBy(x => x.TeamGuid == team.Guid, string.Empty);
                    Equal(0, teams.Count());
                }
            }
        }

        public class FindByAsync
        {
            [Fact]
            public async void InsertsSpecificAmountOfTeamsWithEsportshubEvents_ExpectsToFindByAsyncSameAmountOfTeamsWithThoseEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_specific_amount_of_teams_to_database_and_use_findbyasync_to_get_the_same_amount").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var teams = GetTeamsWithEsportshubEvents(amountOfTeams: 7, amountOfTeamsWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = await teamRepository.FindByAsync(team => team.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)) , string.Empty);
                    Equal(teams.Count, foundTeams.Count());
                }
            }

            [Fact]
            public async void FindByAsyncTeamWithEmptyGuidInDatabase_ExpectsNoTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_team_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = GetTeams(5);
                    teams.ForEach(teamRepository.Insert);
                    teamRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var teams = await teamRepository.FindByAsync(team => team.Guid == Guid.Empty, string.Empty);
                    Equal(0, teams.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentTeamByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_team_in_database").Options;
                var team = Team.Builder().SetTeamGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = await teamRepository.FindByAsync(x => x.TeamGuid == team.Guid, string.Empty);
                    Equal(0, foundTeams.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SaveTeams_ExpectsToFindExactAmountOfTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_teams_in_database").Options;
                var teams = GetTeams(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    teamRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(null, string.Empty);
                    Equal(teams.Count, foundTeams.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfTeams_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_teams_in_database").Options;
                var teams = GetTeams(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    teamRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(null, string.Empty);
                    var foundTeamsCount = foundTeams.Count();
                    NotEqual(teams.Count - 1, foundTeamsCount);
                    NotEqual(teams.Count + 1, foundTeamsCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncTeams_ExpectsToFindExactAmountOfTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_teams_in_database_expects_to_find_exact_amount").Options;
                var teams = GetTeams(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    await teamRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(null, string.Empty);
                    Equal(teams.Count, foundTeams.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfTeams_ExpectsToNotFindDifferentAmountOfTeams()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_teams_in_database_not_to_find_different_amount").Options;
                var teams = GetTeams(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    teams.ForEach(teamRepository.Insert);
                    await teamRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Team>(context);
                    var teamRepository = new TeamRepository(internalRepository);
                    var foundTeams = teamRepository.FindBy(null, string.Empty);
                    var foundTeamsCount = foundTeams.Count();
                    NotEqual(teams.Count - 1, foundTeamsCount);
                    NotEqual(teams.Count + 1, foundTeamsCount);
                }
            }
        }
    }
}
