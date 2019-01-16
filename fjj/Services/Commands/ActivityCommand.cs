using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using fjj.Models;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	[Command(Name = "activity", Description = "start current activity")]
	public class ActivityCommand : TimeCommandBase
	{
		public ActivityCommand(IDbService dbService) : base(dbService, EntryType.Activity)
		{
		}

		[Argument(1, Description = "your actual activity")]
		public string Activity { get; set; }

		protected override JournalEntry CreateEntry()
		{
			return new JournalEntry(){Time = DateTime.Now, EntryType = this.EntryType, Activity = Activity};
		}

		protected override void LogReply(JournalEntry journalEntry, IConsole console)
		{
			console.ForegroundColor = ConsoleColor.DarkGreen;
			console.WriteLine($"{EntryType} - {Activity} - {journalEntry.Time:dd.MM.yyyy} {journalEntry.Time:t}");
		}
	}
}