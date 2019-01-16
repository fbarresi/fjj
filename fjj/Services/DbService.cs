using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using fjj.Interfaces;
using fjj.Models;
using LiteDB;

namespace fjj.Services
{
	public class DbService : IDbService, IDisposable
	{
		private readonly LiteDatabase db;

		public DbService()
		{
			db = new LiteDatabase(Path.Combine(GetSaveLocation(), Constants.Constants.DbFilename));
		}

		public void Dispose()
		{
			db.Dispose();
		}

		private string GetSaveLocation()
		{
			return AppDomain.CurrentDomain.BaseDirectory;
		}

		public void Add(JournalEntry entry)
		{
			var collection = GetCollection<JournalEntry>();
			collection.Insert(entry);
		}

		public IEnumerable<JournalEntry> Filter(Expression<Func<JournalEntry, bool>> predicate)
		{
			var collection = GetCollection<JournalEntry>();
			return collection.Find(predicate);
		}

		private LiteCollection<T> GetCollection<T>() => db.GetCollection<T>(Constants.Constants.JournalCollectionName);
	}
}