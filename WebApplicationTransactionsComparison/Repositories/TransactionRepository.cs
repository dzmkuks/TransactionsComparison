using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TransactionsEF;
using WebApplicationTransactionsComparison.Comparisons;
using WebApplicationTransactionsComparison.Interfaces;
using WebApplicationTransactionsComparison.Models;
using TransactionModel = WebApplicationTransactionsComparison.Models.TransactionModel;

namespace WebApplicationTransactionsComparison.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly TransactionContext _context;

		public TransactionRepository(TransactionContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

        public async Task<List<TransactionModel>> GetAllTransactionsFromDb()
        {
            return Mappings.TransactionMap.MapList(await _context.Transactions.ToListAsync());
        }

		public List<TransactionModel> GetTransactionstAsync(List<TransactionModel> transactions)
		{
			try
			{
				var transactionsFromDb = _context.Transactions.AsEnumerable().SelectMany(t => transactions.AsEnumerable().Where(
				tr => tr.Amount == t.Amount && DescriptionsComparer.Compare(tr.Description, t.Description) && DateTimeComparer.Compare(tr.ProcessedAt, t.ProcessedAt))
				.Select(ob => ob)).ToList();
                return transactionsFromDb;
            }
			catch (Exception ex) 
			{
				throw new Exception(ex.Message);
			}
		}
    }
}
