var root = args.ElementAtOrDefault(0) ?? Directory.GetCurrentDirectory();
Console.WriteLine($"Hosting from: {root}");
var app = WebApplication.CreateSlimBuilder(new WebApplicationOptions { Args = args, ContentRootPath = root }).Build();
var fileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(root);
app.UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider, ServeUnknownFileTypes = true });
app.UseDirectoryBrowser(new DirectoryBrowserOptions { FileProvider = fileProvider });
app.Run();
