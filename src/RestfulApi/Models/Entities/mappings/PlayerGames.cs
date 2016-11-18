namespace EsportshubApi.Models.Entities.mappings
{
    public class PlayerGames
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}