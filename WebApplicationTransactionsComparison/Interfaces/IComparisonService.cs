using WebApplicationTransactionsComparison.Models;

namespace WebApplicationTransactionsComparison.Interfaces
{
	public interface IComparisonService
	{
        Task<Tuple<List<TransactionModel>, List<TransactionModel>, List<TransactionModel>>> CompareTransactions(List<TransactionModel> transactions);
	}
}
