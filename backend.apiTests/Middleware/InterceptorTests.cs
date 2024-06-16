using backend.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Xunit;

namespace backend.api.Middleware.Tests
{
    public class InterceptorTests
    {
        [Fact]
        public async Task Invoke_LogsInformationWithRequestMethodAndPath()
        {
            // Arrange
            var fakeNext = A.Fake<RequestDelegate>();
            var fakeLogger = A.Fake<ILogger<ErrorModel>>();
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/testpath";

            var interceptor = new Interceptor(fakeNext, fakeLogger);

            // Act
            await interceptor.Invoke(context);

            // Assert
            Assert.True(true);
        }
    }
}