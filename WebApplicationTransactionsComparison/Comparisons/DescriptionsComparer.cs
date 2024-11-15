namespace WebApplicationTransactionsComparison.Comparisons
{
	public static class DescriptionsComparer
	{
		public static bool Compare(string first, string second)
		{
			var result = string.Compare(first, second, StringComparison.OrdinalIgnoreCase);

			return result == 0;
		}
	}
}
