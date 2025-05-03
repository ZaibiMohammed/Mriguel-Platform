using Mriguel.Application.Common.Interfaces;

namespace Mriguel.Infrastructure.Services
{
    /// <summary>
    /// Service for datetime operations
    /// </summary>
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
