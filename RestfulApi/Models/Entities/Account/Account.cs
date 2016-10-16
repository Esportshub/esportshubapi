using System;
using Extensions.Entities;

namespace Models.Entities
{
    public class Account
    {
        private Account() { }

        public static AccountBuilder Builder()
        {
            return new AccountBuilder(new Account());
        }

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


        public override bool Equals(object obj)
        {
            Account account = (Account)obj;

            if (this.CompareEntities(obj))
                return account.AccountId == this.AccountId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}