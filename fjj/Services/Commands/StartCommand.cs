using System;
using fjj.Enums;
using fjj.Interfaces;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	[Command(Name = "start", Description = "start your journal")]
	public class StartCommand : TimeCommandBase
	{
		public StartCommand(IDbService dbService) : base(dbService, EntryType.Start)
		{
		}
	}
}