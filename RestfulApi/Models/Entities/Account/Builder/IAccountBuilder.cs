using System;
using System.Collections.Generic;
using Patterns.Builder;

namespace Models.Entities 
{
    public interface IAccountBuilder : IBuilder<Account>
    {
        IAccountBuilder AccountId(int input);   
        IAccountBuilder SetAccountGuid(Guid input);
        IAccountBuilder SetEmail(string input);    
        IAccountBuilder SetSalt(string input);     
        IAccountBuilder Verified(bool input);   
        IAccountBuilder SetChecksum(string input); 
        IAccountBuilder SetPassword(string input); 
        IAccountBuilder SetCreated(DateTime input);
        IAccountBuilder SetUpdated(DateTime input);
        IAccountBuilder SetPlayer(Player input);   
    }

}