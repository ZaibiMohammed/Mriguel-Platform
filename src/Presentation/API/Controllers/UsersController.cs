using AlloVoisinClone.Application.Users.Commands.CreateUser;
using AlloVoisinClone.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlloVoisinClone.API.Controllers
{
    /// <summary>
    /// Controller for user management
    /// </summary>
    public class UsersController : ApiControllerBase
    {
        /// <summary>
        /// Get a user by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            return await Mediator.Send(new GetUserByIdQuery { Id = id });
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            var userId = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }
    }
}
