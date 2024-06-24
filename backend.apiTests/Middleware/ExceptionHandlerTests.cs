using backend.api.Middleware;
using backend.data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace backend.api.Tests.Middleware
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public async Task Invoke_CallsNext_WhenNoExceptionOccurs()
        {
            RequestDelegate fakeNext = A.Fake<RequestDelegate>();
            ILogger<ErrorModel> fakeLogger = A.Fake<ILogger<ErrorModel>>();
            DefaultHttpContext context = new();

            ExceptionHandler handler = new(fakeNext, fakeLogger);

            await handler.Invoke(context);

            A.CallTo(() => fakeNext(context)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Invoke_LogsErrorAndThrowsInvalidOperationException_WhenExceptionOccurs()
        {
            RequestDelegate fakeNext = A.Fake<RequestDelegate>();
            ILogger<ErrorModel> fakeLogger = A.Fake<ILogger<ErrorModel>>();
            DefaultHttpContext context = new();
            Exception exception = new("Test exception");

            A.CallTo(() => fakeNext(context)).Throws(exception);

            ExceptionHandler handler = new(fakeNext, fakeLogger);

            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Invoke(context));
            Assert.Equal("Test exception", ex.Message);
        }
    }
}