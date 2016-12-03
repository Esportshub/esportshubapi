using System;
using Moq;
using RestfulApi.App.Models.Repositories.Players;

namespace RestfulApi.Tests.Repositories
{
    public class PlayerRepositoryTest : IDisposable
    {
        private Mock<IPlayerRepository> _playerRepository;


        void IDisposable.Dispose()
        {
           _playerRepository = null;
        }
    }
}