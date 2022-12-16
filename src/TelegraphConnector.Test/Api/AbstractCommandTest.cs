using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelegraphConnector.Api;

namespace TelegraphConnector.Test.Api
{
    public abstract class AbstractCommandTest
    {
        protected string GetTextFromFile(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return File.ReadAllText(Path.Combine(dirPath, "Api", "MockResponses", relativePath));
        }

        protected Mock<ITelegraphClient> CreateMockClient(string responseFile)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetTextFromFile(responseFile), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var moqClient = new Mock<ITelegraphClient>();
            moqClient.Setup(m => m.GetHttpClient()).Returns(httpClient);
            return moqClient;
        }
    }
}
