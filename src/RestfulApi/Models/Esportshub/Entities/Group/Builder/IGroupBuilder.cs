using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub.Entities.Group.Validator;
using RestfulApi.Models.Esportshub.Entities.GroupPlayer;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Patterns.Builder;

namespace  RestfulApi.Models.Esportshub.Entities.Group.Builder
{
    public interface IGroupBuilder : IBuilder<Group, GroupValidator>
    {
        IGroupBuilder SetGroupGuid(Guid input);
        IGroupBuilder SetName(string input);
        IGroupBuilder SetVisibilty(Visibilty input);
        IGroupBuilder SetRole(Role input);
        IGroupBuilder SetPlayerGroups(List<PlayerGroups> input);
        IGroupBuilder SetGroupEvents(List<GroupEvent> input);

    }

}