using System;
using fjj.Enums;

namespace fjj.Models
{
	public class JournalEntry : CollectionObjectBase
	{
		public EntryType EntryType { get; set; }
		public DateTime Time { get; set; }
		public string Project { get; set; }
		public string Activity { get; set; }
	}
}