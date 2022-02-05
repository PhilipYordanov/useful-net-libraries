using Polly;
using System.Net;

#region Configurations
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Reactive Strategies
var retryAsync = Policy
    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .Or<HttpRequestException>()
    .RetryAsync(retryCount: 3);

var waitAndRetryAsync = Policy
    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .Or<HttpRequestException>()
    .WaitAndRetryAsync(new List<TimeSpan>
    {
        { TimeSpan.FromSeconds(5) },
        { TimeSpan.FromSeconds(10) },
        { TimeSpan.FromSeconds(15) },
    });

var circuitBreakerAsync = Policy
    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .Or<HttpRequestException>()
    .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 3, durationOfBreak: TimeSpan.FromSeconds(20));

var fallbackAsync = Policy
    .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
    .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent("Oops :)")
    });
#endregion

#region Proactive Strategies
var timoutAsync = Policy
    .TimeoutAsync<HttpResponseMessage>(seconds: 4);

var bulkheadAsync = Policy
    .BulkheadAsync<HttpResponseMessage>(maxParallelization: 3, maxQueuingActions: 6);

#endregion

builder.Services.AddHttpClient("BrokenServer", client =>
{
    client.BaseAddress = new Uri("https://localhost:7009/api");
})
.AddPolicyHandler(retryAsync);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
