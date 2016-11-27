using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;

namespace RestfulApi.App.Models.Esportshub.Builders.GroupBuilders
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


        public IGroupBuilder SetPlayerGroups(List<PlayerGroups> input)
        {
           _group.PlayerGroups = input;
           return this;
        }

        public IGroupBuilder SetGroupEvents(List<GroupEvent> input)
        {
            _group.GroupEvents = input;
            return this;
        }

        public IGroupBuilder SetGroupGuid(Guid input)
        {
           _group.GroupGuid = input;
           return this;
        }

        public IGroupBuilder SetName(string input)
        {
            _group.Name = input;
            return this;
        }

        public IGroupBuilder SetRole(Group.Roles input)
        {
            _group.Role = input;
            return this;
        }

        public IGroupBuilder SetVisibilty(Group.Visibilties input)
        {
            _group.Visibilty = input;
            return this;
        }
    }
}