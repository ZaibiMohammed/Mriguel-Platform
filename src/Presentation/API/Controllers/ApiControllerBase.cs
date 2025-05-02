using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mriguel.API.Controllers
{
    /// <summary>
    /// Base controller for API endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
