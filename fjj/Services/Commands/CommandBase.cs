using System;
using System.Linq;
using fjj.Interfaces;

namespace fjj.Services.Commands
{
	public class CommandBase
	{
		
		protected readonly IDbService DBService;

		public CommandBase(IDbService dbService)
		{
			this.DBService = dbService;
		}
	}
}