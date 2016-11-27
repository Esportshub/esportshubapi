using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities.mappings;
using Patterns.Builder;

namespace  EsportshubApi.Models.Entities
{
    public interface IGroupBuilder : IBuilder<Group>
    {
        IGroupBuilder SetGroupId(int input);
        IGroupBuilder SetGroupGuid(Guid input);
        IGroupBuilder SetName(string input);
        IGroupBuilder SetCreated(DateTime input);
        IGroupBuilder SetUpdated(DateTime input);
        IGroupBuilder SetVisibilty(Visibilty input);
        IGroupBuilder SetRole(Role input);
        IGroupBuilder SetPlayerGroups(List<PlayerGroups> input);
        IGroupBuilder SetGroupEvents(List<GroupEvent> input);

    }

}