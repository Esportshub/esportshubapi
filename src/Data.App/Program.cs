using System;
using Data.App.Models.Esportshub;
using Data.App.Models.Repositories.Players;
using Microsoft.EntityFrameworkCore;

namespace Data.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new EsportshubContext())
            {
                var context = new PlayerRepository(db);
                db.EnsureSeedData();


                foreach (var player in db.Players.AsNoTracking())
                {
                    Console.WriteLine("Name: " + player.Nickname);
                    Console.WriteLine("Type: " + player.Created);
                }
            }
        }
    }
}