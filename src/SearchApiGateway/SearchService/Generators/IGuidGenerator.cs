namespace SearchService.Generators
{
	internal interface IGuidGenerator
	{
		Guid Generate(params string[] fields);
	}
}

