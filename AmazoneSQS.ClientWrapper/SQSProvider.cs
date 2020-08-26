namespace AmazoneSQS.ClientWrapper
{
    using Amazon.SQS;
    using Amazon.SQS.Model;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class SQSProvider : ISQSProvider
    {
        private readonly IAmazonSQS sqsClient;

        public SQSProvider(IAmazonSQS sqsClient)
        {
            this.sqsClient = sqsClient;
        }

        public async Task<ReceiveMessageResponse> ReceiveMessageWithDegradation(ReceiveMessageRequest request, CancellationToken cancellationToken)
        {
            var failedAttemptCount = 0;
            Exception lastException = null;
            ReceiveMessageResponse response = null;

            while (failedAttemptCount < 10 && !cancellationToken.IsCancellationRequested)
            {
                var delayTime = SuspendTimeInMilliseconds(failedAttemptCount);

                if (delayTime > 0)
                {
                    await Task.Delay(delayTime, cancellationToken);
                }

                try
                {
                    response = await sqsClient.ReceiveMessageAsync(request, cancellationToken);

                    if (response?.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return response;
                    }

                    if (response?.Messages.Count > 0)
                    {
                        return response;
                    }

                    lastException = new HttpRequestException($"Got HttpStatusCode {response?.HttpStatusCode} by request");
                    failedAttemptCount++;
                }
                catch (AmazonSQSException ex)
                {
                    lastException = ex;
                    failedAttemptCount++;
                }
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            throw new GetResponseFailedException(lastException);
        }

        public async Task<DeleteMessageResponse> DeleteMessageWithDegradation(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            var failedAttemptCount = 0;
            Exception lastException = null;
            DeleteMessageResponse response = null;

            while (failedAttemptCount < 10 && !cancellationToken.IsCancellationRequested)
            {
                var delayTime = SuspendTimeInMilliseconds(failedAttemptCount);

                if (delayTime > 0)
                {
                    await Task.Delay(delayTime, cancellationToken);
                }

                try
                {
                    response = await sqsClient.DeleteMessageAsync(request, cancellationToken);

                    if (response?.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return response;
                    }

                    lastException = new HttpRequestException($"Got HttpStatusCode {response?.HttpStatusCode} by request");
                    failedAttemptCount++;
                }
                catch (AmazonSQSException ex)
                {
                    lastException = ex;
                    failedAttemptCount++;
                }
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            throw new GetResponseFailedException(lastException);
        }

        public async Task<GetQueueUrlResponse> GetQueueUrlWithDegradation(GetQueueUrlRequest request, CancellationToken cancellationToken)
        {
            var failedAttemptCount = 0;
            Exception lastException = null;
            GetQueueUrlResponse response = null;

            while (failedAttemptCount < 10 && !cancellationToken.IsCancellationRequested)
            {
                var delayTime = SuspendTimeInMilliseconds(failedAttemptCount);

                if (delayTime > 0)
                {
                    await Task.Delay(delayTime, cancellationToken);
                }

                try
                {
                    response = await sqsClient.GetQueueUrlAsync(request, cancellationToken);

                    if (response?.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return response;
                    }

                    lastException = new HttpRequestException($"Got HttpStatusCode {response?.HttpStatusCode} by request");
                    failedAttemptCount++;
                }
                catch (AmazonSQSException ex)
                {
                    lastException = ex;
                    failedAttemptCount++;
                }
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            throw new GetResponseFailedException(lastException);
        }

        public async Task<GetQueueAttributesResponse> GetQueueAttributesWithDegradation(GetQueueAttributesRequest request, CancellationToken cancellationToken)
        {
            var failedAttemptCount = 0;
            Exception lastException = null;
            GetQueueAttributesResponse response = null;

            while (failedAttemptCount < 10 && !cancellationToken.IsCancellationRequested)
            {
                var delayTime = SuspendTimeInMilliseconds(failedAttemptCount);

                if (delayTime > 0)
                {
                    await Task.Delay(delayTime, cancellationToken);
                }

                try
                {
                    response = await sqsClient.GetQueueAttributesAsync(request, cancellationToken);

                    if (response?.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return response;
                    }

                    lastException = new HttpRequestException($"Got HttpStatusCode {response?.HttpStatusCode} by request");
                    failedAttemptCount++;
                }
                catch (AmazonSQSException ex)
                {
                    lastException = ex;
                    failedAttemptCount++;
                }
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            throw new GetResponseFailedException(lastException);
        }

        private int SuspendTimeInMilliseconds(int failedAttemptCount)
        {
            const int MillisecondsInSecond = 1000;

            if (failedAttemptCount < 2)
            {
                return 0;
            }

            if (failedAttemptCount < 4)
            {
                return MillisecondsInSecond;
            }

            if (failedAttemptCount < 6)
            {
                return 5 * MillisecondsInSecond;
            }

            if (failedAttemptCount < 8)
            {
                return 10 * MillisecondsInSecond;
            }

            return 30 * MillisecondsInSecond;
        }
    }
}