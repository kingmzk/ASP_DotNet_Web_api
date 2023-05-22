namespace ASP_DotNet_Web_api.Models.DTO
{
    //This REgion Will Have  Properties that we want to expose to our client
    //Dto can have subset of  domain model
    public class RegionDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
