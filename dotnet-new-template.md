# Create a `dotnet new` template

What if you want to share a Minimal API template with your teammates or create a starter template for your own reuse?  Augmenting `dotnet new` with an additional template is a simple, easy way to do just that.

In the repo, you will notice that there is a `.template.config` folder at the root of the project that contains a single `template.json` file.  The contents of this file defines metadata that will be used during the creation of a `dotnet new` template.

To install a new template, navigate to the root of the API project (meaning, one level above the .template.config folder) and run the following command:

`dotnet new --install .\`

>NOTE: If you are running on OSX the command would look like this: `dotnet new --install ./`

A new template will now exist in `dotnet new` that will contain the contents of the current API project.  You can check that the template has been installed by executing `dotnet new --list`.  You should see a template named **minimalapistarter** (as it was named in the template.json file).

If you would like to test out using the template, navigate to an empty folder and execute the following command:

`dotnet new minimalapistarter`

Congratulations!  You now have a templatized project you can share with teammates and re-use.

## Additional Resources

- [Tutorial: Create a project template](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template)

## Next

Lets take full advantage of exposing an OpenAPI specification and see what it takes to [generate a client SDK](generate-client-sdk.md) for the API.
