using System.ComponentModel.DataAnnotations;

namespace ASP_DotNet_Web_api.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        public string Code { get; set; }

        [Required]

        [MaxLength(50, ErrorMessage = "Code has to be maximum of 50 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
