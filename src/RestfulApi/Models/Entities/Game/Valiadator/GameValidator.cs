using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EsportshubApi.Models.Entities;

namespace RestfulApi.Models.Entities.Game.Valiadator
{
    public class GameValidator : IGameValidator
    {

        public void Validate(Game game)
        {
          ValidateCreated(game.Created);
          ValidateGameGuid(game.GameGuid);
          ValidateGameId(game.GameId);
          ValidateName(game.Name);
          ValidateUpdated(game.Updated);          
        }

        public void ValidateCreated(DateTime input)
        {
            var s = DateTime.Now;
            if (input.Date != DateTime.Now.Date)
                throw new ArgumentException("Created at is not valid");
        }


        public void ValidateGameGuid(Guid input)
        {
            if (input == Guid.Empty)
                throw new ArgumentException("GUID cannot be empty");
        }

        public void ValidateGameId(int input)
        {
            if (input <= 0)
                throw new ArgumentException("Id is not valid");
        }

        public void ValidateName(string input)
        {

            if (input.Length <= 2)
                throw new ArgumentException("Name is to short");
        }

        public void ValidateUpdated(DateTime input)
        {
            if (DateTime.Now.Date != input.Date)
                throw new ArgumentException("Update at is not valid");
        }
    }
}