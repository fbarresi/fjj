using System;
using System.IO;
using System.Linq;
using System.Reflection;
using fjj.Interfaces;
using LiteDB;

namespace fjj.Services
{
	public class DbService : IDbService, IDisposable
	{
		private readonly LiteEngine db;

		public DbService()
		{
			db = new LiteEngine(Path.Combine(GetSaveLocation(), Constants.Constants.DbFilename));
		}

		public void Dispose()
		{
			db.Dispose();
		}

		private string GetSaveLocation()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			return Path.GetDirectoryName(location);
		}
	}
}