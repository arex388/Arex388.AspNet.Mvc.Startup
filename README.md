# Arex388.AspNet.Mvc.Startup

This is a classic ASP.NET MVC helper library that started out as a way for me to use the `Microsoft.Extensions.DependencyInjection` NuGet package for dependency injection. As I worked on it, it dawned on me that with a little bit more nudging it can simulate ASP.NET Core's `Startup.cs`.

For a more detailed story about why this library came to be, please read my corresponding [blog post here](https://arex388.com/blog/introducing-arex388-aspnet-mvc-startup-nuget-package-to-simulate-aspnet-cores-startupcs-in-classic-aspnet-mvc-applications). I want to thank David Fowler and Scott Dorman for their code examples which I've taken, adapted, and mostly just glued together.

- [David Fowler's Gist](https://gist.github.com/davidfowl/563a602936426a18f67cd77088574e61) example code.
- [Scott Dorman's Blog](https://scottdorman.blog/2016/03/17/integrating-asp-net-core-dependency-injection-in-mvc-4/) example code.
- The [StackOverflow question](https://stackoverflow.com/questions/43311099/how-to-create-dependency-injection-for-asp-net-mvc-5) that lead me to David and Scott.

I also want to thank [Keith Dahlby](https://github.com/dahlbyk) for helping improve the library.



## How to Use

1. Add the [Arex388.AspNet.Mvc.Startup](https://www.nuget.org/packages/Arex388.AspNet.Mvc.Startup/) NuGet package.
2. Change your `Global.asax.cs` to inherit from `StartupApplication`.
3. Add `[assembly: OwinStartup(typeof(YourNamespace.MvcApplication))]` attribute to the namespace of your `Global.asax.cs`.
4. Implement the `Configure` and `ConfigureServices` methods inherited from `StartupApplication`.
5. Add `ConfigureServices()` to the end of `Application_Start`.

Here's a more complete example:

```
[assembly: OwinStartup(typeof(YourNamespace.MvcApplication))]
namespace YourNamespace {
    public class MvcApplication :
        StartupApplication {
		public override Configure(
			IAppBuilder app) {
			//	Add IAppBuilder configurations
        }

        public override void ConfigureServices(
            IServiceCollection services) {
            var assembly = typeof(MvcApplication).Assembly;

            //	Add your controllers
            services.AddControllers(assembly);

            //	Add other services that have IServiceCollection extensions
        }
    }
}
```



## Changes

#### v1.0.6

- **Added** `GetServiceProvider()` extension on `IAppBuilder`. This extension provides access to the `IServiceProvider` when working with services in the `Configure()` method.
- **Reverted** back all dependencies on `Microsoft.Extensions.DependencyInjection` to v2.1.1. You can continue to use the latest (v5.0.2 as of this writing) with binding redirects.



## Things to Consider

~~The only annoyance I've had so far is with Hangfire's `IServiceCollection` extensions because they're in the `Hangfire.AspNetCore` NuGet package and that forces a bunch of other NuGet packages to be added in. I can live with it for the simplicity in configuration and dependency registrations I get otherwise.~~ Using `Hangfire.NetCore` package instead simplified this issue.