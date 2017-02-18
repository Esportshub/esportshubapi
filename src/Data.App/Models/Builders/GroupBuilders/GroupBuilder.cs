using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.GroupBuilders
{
    public class GroupBuilder : IGroupBuilder
    {
        private readonly Group _group;

        public GroupBuilder (Group group)
        {
          _group = group;
        }

        public Group Build()
        {
            return _group;
        }


        public IGroupBuilder SetPlayerGroups(List<PlayerGroups> playerGroups)
        {
           _group.PlayerGroups = playerGroups;
           return this;
        }

        public IGroupBuilder SetEsportshubEvents(List<EsportshubEvent> esportshubEvents)
        {
            _group.EsportshubEvents = esportshubEvents;
            return this;
        }

        public IGroupBuilder SetGroupGuid(Guid groupGuid)
        {
           _group.GroupGuid = groupGuid;
           return this;
        }

        public IGroupBuilder SetName(string name)
        {
            _group.Name = name;
            return this;
        }

        public IGroupBuilder SetRole(Group.Roles role)
        {
            _group.Role = role;
            return this;
        }

        public IGroupBuilder SetVisibilty(Group.Visibilties visibility)
        {
            _group.Visibilty = visibility;
            return this;
        }
    }
}