namespace NZWalksAPI.Models.DTO
{
    public class RegionDto
    {
        //i want to expose everything back to the client 
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
