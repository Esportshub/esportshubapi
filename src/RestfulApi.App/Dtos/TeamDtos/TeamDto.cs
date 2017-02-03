using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.EsportshubEventsDtos;
using RestfulApi.App.Dtos.GameDtos;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.TeamDtos
{

    public class TeamDto
    {
        public Guid TeamGuid { get; set; }

        public string Name { get; set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        public GameDto Game { get; set; }

        public List<EsportshubEventDto> EsportshubEvents { get; set; }

        public List<PlayerTeamsDto> PlayerTeams { get; set; }
    }
}