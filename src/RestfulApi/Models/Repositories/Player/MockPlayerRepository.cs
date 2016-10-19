using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Models;

namespace EsportshubApi.Models.Repositories
{
    public class MockPlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public MockPlayerRepository(EsportshubContext context) : base(context){}

         public virtual IEnumerable<Player> Get(Expression<Func<Player, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Player> query = dbSet;

            if (filter != null) 
                query = query.Where(filter);

            /**@TODO: foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }*/
                return query.ToList();
        }

        public virtual Player GetByID(object id)
        {

           // Player player = new PlayerBuilder().Nickname("denlillemand").Build();

            return dbSet.Single( x => x.Equals(id));
        }

        public virtual void Insert(Player player)
        {
            dbSet.Add(player);
        }

        public virtual void Delete(object id)
        {
            Player entityToDelete = dbSet.Single(x => x.Equals(id));
            Delete(entityToDelete);
        }

        public virtual void Delete(Player playerToDelete)
        {
            if (context.Entry(playerToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(playerToDelete);
            }
            dbSet.Remove(playerToDelete);
        }

        public virtual void Update(Player entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}