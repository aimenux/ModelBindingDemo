namespace Example01.Tests;

internal static class Extensions
{
    public static HttpClient WithRequestHeader(this HttpClient client, string headerName, string headerValue)
    {
        client.DefaultRequestHeaders.Add(headerName, headerValue);
        return client;
    }
}