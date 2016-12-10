using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.ActivitiesDtos;
using RestfulApi.App.Dtos.ApplicationUserDtos;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerDto
    {
 public int PlayerId { get; set; }

        public Guid PlayerDtoGuid { get; set; }

        public string Nickname { get; set; }

        public DateTime Created { get;  set; }
        public DateTime Updated { get;  set; }

        public ApplicationUserDto ApplicationUserDto { get; set; }
        public string AccountForeignKeyDto { get; set; }

        public List<PlayerDto> FollowersDto { get; set; }

        public List<PlayerGamesDto> PlayerGamesDto { get; set; }

        public List<PlayerTeamsDto> PlayerTeamsDtos { get; set; }
        public List<IntegrationDto> IntegrationsDtos { get; set; }
        public List<ActivityDto> ActivitiesDtos { get; set; }

        public List<PlayerGroupsDto> PlayerGroupsDtos { get; set; }

    }
}