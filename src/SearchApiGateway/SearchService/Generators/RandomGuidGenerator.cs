namespace SearchService.Generators
{
	internal class RandomGuidGenerator : IGuidGenerator
	{
		public Guid Generate(params string[] fields)
        {
            return Guid.NewGuid();
        }
    }
}

