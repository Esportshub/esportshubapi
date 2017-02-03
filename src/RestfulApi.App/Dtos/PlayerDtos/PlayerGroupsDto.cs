using System;
using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public sealed class PlayerGroupsDto
    {
        public Guid PlayerGuid { get; set; }
        public Guid GroupGuid { get; set; }
        public PlayerDto Player { get; set; }
        public GameDto Game { get; set; }

    }
}