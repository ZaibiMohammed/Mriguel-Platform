using AlloVoisinClone.Application.Common.Mappings;
using AlloVoisinClone.Domain.Entities;
using AlloVoisinClone.Domain.Enums;
using AutoMapper;

namespace AlloVoisinClone.Application.Users.Queries.GetUserById
{
    /// <summary>
    /// Data transfer object for a user
    /// </summary>
    public class UserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public List<AddressDto> Addresses { get; set; } = new();
        public List<VerificationDto> Verifications { get; set; } = new();
        public List<UserItemDto> Items { get; set; } = new();
        public DateTime Created { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Addresses, opt => opt.MapFrom(s => s.Addresses))
                .ForMember(d => d.Verifications, opt => opt.MapFrom(s => s.Verifications))
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items));
        }
    }
    
    /// <summary>
    /// Data transfer object for an address
    /// </summary>
    public class AddressDto : IMapFrom<Address>
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string FormattedAddress { get; set; } = string.Empty;
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>()
                .ForMember(d => d.FormattedAddress, opt => opt.MapFrom(s => 
                    $"{s.Street}, {s.City}, {s.State} {s.ZipCode}, {s.Country}"));
        }
    }
    
    /// <summary>
    /// Data transfer object for a verification
    /// </summary>
    public class VerificationDto : IMapFrom<UserVerification>
    {
        public Guid Id { get; set; }
        public VerificationType Type { get; set; }
        public DateTime VerifiedAt { get; set; }
    }
    
    /// <summary>
    /// Data transfer object for a user's item
    /// </summary>
    public class UserItemDto : IMapFrom<Item>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, UserItemDto>()
                .ForMember(d => d.CoverImageUrl, opt => opt.MapFrom(s => 
                    s.Images.FirstOrDefault(i => i.IsCoverImage)?.Url ?? 
                    s.Images.FirstOrDefault()?.Url ?? string.Empty));
        }
    }
}
