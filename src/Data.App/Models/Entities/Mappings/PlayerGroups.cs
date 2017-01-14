﻿namespace Data.App.Models.Entities.Mappings
{
    public class PlayerGroups
    {
        public int PlayerId { get; set; }
        public int GroupId { get; set; }

        public Player Player { get; set; }
        public Group Group { get; set; }

    }
}