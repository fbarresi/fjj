using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using fjj.Models;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	public class ProjectCommand : TimeCommandBase
	{
		public ProjectCommand(IDbService dbService) : base(dbService, EntryType.Project)
		{
		}

		[Option(ShortName = "p", LongName = "project")]
		public string Project { get; set; }


		protected override JournalEntry CreateEntry()
		{
			if (Project.Contains("-"))
			{
				var tiles = Project.Split("-", StringSplitOptions.RemoveEmptyEntries);
				return new JournalEntry(){Time = DateTime.Now, EntryType = this.EntryType, Project = tiles.FirstOrDefault().Trim(), Activity = tiles.LastOrDefault().Trim()};

			}
			else
				return new JournalEntry(){Time = DateTime.Now, EntryType = this.EntryType, Project = Project};
		}
	}
}