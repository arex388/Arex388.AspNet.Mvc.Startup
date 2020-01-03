# Arex388.AspNet.Mvc.Startup

This is a classic ASP.NET MVC helper library that started out as a way for me to use the Microsoft.Extensions.DependencyInjection NuGet package for dependency injection. As I worked on it, it dawned on me that with a little bit more nudging it can simulate ASP.NET Core's `Startup.cs`.

For a more detailed story about why this library came to be, please read my corresponding [blog post here][3]. I want to thank David Fowler and Scott Dorman for their code examples which I've taken, adapted, and mostly just glued together.

- [David Fowler's Gist][0] example code.
- [Scott Dorman's Blog][1] example code.
- The [StackOverflow question][2] that lead me to David and Scott.

#### How to Use

1. Add the Arex388.AspNet.Mvc.Startup NuGet package.
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
        public void Application_Start() {
            //	Other setup and configuration code here...

            ConfigureServices();
        }

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

#### Things to Consider

The only annoyance I've had so far is with HangFire's `IServiceCollection` extensions because they're in the `HangFire.AspNetCore` NuGet package and that force's a bunch of other NuGet packages to be added in. I can live with it for the simplicity in configuration and dependency registrations I get otherwise.

[0]: https://gist.github.com/davidfowl/563a602936426a18f67cd77088574e61
[1]: https://scottdorman.blog/2016/03/17/integrating-asp-net-core-dependency-injection-in-mvc-4/
[2]: https://stackoverflow.com/questions/43311099/how-to-create-dependency-injection-for-asp-net-mvc-5
[3]: https://arex388.com/blog/introducing-arex388-aspnet-mvc-startup-nuget-package-to-simulate-aspnet-cores-startupcs-in-classic-aspnet-mvc-applications

