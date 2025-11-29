using ByteGuard.FileValidator.Configuration;
using Microsoft.Extensions.Options;

namespace ByteGuard.FileValidator.AspNetCore.Configuration
{
    /// <summary>
    /// File validator configuration options validator for use with the options pattern.
    /// </summary>
    public class FileValidatorConfigurationOptionsValidator : IValidateOptions<FileValidatorConfiguration>
    {
        /// <inheritdoc />
        public ValidateOptionsResult Validate(string? name, FileValidatorConfiguration config)
        {
            try
            {
                ConfigurationValidator.ThrowIfInvalid(config);
                return ValidateOptionsResult.Success;
            }
            catch (Exception ex)
            {
                return ValidateOptionsResult.Fail(ex.Message);
            }
        }
    }
}

/* TODO: Add custom configuration class specific to appsettings configuration
 * 
 *   /// <summary>
 *   /// Maximum file size limit in string representation (e.g. "25MB", "2 GB", etc.).
 *   /// </summary>
 *   /// <remarks>
 *   /// Defines the file size limit of files. See <see cref="ByteSize"/> for conversion help.
 *   /// Will be ignored if <see cref="FileSizeLimit"/> is defined.
 *   /// Spacing (<c>"25 MB"</c> vs. <c>"25MB"</c>) is irrelevant.
 *   /// <para>Supported string representation are:
 *   /// <ul>
 *   /// <li><c>B</c>: Bytes</li>
 *   /// <li><c>KB</c>: Kilobytes</li>
 *   /// <li><c>MB</c>: Megabytes</li>
 *   /// <li><c>GB</c>: Gigabytes</li>
 *   /// </ul>
 *   /// </para>
 *   /// </remarks>
 *   public string FriendlyFileSizeLimit { get; set; } = default!;
 */
