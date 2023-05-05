using System.Security.Cryptography;
using System.Text;

namespace SearchService.Generators
{
	public class KeyFieldGuidGenerator : IGuidGenerator
	{
		public KeyFieldGuidGenerator()
		{
		}

        public Guid Generate(params string[] fields)
        {
            using (MD5 md5 = MD5.Create())
            {
                var input = string.Join(string.Empty, fields);
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return new Guid(hash);
            }
        }
    }
}

