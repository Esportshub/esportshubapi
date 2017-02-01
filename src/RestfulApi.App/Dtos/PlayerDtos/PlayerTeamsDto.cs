using System;
using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public sealed class PlayerTeamsDto
    {
        public Guid PlayerGuid { get; set; }
        public Guid TeamGuid { get; set; }
        public PlayerDto Player { get; set; }
        public TeamDto Team { get; set; }

    }
}