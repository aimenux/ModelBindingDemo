using System.Net;
using System.Net.Http.Json;
using Example01.Payloads;
using FluentAssertions;

namespace Example01.Tests;

public class IntegrationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Get_Route_Should_Return_Success_Response(int id)
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync($"/api/{id}");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Trace_Route_Should_Return_Success_Response()
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client
            .WithRequestHeader("X-Trace-Id", "xyz")
            .GetAsync("api/trace");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Search_Route_Should_Return_Success_Response()
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync("api/search?keyword=xyz");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task List_Route_Should_Return_Success_Response()
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync("api/list?take=50&skip=10");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Date_Route_Should_Return_Success_Response()
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync(@"api/date");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("yes")]
    [InlineData("no")]
    [InlineData("oui")]
    [InlineData("non")]
    [InlineData("1")]
    [InlineData("0")]
    public async Task Custom_Route_Should_Return_Success_Response(string answer)
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync($"api/custom?answer={answer}");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }    
    
    [Fact]
    public async Task Post_Route_Should_Return_Success_Response()
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();
        var request = new ApiRequest(1, "Walter", "White");

        // act
        var response = await client.PostAsJsonAsync("api", request);
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Put_Route_Should_Return_Success_Response(int id)
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();
        var request = new ApiRequest(id, "Walter", "White");

        // act
        var response = await client.PutAsJsonAsync($"api/{id}", request);
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        responseBody.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Delete_Route_Should_Return_Success_Response(int id)
    {
        // arrange
        var fixture = new WebApiTestFixture();
        var client = fixture.CreateClient();

        // act
        var response = await client.DeleteAsync($"api/{id}");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        responseBody.Should().BeEmpty();
    }
}