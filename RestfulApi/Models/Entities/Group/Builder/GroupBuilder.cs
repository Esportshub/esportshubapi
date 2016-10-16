using System;
using System.Collections.Generic;
using Models.Entites;

namespace Models.Entities
{
    public class GroupBuilder : IGroupBuilder
    {
        public Group Build()
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder Players(List<Player> input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetCreated(DateTime input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetEvents(List<Event> input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetGroupGuid(Guid input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetGroupId(int input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetName(string input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetRoles(List<Role> input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetUpdated(DateTime input)
        {
            throw new NotImplementedException();
        }

        public IGroupBuilder SetVisibilty(Visibilty input)
        {
            throw new NotImplementedException();
        }
    }
}