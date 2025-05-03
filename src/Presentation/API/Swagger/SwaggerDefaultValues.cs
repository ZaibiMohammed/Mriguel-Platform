using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mriguel.API.Swagger
{
    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document information provided, but not used.
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            // Consider adding authorization if needed
            // operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            // operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            // Set operation ID to make it more readable
            operation.OperationId = apiDescription.TryGetMethodInfo(out var methodInfo)
                ? methodInfo.Name
                : null;

            // Remove parameters that are marked as obsolete
            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions
                    .FirstOrDefault(p => p.Name == parameter.Name);

                if (description == null)
                {
                    continue;
                }

                // Set default values if any
                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    // Convert the default value to an appropriate IOpenApiAny type
                    if (description.DefaultValue is int intValue)
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiInteger(intValue);
                    }
                    else if (description.DefaultValue is string stringValue)
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiString(stringValue);
                    }
                    else if (description.DefaultValue is bool boolValue)
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiBoolean(boolValue);
                    }
                    // Add more type conversions as needed
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
