using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FootballClubApp
{
    class Program
    {
        static void Main(string[] args)
        {
        
            var options = new DbContextOptionsBuilder<FootballClubContext>()
                .UseSqlServer("Server=nazwa_serwera;Database=FootballClub;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            using var context = new FootballClubContext(options);

            context.Database.EnsureCreated();
            Console.WriteLine("Połączenie z bazą danych powiodło się!");
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nZarządzanie Klubem Piłkarskim");
                Console.WriteLine("1. Dodaj ligę");
                Console.WriteLine("2. Wyświetl ligi");
                Console.WriteLine("3. Zaktualizuj ligę");
                Console.WriteLine("4. Usuń ligę");
                Console.WriteLine("5. Dodaj drużynę");
                Console.WriteLine("6. Wyświetl drużyny");
                Console.WriteLine("7. Dodaj zawodnika");
                Console.WriteLine("8. Wyświetl zawodników");
                Console.WriteLine("9. Wyjście");
                Console.Write("Wybierz opcję: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddLeague(context);
                        break;
                    case "2":
                        ViewLeagues(context);
                        break;
                    case "3":
                        UpdateLeague(context);
                        break;
                    case "4":
                        DeleteLeague(context);
                        break;
                    case "5":
                        AddTeam(context);
                        break;
                    case "6":
                        ViewTeams(context);
                        break;
                    case "7":
                        AddPlayer(context);
                        break;
                    case "8":
                        ViewPlayers(context);
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static void AddLeague(FootballClubContext context)
        {
            Console.Write("Podaj nazwę ligi: ");
            var name = Console.ReadLine();

            Console.Write("Podaj kraj ligi: ");
            var country = Console.ReadLine();

            var league = new League { Name = name, Country = country };
            context.Leagues.Add(league);
            context.SaveChanges();

            Console.WriteLine("Liga została dodana pomyślnie.");
        }

        static void ViewLeagues(FootballClubContext context)
        {
            var leagues = context.Leagues.ToList();

            Console.WriteLine("\nLigi:");
            foreach (var league in leagues)
            {
                Console.WriteLine($"ID: {league.LeagueId}, Nazwa: {league.Name}, Kraj: {league.Country}");
            }
        }

        static void UpdateLeague(FootballClubContext context)
        {
            Console.Write("Podaj ID ligi, którą chcesz zaktualizować: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var league = context.Leagues.FirstOrDefault(l => l.LeagueId == id);

                if (league != null)
                {
                    Console.Write("Podaj nową nazwę: ");
                    league.Name = Console.ReadLine();

                    Console.Write("Podaj nowy kraj: ");
                    league.Country = Console.ReadLine();

                    context.SaveChanges();
                    Console.WriteLine("Liga została zaktualizowana pomyślnie.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono ligi o podanym ID.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID.");
            }
        }

        static void DeleteLeague(FootballClubContext context)
        {
            Console.Write("Podaj ID ligi, którą chcesz usunąć: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var league = context.Leagues.FirstOrDefault(l => l.LeagueId == id);

                if (league != null)
                {
                    context.Leagues.Remove(league);
                    context.SaveChanges();
                    Console.WriteLine("Liga została usunięta pomyślnie.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono ligi o podanym ID.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID.");
            }
        }

        static void AddTeam(FootballClubContext context)
        {
            Console.Write("Podaj nazwę drużyny: ");
            var name = Console.ReadLine();

            Console.Write("Podaj ID ligi: ");
            if (int.TryParse(Console.ReadLine(), out int leagueId))
            {
                var team = new Team { Name = name, LeagueId = leagueId };
                context.Teams.Add(team);
                context.SaveChanges();

                Console.WriteLine("Drużyna została dodana pomyślnie.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID ligi.");
            }
        }

        static void ViewTeams(FootballClubContext context)
        {
            var teams = context.Teams.Include(t => t.League).ToList();

            Console.WriteLine("\nDrużyny:");
            foreach (var team in teams)
            {
                Console.WriteLine($"ID: {team.TeamId}, Nazwa: {team.Name}, Liga: {team.League?.Name}");
            }
        }

        static void AddPlayer(FootballClubContext context)
        {
            Console.Write("Podaj imię zawodnika: ");
            var firstName = Console.ReadLine();

            Console.Write("Podaj nazwisko zawodnika: ");
            var lastName = Console.ReadLine();

            Console.Write("Podaj pozycję zawodnika: ");
            var position = Console.ReadLine();

            Console.Write("Podaj datę urodzenia zawodnika (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out var dateOfBirth))
            {
                Console.Write("Podaj ID drużyny: ");
                if (int.TryParse(Console.ReadLine(), out int teamId))
                {
                    var player = new Player
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Position = position,
                        DateOfBirth = dateOfBirth,
                        TeamId = teamId
                    };

                    context.Players.Add(player);
                    context.SaveChanges();
                    Console.WriteLine("Zawodnik został dodany pomyślnie.");
                }
                else
                {
                    Console.WriteLine("Nieprawidłowe ID drużyny.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy format daty.");
            }
        }

        static void ViewPlayers(FootballClubContext context)
        {
            var players = context.Players.Include(p => p.Team).ToList();

            Console.WriteLine("\nZawodnicy:");
            foreach (var player in players)
            {
                Console.WriteLine($"ID: {player.PlayerId}, Imię i nazwisko: {player.FirstName} {player.LastName}, " +
                                  $"Pozycja: {player.Position}, Data urodzenia: {player.DateOfBirth.ToShortDateString()}, " +
                                  $"Drużyna: {player.Team?.Name}");
            }
        }
    }
}
