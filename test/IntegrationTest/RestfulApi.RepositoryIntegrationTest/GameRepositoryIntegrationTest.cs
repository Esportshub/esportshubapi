using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Games;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class GameRepositoryIntegrationTest
    {
        public static List<Game> GetGames(int amount)
        {
            var games = new List<Game>();
            for (var i = 0; i < amount; i++)
                games.Add(Game.Builder().SetGameGuid(Guid.NewGuid()).Build());
            return games;
        }

        public static List<Game> GetGamesWithEsportshubEvents(int amountOfGames, int amountOfGamesWithoutEsportshubEvents, List<EsportshubEvent> esportshubEvents)
        {
            var games = new List<Game>();
            for (var i = 0; i < amountOfGames; i++)
                games.Add(Game.Builder().SetGameGuid(Guid.NewGuid()).SetEsportshubEvents(esportshubEvents.ToList()).Build());
            for (var i = 0; i < amountOfGamesWithoutEsportshubEvents; i++)
                games.Add(Game.Builder().SetGameGuid(Guid.NewGuid()).Build());
            return games;
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
            public void Add7GamesToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_games_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = GetGames(7);
                    games.ForEach(gameRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = gameRepository.FindBy(null, string.Empty);
                    Equal(7, games.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfGamesToDatabase_ExpectsGamesCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_games_to_database").Options;
                var games = GetGames(3);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(null, string.Empty);
                    var count = foundGames.Count();
                    NotEqual(games.Count-1, count);
                    NotEqual(games.Count+1, count);
                }
            }

            [Fact]
            public void AddGameToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_game_to_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Insert(game);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGame = gameRepository.Find(game.GameGuid);
                    NotNull(foundGame);
                    Equal(game.GameGuid, foundGame.GameGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<ArgumentNullException>(() => gameRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdateGameInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_game_and_update_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Insert(game);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGame = gameRepository.Find(game.GameGuid);
                    Equal(foundGame.Name, "Sjuften");
                    foundGame.Name = "DenLilleMand";
                    gameRepository.Update(foundGame);
                    gameRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGame = gameRepository.Find(game.GameGuid);
                    Equal(foundGame.Name, "DenLilleMand");
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_game_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<ArgumentNullException>(() => gameRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentGame_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_game_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).SetName("Sjuften").Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Update(game);
                    Throws<DbUpdateConcurrencyException>(() => gameRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentGame_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_game_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<InvalidOperationException>(() => gameRepository.Delete(game.GameGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_game_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<ArgumentException>(() => gameRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeleteGameInDatabase_ExpectsGameNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Insert(game);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Delete(game.GameGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<InvalidOperationException>(() => gameRepository.Find(game.GameGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindGameInDatabase_ExpectsToFindCorrectGame()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Insert(game);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGame = gameRepository.Find(game.GameGuid);
                    Equal(foundGame.Guid, game.Guid);
                }
            }

            [Fact]
            public void FindGameWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_game_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<ArgumentException>(() => gameRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentGameInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    Throws<InvalidOperationException>(() => gameRepository.Find(game.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncGameInDatabase_ExpectsToFindCorrectGame()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    gameRepository.Insert(game);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGame = await gameRepository.FindAsync(game.GameGuid);
                    Equal(foundGame.Guid, game.Guid);
                }
            }

            [Fact]
            public async void FindAsyncGameWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_game_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await gameRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentGameInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await gameRepository.FindAsync(game.Guid));
                }
            }
        }

        public class FindBy
        {
            [Fact]
            public void FindGamesByFilterInDatabase_ExpectsToFindCorrectGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_games_to_database_and_use_findby").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var games = GetGamesWithEsportshubEvents(amountOfGames: 7, amountOfGamesWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(game => game.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)), string.Empty);
                    Equal(expected: games.Count, actual: foundGames.Count());
                }
            }

            [Fact]
            public void FindByGameWithEmptyGuidInDatabase_ExpectsFindByReturnsNoGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_game_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = GetGames(5);
                    games.ForEach(gameRepository.Insert);
                    gameRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = gameRepository.FindBy(game => game.Guid == Guid.Empty, string.Empty);
                    Equal(0, games.Count());
                }
            }

            [Fact]
            public void FindNonExistentGamesByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = gameRepository.FindBy(x => x.GameGuid == game.Guid, string.Empty);
                    Equal(0, games.Count());
                }
            }
        }

        public class FindByAsync
        {
            [Fact]
            public async void InsertsSpecificAmountOfGamesWithEsportshubEvents_ExpectsToFindByAsyncSameAmountOfGamesWithThoseEsportshubEvents()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_specific_amount_of_games_to_database_and_use_findbyasync_to_get_the_same_amount").Options;
                var esportshubEvents = GetEsportshubEvents(7);
                var games = GetGamesWithEsportshubEvents(amountOfGames: 7, amountOfGamesWithoutEsportshubEvents: 3, esportshubEvents: esportshubEvents);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = await gameRepository.FindByAsync(game => game.EsportshubEvents.All(esportshubEvent => esportshubEvents.Contains(esportshubEvent)) , string.Empty);
                    Equal(games.Count, foundGames.Count());
                }
            }

            [Fact]
            public async void FindByAsyncGameWithEmptyGuidInDatabase_ExpectsNoGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_game_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = GetGames(5);
                    games.ForEach(gameRepository.Insert);
                    gameRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var games = await gameRepository.FindByAsync(game => game.Guid == Guid.Empty, string.Empty);
                    Equal(0, games.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentGameByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_game_in_database").Options;
                var game = Game.Builder().SetGameGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = await gameRepository.FindByAsync(x => x.GameGuid == game.Guid, string.Empty);
                    Equal(0, foundGames.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SaveGames_ExpectsToFindExactAmountOfGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_games_in_database").Options;
                var games = GetGames(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    gameRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(null, string.Empty);
                    Equal(games.Count, foundGames.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfGames_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_games_in_database").Options;
                var games = GetGames(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    gameRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(null, string.Empty);
                    var foundGamesCount = foundGames.Count();
                    NotEqual(games.Count - 1, foundGamesCount);
                    NotEqual(games.Count + 1, foundGamesCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncGames_ExpectsToFindExactAmountOfGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_games_in_database_expects_to_find_exact_amount").Options;
                var games = GetGames(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    await gameRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(null, string.Empty);
                    Equal(games.Count, foundGames.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfGames_ExpectsToNotFindDifferentAmountOfGames()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_games_in_database_not_to_find_different_amount").Options;
                var games = GetGames(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    games.ForEach(gameRepository.Insert);
                    await gameRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Game>(context);
                    var gameRepository = new GameRepository(internalRepository);
                    var foundGames = gameRepository.FindBy(null, string.Empty);
                    var foundGamesCount = foundGames.Count();
                    NotEqual(games.Count - 1, foundGamesCount);
                    NotEqual(games.Count + 1, foundGamesCount);
                }
            }
        }
    }
}
