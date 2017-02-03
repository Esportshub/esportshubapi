using System;
using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public sealed class PlayerGamesDto
    {
        public Guid PlayerGuid { get; set; }
        public Guid GameGuId { get; set; }
        public PlayerDto Player { get; set; }
        public GameDto Game { get; set; }
    }
}