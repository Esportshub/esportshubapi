using System;
using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Integrations;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Xunit.Assert;

namespace IntegrationTest.RestfulApi.RepositoryIntegrationTest
{
    public class IntegrationRepositoryIntegrationTest
    {
        public static List<Integration> GetIntegrations(int amount)
        {
            var integrations = new List<Integration>();
            for (var i = 0; i < amount; i++)
                integrations.Add(Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build());
            return integrations;
        }

        public class Insert
        {
            [Fact]
            public void Add7IntegrationsToDatabase_ExpectsIEnumerableCountEquals7()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_7_integrations_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = GetIntegrations(7);
                    integrations.ForEach(integrationRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = integrationRepository.FindBy(null, string.Empty);
                    Equal(7, integrations.Count());
                }
            }

            [Fact]
            public void AddSpecificAmountOfIntegrationsToDatabase_ExpectsIntegrationsCountNotEqualBorderValueCounts()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_3_integrations_to_database").Options;
                var integrations = GetIntegrations(3);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrations.ForEach(integrationRepository.Insert);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = integrationRepository.FindBy(null, string.Empty);
                    var count = foundIntegrations.Count();
                    NotEqual(integrations.Count-1, count);
                    NotEqual(integrations.Count+1, count);
                }
            }

            [Fact]
            public void AddIntegrationToDatabase_ExpectsCorrectValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_1_integration_to_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Insert(integration);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegration = integrationRepository.Find(integration.IntegrationGuid);
                    NotNull(foundIntegration);
                    Equal(integration.IntegrationGuid, foundIntegration.IntegrationGuid);
                }
            }

            [Fact]
            public void AddNullToDatabase_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_null_to_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<ArgumentNullException>(() => integrationRepository.Insert(null));
                }
            }
        }

        public class Update
        {
            [Fact]
            public void UpdateIntegrationInDatabase_ExpectsCorrectUpdatedValues()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Add_integration_and_update_integration_in_database").Options;
                var guid = Guid.NewGuid();
                var integration = Integration.Builder().SetIntegrationGuid(guid).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Insert(integration);
                    context.SaveChanges();
                }

                var newGuid = Guid.NewGuid();
                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegration = integrationRepository.Find(integration.IntegrationGuid);
                    Equal(foundIntegration.Guid, guid);
                    foundIntegration.IntegrationGuid = newGuid;
                    integrationRepository.Update(foundIntegration);
                    integrationRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegration = integrationRepository.Find(newGuid);
                    Equal(foundIntegration.Guid, newGuid);
                }
            }

            [Fact]
            public void UpdateWithNull_ExpectsArgumentNullException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_null_integration_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<ArgumentNullException>(() => integrationRepository.Update(null));
                }
            }

            [Fact]
            public void UpdateNonExistentIntegration_ExpectsDbUpdateConcurrencyException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Update_non_existant_integration_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Update(integration);
                    Throws<DbUpdateConcurrencyException>(() => integrationRepository.Save());
                }
            }
        }

        public class Delete
        {
            [Fact]
            public void DeleteNonExistentIntegration_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_non_existant_integration_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<InvalidOperationException>(() => integrationRepository.Delete(integration.IntegrationGuid));
                }
            }

            [Fact]
            public void DeleteWithEmptyGuid_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_integration_with_empty_guid_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<ArgumentException>(() => integrationRepository.Delete(Guid.Empty));
                }
            }

            [Fact]
            public void DeleteIntegrationInDatabase_ExpectsIntegrationNonExistentAfterDeletion()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Delete_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Insert(integration);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Delete(integration.IntegrationGuid);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<InvalidOperationException>(() => integrationRepository.Find(integration.IntegrationGuid));
                }
            }
        }

        public class Find
        {
            [Fact]
            public void FindIntegrationInDatabase_ExpectsToFindCorrectIntegration()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Insert(integration);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegration = integrationRepository.Find(integration.IntegrationGuid);
                    Equal(foundIntegration.Guid, integration.Guid);
                }
            }

            [Fact]
            public void FindIntegrationWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_integration_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<ArgumentException>(() => integrationRepository.Find(Guid.Empty));
                }
            }

            [Fact]
            public void FindNonExistentIntegrationInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    Throws<InvalidOperationException>(() => integrationRepository.Find(integration.Guid));
                }
            }
        }

        public class FindAsync
        {
            [Fact]
            public async void FindAsyncIntegrationInDatabase_ExpectsToFindCorrectIntegration()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrationRepository.Insert(integration);
                    context.SaveChanges();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegration = await integrationRepository.FindAsync(integration.IntegrationGuid);
                    Equal(foundIntegration.Guid, integration.Guid);
                }
            }

            [Fact]
            public async void FindAsyncIntegrationWithEmptyGuidInDatabase_ExpectsArgumentException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_integration_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    await ThrowsAsync<ArgumentException>(async () => await integrationRepository.FindAsync(Guid.Empty));
                }
            }

            [Fact]
            public async void FindAsyncNonExistentIntegrationInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "FindAsync_nonexistant_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    await ThrowsAsync<InvalidOperationException>(async () => await integrationRepository.FindAsync(integration.Guid));
                }
            }
        }

        public class FindBy
        {

            [Fact]
            public void FindByIntegrationWithEmptyGuidInDatabase_ExpectsFindByReturnsNoIntegrations()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_integration_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = GetIntegrations(5);
                    integrations.ForEach(integrationRepository.Insert);
                    integrationRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = integrationRepository.FindBy(integration => integration.Guid == Guid.Empty, string.Empty);
                    Equal(0, integrations.Count());
                }
            }

            [Fact]
            public void FindNonExistentIntegrationsByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = integrationRepository.FindBy(x => x.IntegrationGuid == integration.Guid, string.Empty);
                    Equal(0, integrations.Count());
                }
            }
        }

        public class FindByAsync
        {

            [Fact]
            public async void FindByAsyncIntegrationWithEmptyGuidInDatabase_ExpectsNoIntegrations()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_integration_with_empty_guid_in_database").Options;
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = GetIntegrations(5);
                    integrations.ForEach(integrationRepository.Insert);
                    integrationRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var integrations = await integrationRepository.FindByAsync(integration => integration.Guid == Guid.Empty, string.Empty);
                    Equal(0, integrations.Count());
                }
            }

            [Fact]
            public async void FindByAsyncNonExistentIntegrationByFilterInDatabase_ExpectsInvalidOperationException()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "Find_nonexistant_integration_in_database").Options;
                var integration = Integration.Builder().SetIntegrationGuid(Guid.NewGuid()).Build();
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = await integrationRepository.FindByAsync(x => x.IntegrationGuid == integration.Guid, string.Empty);
                    Equal(0, foundIntegrations.Count());
                }
            }
        }

        public class Save
        {
            [Fact]
            public void SaveIntegrations_ExpectsToFindExactAmountOfIntegrations()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_integrations_in_database").Options;
                var integrations = GetIntegrations(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrations.ForEach(integrationRepository.Insert);
                    integrationRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = integrationRepository.FindBy(null, string.Empty);
                    Equal(integrations.Count, foundIntegrations.Count());
                }
            }

            [Fact]
            public void SaveSpecificAmountOfIntegrations_ExpectsToNotFindDifferentAmount()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "save_integrations_in_database").Options;
                var integrations = GetIntegrations(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrations.ForEach(integrationRepository.Insert);
                    integrationRepository.Save();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = integrationRepository.FindBy(null, string.Empty);
                    var foundIntegrationsCount = foundIntegrations.Count();
                    NotEqual(integrations.Count - 1, foundIntegrationsCount);
                    NotEqual(integrations.Count + 1, foundIntegrationsCount);
                }
            }
        }

        public class SaveAsync
        {
            [Fact]
            public async void SaveAsyncIntegrations_ExpectsToFindExactAmountOfIntegrations()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "saveasync_integrations_in_database_expects_to_find_exact_amount").Options;
                var integrations = GetIntegrations(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrations.ForEach(integrationRepository.Insert);
                    await integrationRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = integrationRepository.FindBy(null, string.Empty);
                    Equal(integrations.Count, foundIntegrations.Count());
                }
            }

            [Fact]
            public async void SaveAsyncSpecificAmountOfIntegrations_ExpectsToNotFindDifferentAmountOfIntegrations()
            {
                var options = new DbContextOptionsBuilder<EsportshubContext>().UseInMemoryDatabase(databaseName: "savesync_integrations_in_database_not_to_find_different_amount").Options;
                var integrations = GetIntegrations(7);
                using (var context = new EsportshubContext(options))
                {
                    context.Database.EnsureDeleted();
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    integrations.ForEach(integrationRepository.Insert);
                    await integrationRepository.SaveAsync();
                }

                using (var context = new EsportshubContext(options))
                {
                    var internalRepository = new InternalRepository<Integration>(context);
                    var integrationRepository = new IntegrationRepository(internalRepository);
                    var foundIntegrations = integrationRepository.FindBy(null, string.Empty);
                    var foundIntegrationsCount = foundIntegrations.Count();
                    NotEqual(integrations.Count - 1, foundIntegrationsCount);
                    NotEqual(integrations.Count + 1, foundIntegrationsCount);
                }
            }
        }
    }
}
