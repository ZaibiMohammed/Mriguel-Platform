using Mriguel.Application.Items.Commands.CreateItem;
using Mriguel.Application.Items.Commands.PublishItem;
using Mriguel.Application.Items.Commands.UpdateItem;
using Mriguel.Application.Items.Queries.GetItemById;
using Mriguel.Application.Items.Queries.GetItems;
using Mriguel.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mriguel.API.Controllers
{
    /// <summary>
    /// Controller for item management
    /// </summary>
    public class ItemsController : ApiControllerBase
    {
        /// <summary>
        /// Get items with pagination and filtering
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ItemSummaryDto>>> GetItems([FromQuery] GetItemsQuery query)
        {
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Get an item by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            return await Mediator.Send(new GetItemByIdQuery { Id = id });
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Create(CreateItemCommand command)
        {
            var id = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetItem), new { id }, id);
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(Guid id, UpdateItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Publish an item
        /// </summary>
        [HttpPost("{id}/publish")]
        [Authorize]
        public async Task<ActionResult> Publish(Guid id)
        {
            await Mediator.Send(new PublishItemCommand { Id = id });

            return NoContent();
        }
    }
}
