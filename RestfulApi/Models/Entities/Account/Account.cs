using System;

namespace Models.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public bool Verified { get; set; }
        public string Checksum { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }
    }

}