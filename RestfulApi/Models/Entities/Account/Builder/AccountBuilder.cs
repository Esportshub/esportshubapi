using System;
namespace Models.Entities
{
    public class AccountBuilder : IAccountBuilder
    {
        private Account _account;
        public AccountBuilder(Account account)
        {
            _account = account;
        }
        public Account Build()
        {
            return _account;
        }

        public IAccountBuilder AccountId(int input)
        {
            _account.AccountId = input;
            return this;
        }


        public IAccountBuilder SetAccountGuid(Guid input)
        {
            _account.AccountGuid = input;
            return this;
        }

        public IAccountBuilder SetChecksum(string input)
        {
            _account.Checksum = input;
            return this;
        }

        public IAccountBuilder SetCreated(DateTime input)
        {
            _account.Created = input;
            return this;
        }

        public IAccountBuilder SetEmail(string input)
        {
            _account.Email = input;
            return this;
        }

        public IAccountBuilder SetPassword(string input)
        {
            _account.Password  = input;
            return this;
        }

        public IAccountBuilder SetPlayer(Player input)
        {
            _account.Player = input;
            return this;
        }

        public IAccountBuilder SetSalt(string input)
        {
            _account.Salt = input;
            return this;
        }

        public IAccountBuilder SetUpdated(DateTime input)
        {
            _account.Updated = input;
            return this;
        }

        public IAccountBuilder Verified(bool input)
        {
            _account.Verified = input;
            return this;
        }
    }
}