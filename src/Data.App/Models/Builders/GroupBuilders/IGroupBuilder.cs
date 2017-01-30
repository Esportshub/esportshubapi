using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace  Data.App.Models.Builders.GroupBuilders
{
    public interface IGroupBuilder : IBuilder<Group>
    {
        IGroupBuilder SetGroupGuid(Guid groupGuid);
        IGroupBuilder SetName(string name);
        IGroupBuilder SetVisibilty(Group.Visibilties visibility);
        IGroupBuilder SetRole(Group.Roles role);
        IGroupBuilder SetPlayerGroups(List<PlayerGroups> playerGroups);
        IGroupBuilder SetEsportshubEvents(List<EsportshubEvent> esportshubEvents);
    }

}