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
			DbFile = Path.Combine(GetSaveLocation(), Constants.Constants.DbFilename);
			db = new LiteDatabase(DbFile);
		}

		public void Dispose()
		{
			db.Dispose();
		}

		private string GetSaveLocation()
		{
			var applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var dataLocation = Path.Combine(applicationData, Constants.Constants.DataFolderName);
			if(!Directory.Exists(dataLocation))
				Directory.CreateDirectory(dataLocation);
			return dataLocation;
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

		public string DbFile { get; private set; }

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
			if(CountStartAndStopForADay(entry) % 2 != 1)
				throw new Exception("Journal was not started yet");
		}

		private int CountStartAndStopForADay(JournalEntry entry) => GetCollection()
			.Find(Query.Between(nameof(JournalEntry.Time), entry.Time.Date, entry.Time+TimeSpan.FromDays(1)))
			.Count(journalEntry => journalEntry.EntryType == EntryType.Stop || journalEntry.EntryType == EntryType.Start);

		private void JournalWasStopped(JournalEntry entry)
		{
			if(CountStartAndStopForADay(entry) % 2 != 0)
				throw new Exception("Journal was not stopped yet");
		}


		private LiteCollection<JournalEntry> GetCollection() => db.GetCollection<JournalEntry>(Constants.Constants.JournalCollectionName);

	}
}