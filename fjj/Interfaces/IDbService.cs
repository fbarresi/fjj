
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using fjj.Models;

namespace fjj.Interfaces
{
	public interface IDbService
	{
		void Add(JournalEntry entry);
		IEnumerable<JournalEntry> Filter(Expression<Func<JournalEntry, bool>> predicate);
	}
}