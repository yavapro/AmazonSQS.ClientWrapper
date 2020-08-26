using System;

namespace AmazoneSQS.ClientWrapper
{
    public class GetResponseFailedException : Exception
    {
        public GetResponseFailedException(Exception innerException)
        {
        }
    }
}