namespace FootballClubApp
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public DateTime Founded { get; set; }

        public ICollection<Player> Players { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public ICollection<MatchTeam> MatchTeams { get; set; }
    }
}
