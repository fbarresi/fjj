using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using fjj.Enums;
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


		private void Add(JournalEntry entry)
		{
			var collection = GetCollection();
			collection.Insert(entry);
		}

		public IEnumerable<JournalEntry> Filter(Expression<Func<JournalEntry, bool>> predicate)
		{
			var collection = GetCollection();
			return collection.Find(predicate);
		}

		public void TryAdd(JournalEntry entry)
		{
			CanInsert(entry);
			Add(entry);
		}

		private void CanInsert(JournalEntry entry)
		{
			switch (entry.EntryType)
			{
				case EntryType.Start:
					JournalWasStopped(entry);
					break;
				case EntryType.Stop:
					JournalWasStarted(entry);
					break;
				case EntryType.Project:
					JournalWasStarted(entry);
					break;
				case EntryType.Activity:
					JournalWasStarted(entry);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void JournalWasStarted(JournalEntry entry)
		{
			if(CountStartAndStopForADay(entry) % 2 == 1)
				throw new Exception("Journal was not started yet");
		}

		private int CountStartAndStopForADay(JournalEntry entry) => GetCollection()
			.Count(journalEntry => journalEntry.Time.Date == entry.Time.Date &&
				       (journalEntry.EntryType == EntryType.Stop ||
					       journalEntry.EntryType == EntryType.Start));

		private void JournalWasStopped(JournalEntry entry)
		{
			if(CountStartAndStopForADay(entry) % 2 == 0)
				throw new Exception("Journal was not stopped yet");
		}


		private LiteCollection<JournalEntry> GetCollection() => db.GetCollection<JournalEntry>(Constants.Constants.JournalCollectionName);

	}
}