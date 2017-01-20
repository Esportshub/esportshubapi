using System.Collections.Generic;
using Data.App.Models.Entities;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public sealed class PlayersDto
    {
        public List<PlayerDto> Players { get; set; }

    }
}