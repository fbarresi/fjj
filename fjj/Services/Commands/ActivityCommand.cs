using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using fjj.Models;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	public class ActivityCommand : TimeCommandBase
	{
		public ActivityCommand(IDbService dbService) : base(dbService, EntryType.Activity)
		{
		}
		[Option(ShortName = "p", LongName = "project")]
		public string Activity { get; set; }

		protected override JournalEntry CreateEntry()
		{
			return new JournalEntry(){Time = DateTime.Now, EntryType = this.EntryType, Activity = Activity};
		}
	}
}