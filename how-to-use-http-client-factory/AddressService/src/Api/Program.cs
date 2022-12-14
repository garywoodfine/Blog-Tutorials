using Api.Behaviours;
using Api.Middleware;
using Common;
using Domain;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Services;
using Threenine;
using Threenine.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
    c.CustomSchemaIds(x => x.FullName);
    c.DocumentFilter<JsonPatchDocumentFilter>();
    c.EnableAnnotations();
});
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddMediatR(typeof(Program))
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.Configure<AfdSettings>(builder.Configuration.GetSection(Constants.AfdSettings)).AddHttpClient<IAddressDataProvider, AddressProvider>().ConfigureHttpClient(
    (config, client) =>
    {
        var settings = config.GetRequiredService<IOptions<AfdSettings>>().Value;

        var afdBaseAddress = new UriBuilder(settings.Endpoint);
        var parameters = new AfdParameterBuilder()
            .Create()
            .Data(settings.Data)
            .CountryCode(settings.CountryISO)
            .Serial(settings.Serial)
            .Password(settings.Password)
            .Task(settings.Task)
            .Format(settings.Format)
            .Fields(settings.Fields)
            .Build();
        afdBaseAddress.Query = parameters;

        client.BaseAddress = afdBaseAddress.Uri;
    });



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IEntityValidationService<>),typeof(EntityValidationService<>));
builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}
app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();
app.Run();
