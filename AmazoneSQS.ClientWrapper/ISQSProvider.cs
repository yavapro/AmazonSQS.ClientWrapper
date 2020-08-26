namespace AmazoneSQS.ClientWrapper
{
    using Amazon.SQS.Model;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISQSProvider
    {
        Task<ReceiveMessageResponse> ReceiveMessageWithDegradation(ReceiveMessageRequest request, CancellationToken cancellationToken);

        Task<DeleteMessageResponse> DeleteMessageWithDegradation(DeleteMessageRequest request, CancellationToken cancellationToken);

        Task<GetQueueUrlResponse> GetQueueUrlWithDegradation(GetQueueUrlRequest request, CancellationToken cancellationToken);

        Task<GetQueueAttributesResponse> GetQueueAttributesWithDegradation(GetQueueAttributesRequest request, CancellationToken cancellationToken);
    }
}