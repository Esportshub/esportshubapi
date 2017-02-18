using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class PlayerRepositoryIntegrationTest
    {
        public static List<Player> GetPlayers(int amount)
        {
            var players = new List<Player>();
            for (var i = 0; i < amount; i++)
            {
                players.Add(Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build());
            }
            return players;
        }

        public static List<Player> GetPlayersWithActivities(int amount, List<Activity> activities)
        {
            var players = new List<Player>();
            for (var i = 0; i < amount; i++)
            {
                players.Add(Player.Builder().SetPlayerGuid(Guid.NewGuid()).SetActivities(activities).Build());
            }
            return players;
        }

        public static List<Activity> GetActivities(int amount)
        {
            var activities = new List<Activity>();
            for (var i = 0; i < amount; i++)
            {
                activities.Add(Activity.Builder().SetActivityGuid(Guid.NewGuid()).Build());
            }
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
                    var players = playerRepository.FindBy(null, "");
                    Assert.Equal(7, players.Count());
                }
            }

            [Fact]
            public void Add3PlayersToDatabase_ExpectsPlayersCountNotEqual4()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_players_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = GetPlayers(3);
                    players.ForEach(playerRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = playerRepository.FindBy(null, "");
                    Assert.NotEqual(4, players.Count());
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
                    Assert.NotNull(foundPlayer);
                    Assert.Equal(player.PlayerGuid, foundPlayer.PlayerGuid);
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
                    Assert.Throws<ArgumentNullException>(() => playerRepository.Insert(null));
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
                    Assert.Equal(foundPlayer.Nickname, "Sjuften");
                    foundPlayer.Nickname = "DenLilleMand";
                    playerRepository.Update(foundPlayer);
                    playerRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var foundPlayer = playerRepository.Find(player.PlayerGuid);
                    Assert.Equal(foundPlayer.Nickname, "DenLilleMand");
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
                    Assert.Throws<ArgumentNullException>(() => playerRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistantPlayer_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_player_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).SetNickname("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Update(player);
                    Assert.Throws<DbUpdateConcurrencyException>(() => playerRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistantPlayer_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_player_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    playerRepository.Delete(player.PlayerGuid);
                    Assert.True(true);
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
                    Assert.ThrowsAsync<ArgumentException>(async () => await playerRepository.DeleteAsync(Guid.Empty));
                }
            }

            [Fact]
            public void DeletePlayerInDatabase_ExpectsPlayerNonExistantAfterDeletion()
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
                    Assert.Throws<InvalidOperationException>(() => playerRepository.Find(player.PlayerGuid));
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
                    Assert.Equal(foundPlayer.Guid, player.Guid);
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
                    Assert.Throws<ArgumentException>(() => playerRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistantPlayerInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Assert.Throws<InvalidOperationException>(() => playerRepository.Find(player.Guid));
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
                var players = GetPlayersWithActivities(7, activities);
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
                    Assert.Equal(7, foundPlayers.Count());
                }
            }

            [Fact]
            public void FindByPlayerWithEmptyGuidInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_player_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    var players = playerRepository.FindBy(player => player.Guid == Guid.Empty, string.Empty);
                    Assert.Equal(0, players.Count());
                }
            }

            [Fact]
            public void FindNonExistantPlayerInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_player_in_database").Options;
                var player = Player.Builder().SetPlayerGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Player>(context);
                    var playerRepository = new PlayerRepository(internalRepository);
                    Assert.Throws<InvalidOperationException>(() => playerRepository.Find(player.Guid));
                }
            }
        }
    }
}