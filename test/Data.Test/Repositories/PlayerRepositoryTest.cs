using System;

namespace Data.Test.Repositories
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