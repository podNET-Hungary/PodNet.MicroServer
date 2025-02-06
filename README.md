# PodNet.MicroServer
A simple, small file server, written in 7 lines of code 🤯

# Features
- Can serve any file from the given directory (or the server executables directory)
- Includes directory browser
- Set the URLs you want to listen on including the IP addresses and ports
- Do not use this in production or on public servers, there are no warranties

# Download
- [win-x64 (self-contained, 6.68MB)](https://github.com/podNET-Hungary/PodNet.MicroServer/releases/download/v1.0.0/PodNet.MicroServer.win-x64.exe)

# Run
```ps1
### These commands expect the .exe to be named "PodNet.MicroServer.exe"

# Serves files from the current directory (including the server executable itself)
.\PodNet.MicroServer.exe

# Serves files from the current directory (if the server is at another, relative location)
..\Tools\PodNet.MicroServer.exe

# Serves files from the current directory (if the server is at another, absolute location)
# Note that this syntax is specific to PowerShell, every shell has its own syntax for calling a path with spaces in it
& "C:\Program Files\PodNet.MicroServer.exe"

# Serves files from the current directory (if the server is at a location available from PATH)

# Serves files from the provided directory
.\PodNet.MicroServer.exe "C:\Users\YourSelf\Desktop"

# Serves files from the provided directory on port 3100 on the local machine (only available from the serving machine)
.\PodNet.MicroServer.exe "C:\Users\YourSelf\Desktop" --urls http://localhost:3100

# Serves files from the provided directory on port 80 on *every available IP address*.
# Careful, this opens up communication outside your PC on your IP address as well!
.\PodNet.MicroServer.exe "C:\Users\YourSelf\Desktop" --urls http://*:80
```

# Build
- Install the .NET 9 SDK
- `dotnet build`

# Publish
`dotnet publish`

By default, every [documented optimization](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/optimizing) for native AOT is used.

You can do cross-publishing by providing the appropriate [runtime ID](https://learn.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli#self-contained-deployment).

# Reference
For reference, here's the full source for v1:
```cs
var root = args.ElementAtOrDefault(0) ?? Directory.GetCurrentDirectory();
Console.WriteLine($"Hosting from: {root}");
var app = WebApplication.CreateSlimBuilder(new WebApplicationOptions { Args = args, ContentRootPath = root }).Build();
var fileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(root);
app.UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider, ServeUnknownFileTypes = true });
app.UseDirectoryBrowser(new DirectoryBrowserOptions { FileProvider = fileProvider });
app.Run();
```
