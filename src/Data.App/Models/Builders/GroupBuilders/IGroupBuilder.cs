using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Entities.Mappings;

namespace  Data.App.Models.Builders.GroupBuilders
{
    public interface IGroupBuilder : IBuilder<Group>
    {
        IGroupBuilder SetGroupGuid(Guid input);
        IGroupBuilder SetName(string input);
        IGroupBuilder SetVisibilty(Group.Visibilties input);
        IGroupBuilder SetRole(Group.Roles input);
        IGroupBuilder SetPlayerGroups(List<PlayerGroups> input);
        IGroupBuilder SetGroupEvents(List<GroupEvent> input);
    }

}