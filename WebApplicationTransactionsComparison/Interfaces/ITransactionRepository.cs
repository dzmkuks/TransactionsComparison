
using WebApplicationTransactionsComparison.Models;

namespace WebApplicationTransactionsComparison.Interfaces
{
	public interface ITransactionRepository
	{
        Task<List<TransactionModel>> GetAllTransactionsFromDb();

        List<TransactionModel> GetTransactionstAsync(List<TransactionModel> transaction);

    }
}
