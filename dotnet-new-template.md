# Create a `dotnet new` template

What if you want to share a Minimal API template with your teammates or create a starter template for your own reuse?  Augmenting `dotnet new` with an additional template is a simple, easy way to do just that.

In the repo, you will notice that there is a `.template.config` folder at the root of the project that contains a single `template.json` file.  The contents of this file defines metadata that will be used during the creation of a `dotnet new` template.

To install a new template, navigate to the root of the API project (meaning, one level above the .template.config folder) and run the following command:

`dotnet new --install .\`

>NOTE: If you are running on OSX the command would look like this: `dotnet new --install ./`

A new template will now exist in `dotnet new` that will contain the contents of the current API project.  You can check that the template has been installed by executing `dotnet new --list`.  You should see a template named **minimalapistarter** (as it was named in the template.json file).

If you would like to test out using the template, navigate to an empty folder and execute the following command:

`dotnet new minimalapistarter`

## Template Parameters

An example of utilizing custom template parameters is included in the `template.json` file.  In the template, you will find a "symbol" named "templateEndpoint" that is defined.  

You will see this symbol utilized in Program.cs here:

`app.MapGet("/{templateEndpoint}-from-template", (ILogger<WebApplication> log) => ...`

To adjust the value that lands in this templated location, execute the dotnet new command as follows:

`dotnet new minimalapistarter --templateEndpoint awesomeName`

This will result in the following adjustment when using `dotnet new` to create an instance of the template:

`app.MapGet("/awesomeName-from-template", (ILogger<WebApplication> log) => ...`


Congratulations!  You now have a templatized project you can share with teammates and re-use.

## Additional Resources

- [Tutorial: Create a project template](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template)
- [Custom templates for dotnet new (includes an example of packing a template into a NuGet package)](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates#packing-a-template-into-a-nuget-package-nupkg-file)
- [Blog post from the community by PatridgeDev: Add variables to your custom 'dotnet new' template](https://www.patridgedev.com/2018/10/22/add-variables-to-your-custom-dotnet-new-template/)

## Next

Lets take full advantage of exposing an OpenAPI specification and see what it takes to [generate a client SDK](generate-client-sdk.md) for the API.
