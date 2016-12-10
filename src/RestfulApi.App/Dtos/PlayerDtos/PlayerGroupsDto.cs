using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerGroupsDto
    {
        public int PlayerId { get; set; }
        public int GroupId { get; set; }
        public PlayerDto Player { get; set; }
        public GameDto Game { get; set; }

    }
}