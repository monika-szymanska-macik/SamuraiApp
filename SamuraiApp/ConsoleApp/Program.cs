using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {

            //AddSamurai();
            //GetSamurais("After Add:");
            //InsertMultipleSamurais();
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //MultipleDatabaseOperations();
            //InsertNewSamuraiWithAQuote();
            //InsertNewSamuraiWithMayQuotes();
            //AddQuoteToExistingSamuraiWhileTracked();
            ////RetrieveAndDeleteASamurai();
            //AddQuoteToExistingSamuraiNotTracked(2);
            //AddQuoteToExistingSamuraiNotTracked_Easy(2);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraiWithHappyQuotes();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai1 = new Samurai { Name = "Tomek" };
            var samurai2 = new Samurai { Name = "Adam" };
            var samurai3 = new Samurai { Name = "Kuba" };
            var samurai4 = new Samurai { Name = "Filip" };
            _context.Samurais.AddRange(samurai1,samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Monika" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamuraisSimpler()
        {
            var samurais = _context.Samurais.ToList();
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void QueryFilters()
        {
            var name = "Monika";
            var samurais = _context.Samurais.FirstOrDefault(s => s.Name == name );
        }
        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }
        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Kikuchiypo" });
            _context.SaveChanges();
        }
        ////private static void RetrieveAndDeleteASamurai()
        //{
        //    var samurai = _context.Samurais.Find(18);
        //    _context.Samurais.Remove(samurai);
        //    _context.SaveChanges();
        //}
        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazma",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }
        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using(var newContextInstace = new SamuraiContext())
            {
                newContextInstace.Battles.Update(battle);
                newContextInstace.SaveChanges();
            }
        }
        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimaba",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "I've come to save yu"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();       
        }
        private static void InsertNewSamuraiWithMayQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kambei",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "Watch out for my sharp sword!"},
                    new Quote {Text = "I've come to save yu"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, you will feed me deinner?"
            });
            using(var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        } 
        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me dinner again?",
                SamuraiId = samuraiId
            };
            using(var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
        }
        private static void ProjectSamuraiWithHappyQuotes()
        {
            var samuraiWithHappyQuote = _context.Samurais.Select(s => new { Samurais = s, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) }).ToList();
           }
        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }
        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            samurai.Quotes[0].Text = "Did you hear it ?";
            _context.SaveChanges();
        }
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = samurai.Quotes[0];
            quote.Text += "Did you hear it?";
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Update(quote);
                newContext.SaveChanges();
            }
        }
        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }
        private static void EnlistSamuraiIntoABattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles
                  .Add(new SamuraiBattle { SamuraiId = 21 });
            _context.SaveChanges();
        }
        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 2 };
            _context.Remove(join);
            _context.SaveChanges();
        }
        private static void GetSamuraiWithBattles()
        {

            var samuraiWithBattle = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(s => s.Id == 2);
        }
        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiUsingId(int id)
        {
            var horse = new Horse { Name = "Flesh", SamuraiId = 2 };
            _context.Add(horse);
            _context.SaveChanges();
        }
        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);
            var horseWithSamurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Horse.Id == 3);
            var horsesWthSamurai = _context.Samurais
                .Where(s => s.Horse != null)
                .Select(s => new { Horse = s.Horse, Samurai = s })
                .ToList();
        }
        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clan).FirstOrDefault();
        }
        private static void GetClanWithSamura()
        {
            var clan = _context.Clans.Find(3);
            var samuraiForClan = _context.Samurais.Where(s => s.Clan.Id == 3).ToList();
        }
    }
}
