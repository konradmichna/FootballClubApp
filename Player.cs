namespace FootballClubApp
{
    using System;

    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
