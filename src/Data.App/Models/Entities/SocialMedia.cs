using System;

namespace Data.App.Models.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; private set; }

        public Guid SocialMediaGuid { get; private set; }

        public Integration Integration { get; set; }

        public string Name { get; set; }

    }
}