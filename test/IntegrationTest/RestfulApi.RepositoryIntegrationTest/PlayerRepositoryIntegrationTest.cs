using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class PlayerRepositoryIntegrationTest
    {
        public static List<Player> GetPlayers(int amount)
        {
            var players = new List<Player>();
            for (var i = 0; i < amount; i++)
                players.Add(Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build());
            return players;
        }

        public static List<Player> GetPlayersWithActivities(int amountOfPlayers, int amountOfPlayersWithoutActivities, List<Activity> activities)
        {
            var players = new List<Player>();
            for (var i = 0; i < amountOfPlayers; i++)
                players.Add(Player.Builder().SetPlayerGuid(Guid.NewGuid()).SetActivities(activities.ToList()).Build());
            for (var i = 0; i < amountOfPlayersWithoutActivities; i++)
                players.Add(Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build());
            return players;
        }

        public static List<Activity> GetActivities(int amount)
        {
            var activities = new List<Activity>();
            for (var i = 0; i < amount; i++)
                activities.Add(Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build());
            return activities;
        }

        public class Insert
        {
            [Fact]
            public void Add7PlayersToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_players_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = GetPlayers(7);
                    players.ForEach(playerRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = playerRepository.FindBy(null, string.Empty);
                    Equal(7, players.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfPlayersToDatabase_ExpectsPlayersCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_players_to_database").Options;
                var players = GetPlayers(3);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(null, string.Empty);
                    var count = foundPlayers.Count();
                    NotEqual(players.Count-1, count);
                    NotEqual(players.Count+1, count);
                }
            }

            [Fact]
            public void AddPlayerToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_player_to_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Insert(player);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = playerRepository.Find(player.PlayerGuid);
                    NotNull(foundPlayer);
                    Equal(player.PlayerGuid, foundPlayer.PlayerGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<ArgumentNullException>(() => playerRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdatePlayerInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_player_and_update_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).SetNickname("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Insert(player);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = playerRepository.Find(player.PlayerGuid);
                    Equal(foundPlayer.Nickname, "Sjuften");
                    foundPlayer.Nickname = "DenLilleMand";
                    playerRepository.Update(foundPlayer);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = playerRepository.Find(player.PlayerGuid);
                    Equal(foundPlayer.Nickname, "DenLilleMand");
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_player_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<ArgumentNullException>(() => playerRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentPlayer_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_player_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).SetNickname("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Update(player);
                    Throws<DbUpdateConcurrencyException>(() => playerRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentPlayer_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_player_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<InvalidOperationException>(() => playerRepository.Delete(player.PlayerGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_player_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<ArgumentException>(() => playerRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeletePlayerInDatabase_ExpectsPlayerNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Insert(player);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Delete(player.PlayerGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<InvalidOperationException>(() => playerRepository.Find(player.PlayerGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindPlayerInDatabase_ExpectsToFindCorrectPlayer()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Insert(player);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = playerRepository.Find(player.PlayerGuid);
                    Equal(foundPlayer.Guid, player.Guid);
                }
            }

            [Fact]
            public void FindPlayerWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_player_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<ArgumentException>(() => playerRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentPlayerInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Throws<InvalidOperationException>(() => playerRepository.Find(player.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncPlayerInDatabase_ExpectsToFindCorrectPlayer()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Insert(player);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = await playerRepository.FindAsync(player.PlayerGuid);
                    Equal(foundPlayer.Guid, player.Guid);
                }
            }

            [Fact]
            public async void FindAsyncPlayerWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_player_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await playerRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentPlayerInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await playerRepository.FindAsync(player.Guid));
                }
            }
        }

        public class FindBy
        {
            [Fact]
            public void FindPlayersByFilterInDatabase_ExpectsToFindCorrectPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_players_to_database_and_use_findby").Options;
                var activities = GetActivities(7);
                var players = GetPlayersWithActivities(amountOfPlayers: 7, amountOfPlayersWithoutActivities: 3, activities: activities);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(player => player.Activities.All(activity => activities.Contains(activity)), string.Empty);
                    Equal(players.Count(), foundPlayers.Count());
                }
            }

            [Fact]
            public void FindByPlayerWithEmptyGuidInDatabase_ExpectsFindByReturnsNoPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_player_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = GetPlayers(5);
                    players.ForEach(playerRepository.Insert);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = playerRepository.FindBy(player => player.Guid == Guid.Empty, string.Empty);
                    Equal(0, players.Count());
                }
            }

            [Fact]
            public void FindNonExistentPlayersByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = playerRepository.FindBy(x => x.PlayerGuid == player.Guid, string.Empty);
                    Equal(0, players.Count());
                }
            }
        }

        public class FindByAsync
        {
            [Fact]
            public async void InsertsSpecificAmountOfPlayersWithActivities_ExpectsToFindByAsyncSameAmountOfPlayersWithThoseActivities()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_specific_amount_of_players_to_database_and_use_findbyasync_to_get_the_same_amount").Options;
                var activities = GetActivities(7);
                var players = GetPlayersWithActivities(amountOfPlayers: 7, amountOfPlayersWithoutActivities: 3, activities: activities);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = await playerRepository.FindByAsync(player => player.Activities.All(activity => activities.Contains(activity)) , string.Empty);
                    Equal(players.Count(), foundPlayers.Count());
                }
            }

            [Fact]
            public async void FindByAsyncPlayerWithEmptyGuidInDatabase_ExpectsNoPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_player_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = GetPlayers(5);
                    players.ForEach(playerRepository.Insert);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = await playerRepository.FindByAsync(player => player.Guid == Guid.Empty, string.Empty);
                    Equal(0, players.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentPlayerByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = await playerRepository.FindByAsync(x => x.PlayerGuid == player.Guid, string.Empty);
                    Equal(0, foundPlayers.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SavePlayers_ExpectsToFindExactAmountOfPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_players_in_database").Options;
                var players = GetPlayers(7);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(null, string.Empty);
                    Equal(players.Count(), foundPlayers.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfPlayers_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_players_in_database").Options;
                var players = GetPlayers(7);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(null, string.Empty);
                    var foundPlayersCount = foundPlayers.Count();
                    NotEqual(players.Count() - 1, foundPlayersCount);
                    NotEqual(players.Count() + 1, foundPlayersCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncPlayers_ExpectsToFindExactAmountOfPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_players_in_database_expects_to_find_exact_amount").Options;
                var players = GetPlayers(7);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    await playerRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(null, string.Empty);
                    Equal(players.Count(), foundPlayers.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfPlayers_ExpectsToNotFindDifferentAmountOfPlayers()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_players_in_database_not_to_find_different_amount").Options;
                var players = GetPlayers(7);
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    players.ForEach(playerRepository.Insert);
                    await playerRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayers = playerRepository.FindBy(null, string.Empty);
                    var foundPlayersCount = foundPlayers.Count();
                    NotEqual(players.Count() - 1, foundPlayersCount);
                    NotEqual(players.Count() + 1, foundPlayersCount);
                }
            }
        }
    }
}