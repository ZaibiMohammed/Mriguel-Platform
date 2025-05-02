using AlloVoisinClone.Application.Common.Mappings;
using AlloVoisinClone.Domain.Entities;
using AlloVoisinClone.Domain.Enums;
using AutoMapper;

namespace AlloVoisinClone.Application.Items.Queries.GetItems
{
    /// <summary>
    /// Data transfer object for an item summary
    /// </summary>
    public class ItemSummaryDto : IMapFrom<Item>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public MoneyDto DailyPrice { get; set; } = null!;
        public string CoverImageUrl { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerProfilePictureUrl { get; set; } = string.Empty;
        public LocationDto Location { get; set; } = null!;
        public List<string> Categories { get; set; } = new();
        public DateTime Created { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, ItemSummaryDto>()
                .ForMember(d => d.CoverImageUrl, opt => opt.MapFrom(s => 
                    s.Images.FirstOrDefault(i => i.IsCoverImage)?.Url ?? 
                    s.Images.FirstOrDefault()?.Url ?? string.Empty))
                .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => $"{s.Owner.FirstName} {s.Owner.LastName}"))
                .ForMember(d => d.OwnerProfilePictureUrl, opt => opt.MapFrom(s => s.Owner.ProfilePictureUrl))
                .ForMember(d => d.Categories, opt => opt.MapFrom(s => 
                    s.Categories.Select(c => c.Category.Name).ToList()));
        }
    }
    
    /// <summary>
    /// Data transfer object for money
    /// </summary>
    public class MoneyDto : IMapFrom<Domain.ValueObjects.Money>
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string FormattedValue { get; set; } = string.Empty;
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ValueObjects.Money, MoneyDto>()
                .ForMember(d => d.FormattedValue, opt => opt.MapFrom(s => $"{s.Amount} {s.Currency}"));
        }
    }
    
    /// <summary>
    /// Data transfer object for location
    /// </summary>
    public class LocationDto : IMapFrom<Domain.ValueObjects.Location>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
