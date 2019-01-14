using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using fjj.Models;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	public class TimeCommandBase : CommandBase
	{
		private readonly EntryType entryType;

		[Option(ShortName = "-")]
		public int Minus { get; }

		[Option(ShortName = "+")]
		public int Plus { get; }


		public TimeCommandBase(IDbService dbService, EntryType entryType) : base(dbService)
		{
			this.entryType = entryType;
		}

		protected virtual JournalEntry CreateEntry()
		{
			return new JournalEntry(){Time = DateTime.Now, EntryType = entryType};
		}

		protected virtual void LogReply(JournalEntry journalEntry, IConsole console)
		{
			console.ForegroundColor = ConsoleColor.DarkGreen;
			console.WriteLine($"{entryType} - {journalEntry.Time:dd.MM.yyyy} {journalEntry.Time:t}");
		}

		protected virtual void OnExecute(CommandLineApplication app, IConsole console)
		{
			var journalEntry = CreateEntry();
			
			journalEntry.Time += TimeSpan.FromMinutes(Plus);
			journalEntry.Time -= TimeSpan.FromMinutes(Minus);

			DBService.Add(journalEntry);

			LogReply(journalEntry, console);
		}
	}
}