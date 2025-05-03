namespace Mriguel.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for identity service
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Gets the user name for the specified user ID
        /// </summary>
        Task<string?> GetUserNameAsync(string userId);
        
        /// <summary>
        /// Creates a user with the specified user name, email, and password
        /// </summary>
        Task<(string UserId, string UserName, IEnumerable<string> Errors)> CreateUserAsync(string userName, string email, string password);
        
        /// <summary>
        /// Determines if a user is in the specified role
        /// </summary>
        Task<bool> IsInRoleAsync(string userId, string role);
        
        /// <summary>
        /// Authorizes a user against a set of policies
        /// </summary>
        Task<bool> AuthorizeAsync(string userId, string policyName);
        
        /// <summary>
        /// Deletes a user with the specified ID
        /// </summary>
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId);
    }
}
