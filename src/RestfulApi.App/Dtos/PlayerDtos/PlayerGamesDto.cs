using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerGamesDto
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public PlayerDto Player { get; set; }
        public GameDto Game { get; set; }
    }
}