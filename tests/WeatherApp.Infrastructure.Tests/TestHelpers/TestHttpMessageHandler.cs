using System.Net.Http;

namespace WeatherApp.Infrastructure.Tests;

internal sealed class TestHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _responseFactory;

    public TestHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> responseFactory)
    {
        _responseFactory = responseFactory;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _responseFactory(request);
    }
}
