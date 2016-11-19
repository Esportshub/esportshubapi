using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities.mappings;
using RestfulApi.Models.Validator;

namespace EsportshubApi.Models.Entities
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

        public Group Build(IValidator<Group> validator)
        {
            throw new NotImplementedException();
        }

        public Group Build(GroupValidator validator)
        {
            validator.Validate(_group);
            return _group;
        }

        public IGroupBuilder SetPlayerGroups(List<PlayerGroups> input)
        {
           _group.PlayerGroups = input;
           return this;
        }

        public IGroupBuilder SetCreated(DateTime input)
        {
            _group.Created = input;
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

        public IGroupBuilder SetRole(Role input)
        {
            _group.Role = input;
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