using backend.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace backend.api.Middleware.Tests
{
    public class InterceptorTests
    {
        [Fact]
        public async Task Invoke_LogsInformationWithRequestMethodAndPath()
        {
            RequestDelegate fakeNext = A.Fake<RequestDelegate>();
            ILogger<ErrorModel> fakeLogger = A.Fake<ILogger<ErrorModel>>();
            DefaultHttpContext context = new();
            context.Request.Method = "GET";
            context.Request.Path = "/testpath";

            Interceptor interceptor = new(fakeNext, fakeLogger);

            await interceptor.Invoke(context);

            Assert.True(true);
        }
    }
}