using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.AccountDtos;
using RestfulApi.App.Dtos.ActivitiesDtos;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }

        public Guid PlayerGuid { get; set; }

        public string Nickname { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ApplicationUserDto ApplicationUser { get; set; }
        public string AccountForeignKey { get; set; }

        public List<PlayerDto> Followers { get; set; }

        public List<PlayerGamesDto> PlayerGames { get; set; }

        public List<PlayerTeamsDto> PlayerTeams { get; set; }
        public List<IntegrationDto> Integrations { get; set; }
        public List<ActivityDto> Activities { get; set; }

        public List<PlayerGroupsDto> PlayerGroups { get; set; }
    }
}