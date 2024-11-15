using WebApplicationTransactionsComparison.Interfaces;
using WebApplicationTransactionsComparison.Models;

namespace WebApplicationTransactionsComparison.Services
{
    public class ComparisonService : IComparisonService
    {
		private readonly ITransactionRepository _transactionRepository;

		public ComparisonService(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
		}

		public async Task<Tuple<List<TransactionModel>, List<TransactionModel>, List<TransactionModel>>> CompareTransactions(List<TransactionModel> transactions)
		{
			var identicalTransactions = _transactionRepository.GetTransactionstAsync(transactions);

			var notFoundTransactions = new List<TransactionModel>();

			foreach (var transaction in transactions)
			{
				if (!identicalTransactions.Contains(transaction))
				{
					notFoundTransactions.Add(transaction);
				}
			}

			var alltrancFromdb = await _transactionRepository.GetAllTransactionsFromDb();

			foreach(var transaction in identicalTransactions)
            {
                alltrancFromdb.RemoveAll(t => t.Amount == transaction.Amount);
            }

            return Tuple.Create(identicalTransactions, notFoundTransactions, alltrancFromdb);
		}
    }
}
