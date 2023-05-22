using System.ComponentModel.DataAnnotations;

namespace ASP_DotNet_Web_api.Models.DTO
{
    public class UpdateWalkRequestDto
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }


    }
}
