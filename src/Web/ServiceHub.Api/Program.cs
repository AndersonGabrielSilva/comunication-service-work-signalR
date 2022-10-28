using Microsoft.AspNetCore.ResponseCompression;
using ServiceHub.Api.Hubs;
using ServiceWorker.Core.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
//builder.Services.AddCors();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});



var app = builder.Build();

app.UseResponseCompression();

//app.UseCors(builder =>
//{
//    builder
//      .AllowAnyOrigin()
//      .AllowAnyHeader()
//      .WithMethods("GET", "PATCH", "DELETE", "PUT", "POST", "OPTIONS")
//      .AllowCredentials();

//});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseRouting();

//app.MapControllers();
//app.MapHub<WokerHub>(SignalRName.SignalRRouteWorkerHub);
//app.MapControllerRoute("api", "api/{controller}/{action}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<WokerHub>(SignalRName.SignalRRouteWorkerHub);
    endpoints.MapControllerRoute("api", "api/{controller}/{action}");

});

app.Run();
