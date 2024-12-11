namespace FootballClubApp
{
    using System;
    using System.Collections.Generic;

    public class Match
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Score { get; set; }

        public ICollection<MatchTeam> MatchTeams { get; set; }
    }

    public class MatchTeam
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
