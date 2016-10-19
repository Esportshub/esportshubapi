using System;
using System.Collections.Generic;
using Models.Entites;

namespace Models.Entities
{
    public class GroupBuilder : IGroupBuilder
    {
        private Group _group;

        public GroupBuilder (Group group)
        {
          _group = group;
        }
        public Group Build()
        {
            return _group;
        }

        public IGroupBuilder Players(List<Player> input)
        {
           _group.Players = input;
           return this;
        }

        public IGroupBuilder SetCreated(DateTime input)
        {
            _group.Created = input;
            return this;
        }

        public IGroupBuilder SetEvents(List<Event> input)
        {
            _group.Events = input;
            return this;
        }

        public IGroupBuilder SetGroupGuid(Guid input)
        {
           _group.GroupGuid = input;
           return this;
        }

        public IGroupBuilder SetGroupId(int input)
        {
            _group.GroupId = input;
            return this;
        }

        public IGroupBuilder SetName(string input)
        {
            _group.Name = input;
            return this;
        }

        public IGroupBuilder SetRoles(List<Role> input)
        {
            _group.Roles = input;
            return this;
        }

        public IGroupBuilder SetUpdated(DateTime input)
        {
            _group.Updated = input;
            return this;
        }

        public IGroupBuilder SetVisibilty(Visibilty input)
        {
            _group.Visibilty = input;
            return this;
        }
    }
}