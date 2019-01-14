using System;
using System.Linq;
using fjj.Enums;
using fjj.Interfaces;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	[Command(Name = "stop", Description = "stop your journal")]
	public class StopCommand : TimeCommandBase
	{
		public StopCommand(IDbService dbService) : base(dbService, EntryType.Stop)
		{
		}
	}
}