using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {

            AddSamurai();
            GetSamurais("After Add:");
            InsertMultipleSamurais();
            QueryFilters();
            RetrieveAndUpdateSamurai();
            RetrieveAndUpdateMultipleSamurais();
            MultipleDatabaseOperations();
            RetrieveAndDeleteASamurai();
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
        private static void RetrieveAndDeleteASamurai()
        {
            var samurai = _context.Samurais.Find(18);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
    }
}
