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
            // Arrange
            var fakeNext = A.Fake<RequestDelegate>();
            var fakeLogger = A.Fake<ILogger<ErrorModel>>();
            var context = new DefaultHttpContext();

            var handler = new ExceptionHandler(fakeNext, fakeLogger);

            // Act
            await handler.Invoke(context);

            // Assert
            A.CallTo(() => fakeNext(context)).MustHaveHappenedOnceExactly();
        }

        // Exception handling test
        [Fact]
        public async Task Invoke_LogsErrorAndThrowsInvalidOperationException_WhenExceptionOccurs()
        {
            // Arrange
            var fakeNext = A.Fake<RequestDelegate>();
            var fakeLogger = A.Fake<ILogger<ErrorModel>>();
            var context = new DefaultHttpContext();
            var exception = new Exception("Test exception");

            A.CallTo(() => fakeNext(context)).Throws(exception);

            var handler = new ExceptionHandler(fakeNext, fakeLogger);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Invoke(context));
            Assert.Equal("Test exception", ex.Message);
        }
    }
}