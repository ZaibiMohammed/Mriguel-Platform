using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Application.Common.Models;
using AlloVoisinClone.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Items.Queries.GetItems
{
    /// <summary>
    /// Query to get a list of items
    /// </summary>
    public record GetItemsQuery : IRequest<PaginatedList<ItemSummaryDto>>
    {
        public string? SearchTerm { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public double? Distance { get; init; }
        public Guid? CategoryId { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SortBy { get; init; }
        public bool SortAscending { get; init; } = true;
    }
    
    /// <summary>
    /// Handler for getting a list of items
    /// </summary>
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PaginatedList<ItemSummaryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PaginatedList<ItemSummaryDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Items
                .Include(i => i.Owner)
                .Include(i => i.Images)
                .Include(i => i.Categories)
                    .ThenInclude(c => c.Category)
                .Where(i => i.Status == ItemStatus.Published)
                .AsQueryable();
                
            // Apply filters
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(i => i.Title.ToLower().Contains(searchTerm) || 
                                        i.Description.ToLower().Contains(searchTerm));
            }
            
            if (request.CategoryId.HasValue)
            {
                var categoryId = request.CategoryId.Value;
                query = query.Where(i => i.Categories.Any(c => c.CategoryId == categoryId));
            }
            
            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                var startDate = request.StartDate.Value;
                var endDate = request.EndDate.Value;
                
                query = query.Where(i => i.Availabilities.Any(a => 
                    a.StartDate <= startDate && a.EndDate >= endDate));
                    
                query = query.Where(i => !i.Rentals.Any(r => 
                    r.Status != RentalStatus.Cancelled && 
                    r.Status != RentalStatus.Declined &&
                    r.StartDate <= endDate && 
                    r.EndDate >= startDate));
            }
            
            if (request.MinPrice.HasValue)
            {
                var minPrice = request.MinPrice.Value;
                query = query.Where(i => i.DailyPrice.Amount >= minPrice);
            }
            
            if (request.MaxPrice.HasValue)
            {
                var maxPrice = request.MaxPrice.Value;
                query = query.Where(i => i.DailyPrice.Amount <= maxPrice);
            }
            
            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "price" => request.SortAscending 
                    ? query.OrderBy(i => i.DailyPrice.Amount)
                    : query.OrderByDescending(i => i.DailyPrice.Amount),
                "rating" => request.SortAscending 
                    ? query.OrderBy(i => i.AverageRating)
                    : query.OrderByDescending(i => i.AverageRating),
                "popularity" => request.SortAscending 
                    ? query.OrderBy(i => i.ViewCount)
                    : query.OrderByDescending(i => i.ViewCount),
                "created" => request.SortAscending 
                    ? query.OrderBy(i => i.Created)
                    : query.OrderByDescending(i => i.Created),
                _ => query.OrderByDescending(i => i.Created) // Default to most recent
            };
            
            return await query
                .ProjectTo<ItemSummaryDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
