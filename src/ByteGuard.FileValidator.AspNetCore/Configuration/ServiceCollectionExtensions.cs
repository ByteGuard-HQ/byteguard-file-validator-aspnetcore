using ByteGuard.FileValidator.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ByteGuard.FileValidator.AspNetCore.Configuration
{
    /// <summary>
    /// Service collection extensions for adding the file validator service.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the file validator service with the specified configuration.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="config">Wanted configuration (see <see cref="FileValidatorConfiguration"/> for possible options).</param>
        public static IServiceCollection AddFileValidator(this IServiceCollection services, Action<FileValidatorConfiguration> config)
        {
            // Add and validate configuration options.
            services.AddSingleton<IValidateOptions<FileValidatorConfiguration>,
                FileValidatorConfigurationOptionsValidator>();

            services.AddOptions<FileValidatorConfiguration>()
                .Configure(config)
                .PostConfigure(options =>
                {
                    if (options.FileSizeLimit == -1 && !string.IsNullOrWhiteSpace(options.FriendlyFileSizeLimit))
                    {
                        options.FileSizeLimit = ByteSize.Parse(options.FriendlyFileSizeLimit);
                    }
                })
                .ValidateOnStart();

            // Register file validator service
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IOptions<FileValidatorConfiguration>>().Value;
                return new FileValidator(configuration);
            });

            return services;
        }
    }
}
