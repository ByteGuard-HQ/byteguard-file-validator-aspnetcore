# ByteGuard.FileValidator.AspNetCore ![NuGet Version](https://img.shields.io/nuget/v/ByteGuard.FileValidator.AspNetCore)

`ByteGuard.FileValidator.AspNetCore` provides first-class integration of `ByteGuard.FileValidator` with ASP.NET Core.

It gives you:
- Extension methods to register the file validator in the DI container
- Easy configuration via appsettings.json or fluent configuration in code
- A single, consistent way to validate uploaded files across your ASP.NET Core apps

> This package is the ASP.NET Core integration layer.
> The core validation logic lives in [ByteGuard.FileValidator](https://github.com/ByteGuard-HQ/byteguard-file-validator-net).

## Getting Started

### Installation
This package is published and installed via NuGet.

Reference the package in your project:
```bash
dotnet add package ByteGuard.FileValidator.AspNetCore
```

## Usage

### Add to DI container
In your `Program.cs` (or `Startup.cs` in older projects), register the validator:

```csharp
using ByteGuard.FileValidator.AspNetCore;

// Using inline configuration
builder.Services.AddFileValidator(options => 
{
    options.AllowFileTypes(FileExtensions.Pdf, FileExtensions.Jpg, FileExtensions.Png);
    options.FileSizeLimit = ByteSize.MegaBytes(25);
    options.ThrowOnInvalidFiles(false);
});

// Using configuration from appsettings.json
builder.Services.AddFileValidator(options => configuration.GetSection("FileValidatorConfiguration").Bind(options));
```

### Injection & Usage
You can then inject `FileValidator` into your services and other classes.

```csharp
public class MyService
{
    private readonly FileValidator _fileValidator;

    public MyService(FileValidator fileValidator)
    {
        _fileValidator = fileValidator;
    }

    public bool SaveFile(Stream fileStream, string fileName)
    {
        var isValid = _fileValidator.IsValidFile(fileName, fileStream);
        
        // ...
    }
}
```

### Configuration via appsettings
It's possible to configure the `FileValidator` through `appsettings.json`.

> _ℹ️ As you'll notice below, you can either define the `FileSizeLimit` in raw byte size, or use the `FriendlyFileSizeLimit` to define
> the file size in a more human readable format. When both are defined, `FileSizeLimit` always wins over `FriendlyFileSizeLimit`._

```json
{
    "FileValidatorConfiguration": {
        "SupportedFileTypes": [ ".pdf", ".jpg", ".png" ],
        "FileSizeLimit": 26214400, // (25 MB) Prefer "FriendlyFileSizeLimit"
        "FriendlyFileSizeLimit": "25MB", // Preferred over "FileSizeLimit" (supports KB, MB, and GB)
        "ThrowExceptionOnInvalidFile": true
    }
}
```

## License
_ByteGuard.FileValidator.AspNetCore is Copyright © ByteGuard Contributors - Provided under the MIT license._