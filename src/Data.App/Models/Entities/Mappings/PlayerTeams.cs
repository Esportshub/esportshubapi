namespace Data.App.Models.Entities.Mappings
{
    public class PlayerTeams
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public Player Player { get; set; }
        public Team Team { get; set; }
    }
}