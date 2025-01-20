using Microsoft.AspNetCore.Mvc.Testing;

namespace BreadmakerTests;

public class DotnetApiTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Theory]
    [InlineData("/")]
    public async Task Get_PlaintextEndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/plain; charset=utf-8", 
            response.Content.Headers.ContentType!.ToString());
    }

    [Fact]
    public async Task Get_HelloWorldResponse_FromBaseUrl()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        Assert.Equal("text/plain; charset=utf-8",
            response.Content.Headers.ContentType!.ToString());
        Assert.Equal("Hello, world!", await response.Content.ReadAsStringAsync());
    }
}


