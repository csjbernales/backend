using Microsoft.AspNetCore.Builder;

namespace backend.api.Auth.Tests
{
    public class AuthConfigTests
    {
        private readonly WebApplicationBuilder builder;
        private readonly IEnvironmentWrapper env;

        public AuthConfigTests()
        {
            builder = WebApplication.CreateBuilder();
            env = A.Fake<IEnvironmentWrapper>();
        }

        [Fact()]
        public void AuthOptionsTest_Prod()
        {

            A.CallTo(() => env.IsDevelopment()).Returns(false);
            AuthConfig.AuthOptions(builder, env);
            Assert.True(true);
        }

        [Fact()]
        public void AuthOptionsTest_Dev()
        {
            A.CallTo(() => env.IsDevelopment()).Returns(true);
            AuthConfig.AuthOptions(builder, env);
            Assert.True(true);
        }
    }
}