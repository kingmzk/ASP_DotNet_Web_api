namespace ASP_DotNet_Web_api.Models.domain
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        // Navigation properties
        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }
    }
}

//Navigation for diificultyId and RegionId -->(Because they are foreign Key)
//telling Entity framework to navigate it is smart and helps in migration  . Walk will have Difficulty and REgion