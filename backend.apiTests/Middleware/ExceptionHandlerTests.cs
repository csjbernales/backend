using backend.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace backend.api.Middleware.Tests
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public async Task Invoke_CallsNext_WhenNoExceptionOccurs()
        {
                       var fakeNext = A.Fake<RequestDelegate>();
            var fakeLogger = A.Fake<ILogger<ErrorModel>>();
            var context = new DefaultHttpContext();

            var handler = new ExceptionHandler(fakeNext, fakeLogger);

                       await handler.Invoke(context);

                       A.CallTo(() => fakeNext(context)).MustHaveHappenedOnceExactly();
        }

               [Fact]
        public async Task Invoke_LogsErrorAndThrowsInvalidOperationException_WhenExceptionOccurs()
        {
                       var fakeNext = A.Fake<RequestDelegate>();
            var fakeLogger = A.Fake<ILogger<ErrorModel>>();
            var context = new DefaultHttpContext();
            var exception = new Exception("Test exception");

            A.CallTo(() => fakeNext(context)).Throws(exception);

            var handler = new ExceptionHandler(fakeNext, fakeLogger);

                       var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Invoke(context));
            Assert.Equal("Test exception", ex.Message);
        }
    }
}