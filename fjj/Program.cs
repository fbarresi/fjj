using System;
using fjj.Interfaces;
using fjj.Services;
using Microsoft.Extensions.DependencyInjection;
using McMaster.Extensions.CommandLineUtils;


namespace fjj
{
    public class Program
    {
	    public static int Main(string[] args)
	    {
		    var services = new ServiceCollection()
			    .AddSingleton<IDbService>(new DbService())
			    .AddSingleton<IConsole>(PhysicalConsole.Singleton)
			    .BuildServiceProvider();

		    var app = new CommandLineApplication<CommandService>();
		    app.Conventions
			    .UseDefaultConventions()
			    .UseConstructorInjection(services);
		    return app.Execute(args);
	    }
    }
}
