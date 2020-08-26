namespace AmazoneSQS.ClientWrapper.Tests
{
    using Amazon.SQS;
    using Moq;
    using Xunit;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using Amazon.SQS.Model;
    using FluentAssertions;

    public class SQSProviderTestsUnit
    {
        private readonly SQSProvider target;
        private readonly Mock<IAmazonSQS> amazonSQSMock;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        public SQSProviderTestsUnit()
        {
            amazonSQSMock = new Mock<IAmazonSQS>();

            target = new SQSProvider(amazonSQSMock.Object);
        }

        [Fact]
        public async void ReceiveMessageWithDegradation_Test()
        {
            var request = new ReceiveMessageRequest();

            var message = new ReceiveMessageResponse
            {
                HttpStatusCode = HttpStatusCode.OK,
                Messages = new List<Message>()
            };

            amazonSQSMock
                .Setup(e => e.ReceiveMessageAsync(request, cancellationToken))
                .ReturnsAsync(message);

            var response = await target.ReceiveMessageWithDegradation(request, cancellationToken);

            response.Should().NotBeNull();
            response.Should().Be(message);

            amazonSQSMock.VerifyAll();
        }

        [Fact]
        public async void DeleteMessageWithDegradation_Test()
        {
            var request = new DeleteMessageRequest();

            var message = new DeleteMessageResponse
            {
                HttpStatusCode = HttpStatusCode.OK
            };

            amazonSQSMock
                .Setup(e => e.DeleteMessageAsync(request, cancellationToken))
                .ReturnsAsync(message);

            var response = await target.DeleteMessageWithDegradation(request, cancellationToken);

            response.Should().NotBeNull();
            response.Should().Be(message);

            amazonSQSMock.VerifyAll();
        }

        [Fact]
        public async void GetQueueAttributesWithDegradation_Test()
        {
            var request = new GetQueueAttributesRequest();

            var message = new GetQueueAttributesResponse
            {
                HttpStatusCode = HttpStatusCode.OK
            };

            amazonSQSMock
                .Setup(e => e.GetQueueAttributesAsync(request, cancellationToken))
                .ReturnsAsync(message);

            var response = await target.GetQueueAttributesWithDegradation(request, cancellationToken);

            response.Should().NotBeNull();
            response.Should().Be(message);

            amazonSQSMock.VerifyAll();
        }

        [Fact]
        public async void GetQueueUrlWithDegradation_Test()
        {
            var request = new GetQueueUrlRequest();

            var message = new GetQueueUrlResponse
            {
                HttpStatusCode = HttpStatusCode.OK
            };

            amazonSQSMock
                .Setup(e => e.GetQueueUrlAsync(request, cancellationToken))
                .ReturnsAsync(message);

            var response = await target.GetQueueUrlWithDegradation(request, cancellationToken);

            response.Should().NotBeNull();
            response.Should().Be(message);

            amazonSQSMock.VerifyAll();
        }
    }
}