using System.Diagnostics.CodeAnalysis;

namespace Driver.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SearchResultNotFoundException :BaseException
    {
        public SearchResultNotFoundException():base("Result not found")
        {
                
        }
    }
}
