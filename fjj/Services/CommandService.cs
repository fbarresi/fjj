using System;
using System.Reflection;
using fjj.Services.Commands;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services
{
	[Command(Name = "fjj", Description = "A simple file based job journal"),
	 Subcommand(typeof(StartCommand), typeof(StopCommand), typeof(ProjectCommand), typeof(ActivityCommand))]
	[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
	public class CommandService
	{
		private int OnExecute(CommandLineApplication app, IConsole console)
		{
			app.ShowHelp();

			console.WriteLine();
			console.WriteLine("You have to specify a subcommand.");

			return 1;
		}
		public static string GetVersion()
			=> typeof(CommandService).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
	}
}