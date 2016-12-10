using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.ActivitiesDtos
{
    public class ActivityDto
    {
        public int ActivityDtoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PlayerDto PlayerDto { get; set; }

    }
}