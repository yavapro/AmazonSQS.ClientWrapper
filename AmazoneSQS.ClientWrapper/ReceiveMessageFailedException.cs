using System;

namespace AmazoneSQS.ClientWrapper
{
    public class ReceiveMessageFailedException : Exception
    {
        public ReceiveMessageFailedException(Exception innerException)
        {
        }
    }
}