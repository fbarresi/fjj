﻿using System;
using System.Reflection;
using fjj.Services.Commands;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services
{
	[Command(Name = "fjj", Description = "A simple file based job journal"),
	 Subcommand(typeof(StartCommand), typeof(StopCommand), typeof(ProjectCommand), typeof(ActivityCommand))]
	public class CommandService
	{
		private int OnExecute(CommandLineApplication app, IConsole console)
		{
			app.ShowHelp();

			console.WriteLine();
			console.ForegroundColor = ConsoleColor.Red;
			console.WriteLine("You must specify a subcommand.");

			return 1;
		}
	}
}