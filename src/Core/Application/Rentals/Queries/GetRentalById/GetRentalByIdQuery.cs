using Mriguel.Application.Common.Exceptions;
using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Application.Rentals.Queries.GetRentalById
{
    /// <summary>
    /// Query to get a rental by ID
    /// </summary>
    public record GetRentalByIdQuery : IRequest<RentalDto>
    {
        public Guid Id { get; init; }
    }
    
    /// <summary>
    /// Handler for getting a rental by ID
    /// </summary>
    public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, RentalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public GetRentalByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<RentalDto> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
        {
            var rental = await _context.Rentals
                .Include(r => r.Item)
                    .ThenInclude(i => i.Images)
                .Include(r => r.Owner)
                .Include(r => r.Renter)
                .Include(r => r.Messages)
                .Include(r => r.Payments)
                .Where(r => r.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
                
            if (rental == null)
            {
                throw new NotFoundException(nameof(Rental), request.Id);
            }
            
            // Check if the current user is either the owner or the renter
            var userId = _currentUserService.UserId;
            if (rental.OwnerId.ToString() != userId && rental.RenterId.ToString() != userId)
            {
                throw new ForbiddenAccessException();
            }
            
            return _mapper.Map<RentalDto>(rental);
        }
    }
}
