using Microsoft.Extensions.DependencyInjection;

namespace Log.Parser.BL.Extensions
{
    public static class CorsPolicyHandler
    {
        /// <summary>
        /// A Hanlder used in the Program.cs to apply CORS policy to prevent unauthorized app access
        /// Only origins passed to this method will be able to access the APIs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="origins"></param>
        /// <returns></returns>
        public static IServiceCollection UseCorsPolicyHandler(this IServiceCollection services, string[] origins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
