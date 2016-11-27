namespace RestfulApi.App.Models.Esportshub.Entities.Mappings
{
    public class PlayerGames
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}