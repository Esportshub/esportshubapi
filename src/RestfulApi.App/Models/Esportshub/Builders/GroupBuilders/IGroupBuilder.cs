using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;

namespace  RestfulApi.App.Models.Esportshub.Builders.GroupBuilders
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