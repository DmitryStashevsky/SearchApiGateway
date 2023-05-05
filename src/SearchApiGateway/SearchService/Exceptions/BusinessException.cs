namespace SearchService.Exceptions
{
	public class BusinessException : Exception
    {
		public string Message { get; init; }

		public BusinessException(string message) 
		{
			Message = message;
		}
	}
}

