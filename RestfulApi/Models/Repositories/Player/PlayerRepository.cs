using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Models.Repositories;
using Models.Entities;

namespace Models.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(EsportshubContext context) : base(context)
        {
        }
    }
}