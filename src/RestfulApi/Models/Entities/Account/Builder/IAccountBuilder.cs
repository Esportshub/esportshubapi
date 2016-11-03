using System;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IAccountBuilder : IBuilder<Account, AccountValidator>
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