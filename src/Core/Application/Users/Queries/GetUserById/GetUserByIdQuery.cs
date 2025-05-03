using Mriguel.Application.Common.Exceptions;
using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Application.Users.Queries.GetUserById
{
    /// <summary>
    /// Query to get a user by ID
    /// </summary>
    public record GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; init; }
    }
    
    /// <summary>
    /// Handler for getting a user by ID
    /// </summary>
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Addresses)
                .Include(u => u.Verifications)
                .Include(u => u.Items.Where(i => i.Status == Domain.Enums.ItemStatus.Published))
                .Where(u => u.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
                
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            
            return _mapper.Map<UserDto>(user);
        }
    }
}
