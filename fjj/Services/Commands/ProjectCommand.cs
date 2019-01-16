using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using fjj.Models;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	[Command(Name = "project", Description = "set current project")]
	public class ProjectCommand : TimeCommandBase
	{
		public ProjectCommand(IDbService dbService) : base(dbService, EntryType.Project)
		{
		}

		[Argument(0, Description = "your actual project")]
		public string Project { get; set; }
		[Argument(1, Description = "your actual activity")]
		public string Activity { get; set; }


		protected override JournalEntry CreateEntry()
		{
			return new JournalEntry(){Time = DateTime.Now, EntryType = this.EntryType, Project = Project, Activity = Activity};
		}

		protected override void LogReply(JournalEntry journalEntry, IConsole console)
		{
			console.ForegroundColor = ConsoleColor.DarkGreen;
			console.WriteLine($"{EntryType} - {Project} - {Activity} - {journalEntry.Time:dd.MM.yyyy} {journalEntry.Time:t}");
		}
	}
}