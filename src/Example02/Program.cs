using Example02.Binders;
using Example02.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISystemClock, SystemClock>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .MapGet("api/{id:int}", (int id) =>
    {
        var response = new
        {
            Id = id,
            Source = "FromRoute"
        };
        return Results.Ok(response);
    });

app
    .MapGet("api/trace", ([FromHeader(Name = "X-Trace-Id")] string traceId) =>
    {
        var response = new
        {
            TraceId = traceId,
            Source = "FromHeader"
        };
        return Results.Ok(response);
    });

app
    .MapGet("api/search", ([FromQuery] string keyword) =>
    {
        var response = new
        {
            KeyWord = keyword,
            Source = "FromQuery"
        };
        return Results.Ok(response);
    });

app
    .MapGet("api/list", ([AsParameters] ApiQuery query) =>
    {
        var response = new
        {
            ApiQuery = query,
            Source = "FromQuery"
        };
        return Results.Ok(response);
    });

app
    .MapGet("api/custom", ([FromQuery] BooleanModelBinder answer) =>
    {
        var response = new
        {
            Value = answer.Value,
            Source = "CustomBinding"
        };
        return Results.Ok(response);
    });

app
    .MapGet("api/date", ([FromServices] ISystemClock systemClock) =>
    {
        var response = new
        {
            UtcNow = systemClock.UtcNow,
            Source = "FromQuery"
        };
        return Results.Ok(response);
    });

app
    .MapPost("api", ([FromBody] ApiRequest request) =>
    {
        var response = new
        {
            ApiResponse = new ApiResponse($"{request.FirstName} {request.LastName}"),
            Source = "FromBody"
        };
        return Results.Ok(response);
    });

app
    .MapPut("api/{id:int}", ([FromRoute] int id, [FromBody] ApiRequest request) => request.Id == id ? Results.NoContent() : Results.BadRequest());

app
    .MapDelete("api/{id:int}", ([FromRoute] int id) => id > 0 ? Results.NoContent() : Results.BadRequest());

app.Run();