using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.EventsDtos;
using RestfulApi.App.Dtos.GameDtos;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.TeamDtos
{

    public class TeamDto
    {

        public int TeamId { get;set; }

        public string Name { get; set; }

        public Guid TeamGuid { get; set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        public GameDto Game { get; set; }

        public List<TeamEventDto> TeamEventsDtos { get; set; }

        public List<PlayerTeamsDto> PlayerTeamsDtos { get; set; }
    }
}