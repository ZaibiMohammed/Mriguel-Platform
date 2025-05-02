using Mriguel.Application.Common.Interfaces;
using Mriguel.SignalR.Hubs;
using Mriguel.SignalR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mriguel.SignalR
{
    /// <summary>
    /// Extension methods for configuring SignalR services
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Adds SignalR services to the DI container
        /// </summary>
        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<INotificationService, NotificationService>();
            
            return services;
        }
        
        /// <summary>
        /// Maps SignalR hubs to endpoints
        /// </summary>
        public static WebApplication UseSignalRHubs(this WebApplication app)
        {
            app.MapHub<ChatHub>("/chatHub");
            app.MapHub<NotificationHub>("/notificationHub");
            
            return app;
        }
    }
}
