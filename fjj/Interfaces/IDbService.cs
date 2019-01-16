
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using fjj.Models;

namespace fjj.Interfaces
{
	public interface IDbService
	{
		IEnumerable<JournalEntry> Filter(Expression<Func<JournalEntry, bool>> predicate);
		void TryAdd(JournalEntry entry);
	}
}