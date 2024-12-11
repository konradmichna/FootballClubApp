namespace FootballClubApp
{
    using System.Collections.Generic;

    public class League
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
