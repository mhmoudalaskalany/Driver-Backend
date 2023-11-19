using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Driver.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
