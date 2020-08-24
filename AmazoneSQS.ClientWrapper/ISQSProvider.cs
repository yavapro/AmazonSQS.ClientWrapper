namespace AmazoneSQS.ClientWrapper
{
    using Amazon.SQS.Model;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISQSProvider
    {
        Task<ReceiveMessageResponse> ReceiveMessageWithDegradation(ReceiveMessageRequest request, CancellationToken cancellationToken);
    }
}