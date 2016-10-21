using System.Collections.Generic;
using RestfulApi.Models.Validator;

namespace RestfulApi.Models.Entities.Game.Valiadator
{
    public interface IGameValidator: IValidator<Game>
    {
        void ValidateGameId(int input);
        void ValidateName(string input);
        void ValidateGameGuid(Guid input);
        void ValidateCreated(DateTime input);
        void ValidateUpdated(DateTime input);
    }
}