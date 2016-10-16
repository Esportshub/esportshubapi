using System;
using System.Collections.Generic;
using Models.Entities;
using Patterns.Builder;

namespace Models.Entities 
{
    public class PlayerBuilder : IPlayerBuilder
    {
        Account IPlayerBuilder.Account
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Activity> IPlayerBuilder.Activities
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Player> IPlayerBuilder.Followers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Game> IPlayerBuilder.Games
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Group> IPlayerBuilder.Groups
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Integration> IPlayerBuilder.Integrations
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IPlayerBuilder.Nickname
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        Guid IPlayerBuilder.PlayerGuid
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        int IPlayerBuilder.PlayerId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<Team> IPlayerBuilder.Teams
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        Player IBuilder<Player>.Build()
        {
            throw new NotImplementedException();
        }
    }
}