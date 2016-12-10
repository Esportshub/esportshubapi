using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public class PlayerGroupsDto
    {
        public int PlayerId { get; set; }
        public int GroupId { get; set; }
        public PlayerDto PlayerDto { get; set; }
        public GameDto GameDto { get; set; }

    }
}