using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using Mriguel.Domain.Entities.Identity;
using MediatR;

namespace Mriguel.Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Command to create a new user
    /// </summary>
    public record CreateUserCommand : IRequest<string>
    {
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
    
    /// <summary>
    /// Handler for creating a new user
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        
        public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }
        
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Create identity user
            var (userId, _, errors) = await _identityService.CreateUserAsync(
                request.Email,
                request.Email,
                request.Password);
                
            if (errors.Any())
            {
                throw new Exception($"Error creating user: {string.Join(", ", errors)}");
            }
            
            // Create domain user
            var user = new User(
                request.Email,
                request.FirstName,
                request.LastName,
                new ApplicationUser { Id = userId });
                
            // Update profile with phone number
            user.UpdateProfile(request.FirstName, request.LastName, null, request.PhoneNumber, null, null);
            
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return userId;
        }
    }
}
