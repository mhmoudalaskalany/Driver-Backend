using System.Diagnostics.CodeAnalysis;

namespace Driver.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class SuccessResponse<T>
    {
        public T Data { get; set; }
    }
}
