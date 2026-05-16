# No1.EnvBasedEndpoints

With EnvBasedEndpoints, you can enable or disable specific endpoints or controllers based on specific environments!

## Usage

#### Register EnvironmentControllerFeatureProvider

At the beginning add below code in your `program.cs`:

```cs
using No1.EnvBasedEndpoints;	// import this
//...
var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;	// get application's running environment
//...
builder.Services.AddControllers().ConfigureApplicationPartManager(x => EnvironmentControllerFeatureProvider.Register(x, environment));
```

Now you can use different Attributes based on your requirements:

### [NonProduction]

Imagine you need to place a Controller for testing reasons in your app, but you do not want it to be present on production.

It is simple in spring and spring boot. You can add that service conditionally, but in asp.net, there is not such conditionally annotations.

So, you can apply [NonProduction] attribute from this library and then that endpoint exists anywhere but on Production:

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

### [ProductionOnly]

Controllers and endpoints that have this attribute, will be available only in Production environment

### [StagingOnly]

Controllers and endpoints that have this attribute, will be available only in Staging environment

### [DevelopmentOnly]

Controllers and endpoints that have this attribute, will be available only in Development environment


### [EnvironmentAttribute]

This attribute takes two lists. First list of included environments and second, list of excluded environments. Applied controller or endpoint, will be available only if the application is being run under any included envs (if there was any) and none of excluded envs (if there was any)

```cs
[Route("LocalOnly/Test")]
[ApiController]
[Environment([], ["Production"])] // Controller will not published on production
public class TestController : ControllerBase
{
	[HttpGet]
	public void ThrowException() => throw new Exception("Exception Message");
}
```


### Building.

For enabling Husky.NET run:

```
dotnet new tool-manifest
dotnet tool install Husky
git add .husky/pre-commit
```

After this, Husky formats the code in staged files based on .editorconfig configurations just before commit.