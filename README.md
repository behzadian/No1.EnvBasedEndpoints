# No1.EnvBasedApi

With EnvBsedApi, you can enable or disable specific endpoints or controllers based on specific environments!

## Usage

### NonProduction

Imagine you need to place a Controller for testing reasons in your app, but you do not want it to be present on production.

It is simple in spring and spring boot. You can add that service conditionally, but in asp.net, there is not such conditionally annotations.

So, you can apply [NonProduction] attribute from this library and then that endpoint exists anywhere exception on Production:

```cs
[Route("LocalOnly/Test")]
[ApiController]
[NonProduction]
public class TestController : ControllerBase
{
	[HttpGet]
	public void ThrowException() => throw new Exception("Exception Message");
}
```

There is another step to make this work. Add below line in your `program.cs` before any other middleware:

```cs
using No1.EnvBasedApi;
//...
var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
//...
builder.Services.AddControllers().ConfigureApplicationPartManager(x => EnvironmentControllerFeatureProvider.Register(x, environment));
```


### Building.

For enabling Husky.NET run:

```
dotnet new tool-manifest
dotnet tool install Husky
git add .husky/pre-commit
```

After this, Husky formats the code in staged files based on .editorconfig configurations just before commit.