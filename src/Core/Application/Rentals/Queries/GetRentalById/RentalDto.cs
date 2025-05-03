using Mriguel.Application.Common.Mappings;
using Mriguel.Domain.Entities;
using Mriguel.Domain.Enums;
using AutoMapper;

namespace Mriguel.Application.Rentals.Queries.GetRentalById
{
    /// <summary>
    /// Data transfer object for a rental
    /// </summary>
    public class RentalDto : IMapFrom<Rental>
    {
        public Guid Id { get; set; }
        public RentalItemDto Item { get; set; } = null!;
        public RentalUserDto Renter { get; set; } = null!;
        public RentalUserDto Owner { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentalStatus Status { get; set; }
        public MoneyDto TotalPrice { get; set; } = null!;
        public MoneyDto? SecurityDeposit { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? CancellationReason { get; set; }
        public string? DeclineReason { get; set; }
        public List<RentalMessageDto> Messages { get; set; } = new();
        public List<PaymentDto> Payments { get; set; } = new();
        public DateTime Created { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rental, RentalDto>()
                .ForMember(d => d.Item, opt => opt.MapFrom(s => s.Item))
                .ForMember(d => d.Renter, opt => opt.MapFrom(s => s.Renter))
                .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.Owner))
                .ForMember(d => d.Messages, opt => opt.MapFrom(s => s.Messages))
                .ForMember(d => d.Payments, opt => opt.MapFrom(s => s.Payments));
        }
    }
    
    /// <summary>
    /// Data transfer object for a rental item
    /// </summary>
    public class RentalItemDto : IMapFrom<Item>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MoneyDto DailyPrice { get; set; } = null!;
        public string CoverImageUrl { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new();
        
        private string GetCoverImageUrl(Item item)
        {
            var coverImage = item.Images.FirstOrDefault(i => i.IsCoverImage);
            if (coverImage != null && coverImage.Url != null)
                return coverImage.Url;
                
            var firstImage = item.Images.FirstOrDefault();
            if (firstImage != null && firstImage.Url != null)
                return firstImage.Url;
                    
            return string.Empty;
        }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, RentalItemDto>()
                .ForMember(d => d.CoverImageUrl, opt => opt.MapFrom<string>((src, dest, destMember, context) => GetCoverImageUrl(src)))
                .ForMember(d => d.ImageUrls, opt => opt.MapFrom(s => 
                    s.Images.Select(i => i.Url).ToList()));
        }
    }
    
    /// <summary>
    /// Data transfer object for a rental user
    /// </summary>
    public class RentalUserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, RentalUserDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        }
    }
    
    /// <summary>
    /// Data transfer object for a rental message
    /// </summary>
    public class RentalMessageDto : IMapFrom<RentalMessage>
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderProfilePictureUrl { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RentalMessage, RentalMessageDto>()
                .ForMember(d => d.SenderId, opt => opt.MapFrom(s => s.SenderId))
                .ForMember(d => d.SenderName, opt => opt.MapFrom(s => $"{s.Sender.FirstName} {s.Sender.LastName}"))
                .ForMember(d => d.SenderProfilePictureUrl, opt => opt.MapFrom(s => s.Sender.ProfilePictureUrl));
        }
    }
    
    /// <summary>
    /// Data transfer object for a payment
    /// </summary>
    public class PaymentDto : IMapFrom<Payment>
    {
        public Guid Id { get; set; }
        public PaymentType Type { get; set; }
        public PaymentStatus Status { get; set; }
        public MoneyDto Amount { get; set; } = null!;
        public string TransactionId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
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
}
