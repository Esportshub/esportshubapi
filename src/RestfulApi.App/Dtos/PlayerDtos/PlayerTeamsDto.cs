using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerTeamsDto
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public PlayerDto Player { get; set; }
        public TeamDto Team { get; set; }

    }
}