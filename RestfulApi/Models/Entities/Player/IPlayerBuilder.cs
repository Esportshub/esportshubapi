using System;
using System.Collections.Generic;
using Patterns.Builder;

namespace Models.Entities 
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        int PlayerId { get; set; }
        Guid PlayerGuid { get; set; }
        string Nickname { get; set; }
        Account Account { get; set; }
        List<Player> Followers { get; set; }
        List<Game> Games { get; set; }
        List<Team> Teams { get; set; }
        List<Integration> Integrations { get; set; }
        List<Activity> Activities { get; set; }
        List<Group> Groups { get; set; }
    }

}