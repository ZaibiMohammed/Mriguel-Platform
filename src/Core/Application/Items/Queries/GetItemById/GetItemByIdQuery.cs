using Mriguel.Application.Common.Exceptions;
using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Application.Items.Queries.GetItemById
{
    /// <summary>
    /// Query to get an item by ID
    /// </summary>
    public record GetItemByIdQuery : IRequest<ItemDto>
    {
        public Guid Id { get; init; }
    }
    
    /// <summary>
    /// Handler for getting an item by ID
    /// </summary>
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetItemByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items
                .Include(i => i.Owner)
                .Include(i => i.Images)
                .Include(i => i.Categories)
                    .ThenInclude(c => c.Category)
                .Include(i => i.Availabilities)
                .Include(i => i.Reviews)
                .Where(i => i.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
                
            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }
            
            // Increment view count
            item.IncrementViewCount();
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<ItemDto>(item);
        }
    }
}
