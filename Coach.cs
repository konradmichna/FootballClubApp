namespace FootballClubApp
{
    using System.Collections.Generic;

    public class Coach
    {
        public int CoachId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearsOfExperience { get; set; }
        public string License { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
