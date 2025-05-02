using AlloVoisinClone.Application.Rentals.Commands.CreateRental;
using AlloVoisinClone.Application.Rentals.Commands.UpdateRentalStatus;
using AlloVoisinClone.Application.Rentals.Queries.GetRentalById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlloVoisinClone.API.Controllers
{
    /// <summary>
    /// Controller for rental management
    /// </summary>
    [Authorize]
    public class RentalsController : ApiControllerBase
    {
        /// <summary>
        /// Get a rental by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(Guid id)
        {
            return await Mediator.Send(new GetRentalByIdQuery { Id = id });
        }
        
        /// <summary>
        /// Create a new rental request
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateRentalCommand command)
        {
            var id = await Mediator.Send(command);
            
            return CreatedAtAction(nameof(GetRental), new { id }, id);
        }
        
        /// <summary>
        /// Accept a rental request
        /// </summary>
        [HttpPost("{id}/accept")]
        public async Task<ActionResult> Accept(Guid id)
        {
            await Mediator.Send(new AcceptRentalCommand { Id = id });
            
            return NoContent();
        }
        
        /// <summary>
        /// Decline a rental request
        /// </summary>
        [HttpPost("{id}/decline")]
        public async Task<ActionResult> Decline(Guid id, [FromBody] DeclineRentalCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            
            await Mediator.Send(command);
            
            return NoContent();
        }
    }
}
