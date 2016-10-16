using System;
using System.Collections.Generic;
using Models.Entites;
using Patterns.Builder;

namespace Models.Entities
{
    public interface IGroupBuilder : IBuilder<Group>
    {
        IGroupBuilder SetGroupId(int input);
        IGroupBuilder SetGroupGuid(Guid input);
        IGroupBuilder SetName(string input);
        IGroupBuilder SetCreated(DateTime input);
        IGroupBuilder SetUpdated(DateTime input);
        IGroupBuilder SetVisibilty(Visibilty input);
        IGroupBuilder SetRoles(List<Role> input);
        IGroupBuilder Players(List<Player> input);
        IGroupBuilder SetEvents(List<Event> input);

    }

}