namespace Mriguel.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for date time provider
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Gets the current UTC date and time
        /// </summary>
        DateTime Now { get; }
    }
}
