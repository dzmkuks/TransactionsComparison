using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsEF
{
	public class TransactionMap : ClassMap<Transaction>
	{
		public TransactionMap()
		{
			Map(m => m.Id).Ignore();
			Map(m => m.Amount).Index(0).Name("Amount");
			Map(m => m.Description).Index(1).Name("Description");
			Map(m => m.ProcessedAt).Index(2).Name("ProcessedAt");
		}
	}
}
