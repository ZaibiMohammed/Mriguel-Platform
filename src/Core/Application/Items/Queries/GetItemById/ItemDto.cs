using Mriguel.Application.Common.Mappings;
using Mriguel.Domain.Entities;
using Mriguel.Domain.Enums;
using AutoMapper;

namespace Mriguel.Application.Items.Queries.GetItemById
{
    /// <summary>
    /// Data transfer object for an item
    /// </summary>
    public class ItemDto : IMapFrom<Item>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MoneyDto DailyPrice { get; set; } = null!;
        public MoneyDto? SecurityDeposit { get; set; }
        public ItemOwnerDto Owner { get; set; } = null!;
        public ItemStatus Status { get; set; }
        public LocationDto Location { get; set; } = null!;
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public int ViewCount { get; set; }
        public List<ItemImageDto> Images { get; set; } = new();
        public List<ItemCategoryDto> Categories { get; set; } = new();
        public List<ItemAvailabilityDto> Availabilities { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, ItemDto>()
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images))
                .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Categories))
                .ForMember(d => d.Availabilities, opt => opt.MapFrom(s => s.Availabilities));
        }
    }
    
    /// <summary>
    /// Data transfer object for an item's owner
    /// </summary>
    public class ItemOwnerDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, ItemOwnerDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"));
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
    
    /// <summary>
    /// Data transfer object for an item image
    /// </summary>
    public class ItemImageDto : IMapFrom<ItemImage>
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsCoverImage { get; set; }
    }
    
    /// <summary>
    /// Data transfer object for an item category
    /// </summary>
    public class ItemCategoryDto : IMapFrom<ItemCategory>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemCategory, ItemCategoryDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
    
    /// <summary>
    /// Data transfer object for an item availability
    /// </summary>
    public class ItemAvailabilityDto : IMapFrom<ItemAvailability>
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
