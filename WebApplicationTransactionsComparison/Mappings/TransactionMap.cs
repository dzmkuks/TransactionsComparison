using TransactionsEF;
using WebApplicationTransactionsComparison.Models;

namespace WebApplicationTransactionsComparison.Mappings
{
    public static class TransactionMap
    {
        public static Transaction Map(TransactionModel transaction)
        {
            return new Transaction()
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                ProcessedAt = transaction.ProcessedAt,
            };
        }

        public static List<Transaction> MapList(List<TransactionModel> transactions)
        {
            var list = new List<Transaction>();
            transactions.ForEach(transaction => { list.Add(Map(transaction)); });
            return list;
        }

        public static TransactionModel Map(Transaction transaction)
        {
            return new TransactionModel()
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                ProcessedAt = transaction.ProcessedAt,
            };
        }

        public static List<TransactionModel> MapList(List<Transaction> transactions)
        {
            var list = new List<TransactionModel>();
            transactions.ForEach(transaction => { list.Add(Map(transaction)); });
            return list;
        }
    }
}
