using fjj.Interfaces;
using McMaster.Extensions.CommandLineUtils;

namespace fjj.Services.Commands
{
	[Command(Name = "start", Description = "start your journal")]
	public class StartCommand : TimeCommandBase
	{
		public StartCommand(IDbService dbService) : base(dbService)
		{
		}

		private int OnExecute(CommandLineApplication app, IConsole console)
		{
			console.WriteLine("Yeah!");
			return 0;
		}
	}

	public class CommandBase
	{
		
		protected readonly IDbService DBService;

		public CommandBase(IDbService dbService)
		{
			this.DBService = dbService;
		}
	}

	public class TimeCommandBase : CommandBase
	{
		[Option(ShortName = "-")]
		public int Minus { get; }

		[Option(ShortName = "+")]
		public int Plus { get; }


		public TimeCommandBase(IDbService dbService) : base(dbService)
		{
		}
	}
}