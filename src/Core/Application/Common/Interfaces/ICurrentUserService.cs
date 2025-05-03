namespace Mriguel.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for the current user service
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the current user's ID
        /// </summary>
        string? UserId { get; }
        
        /// <summary>
        /// Determines if the current user is authenticated
        /// </summary>
        bool IsAuthenticated { get; }
    }
}
