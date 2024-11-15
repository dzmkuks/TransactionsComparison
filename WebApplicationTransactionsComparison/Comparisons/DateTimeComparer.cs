namespace WebApplicationTransactionsComparison.Comparisons
{
	public static class DateTimeComparer
	{
		public static bool Compare(DateTime first, DateTime second)
		{
			var result = DateTime.Compare(first, second);

			if(result > 0) 
			{
				return DateTime.Compare(first.AddHours(-24), second) > 0;
            }
            if (result < 0)
            {
                return DateTime.Compare(first, second.AddHours(-24)) < 0;
            }

            return true;
		}
	}
}
