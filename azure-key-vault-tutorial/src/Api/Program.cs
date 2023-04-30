using Api.Behaviours;
using Api.Middleware;
using Azure.Identity;
using Database.VaultTutorialKVs;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.OpenApi.Models;
using Serilog;
using Threenine;
using Threenine.Data.DependencyInjection;
using Threenine.Services;

const string ConnectionsStringName = "Local_DB";


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
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(Program).Assembly , typeof(VaultTutorialKVContext).Assembly});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
var connectionString = builder.Configuration.GetConnectionString(ConnectionsStringName);
builder.Services.AddDbContext<VaultTutorialKVContext>(x => x.UseNpgsql(connectionString)).AddUnitOfWork<VaultTutorialKVContext>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IEntityValidationService<>),typeof(EntityValidationService<>));
builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));

var keyVault = builder.Configuration.GetSection(ApplicationConstants.KeyVault).Get<KeyVaultSettings>();
builder.Services.AddAzureClients(clientBuilder =>
{
    // Add a KeyVault client
    clientBuilder.AddSecretClient(new Uri($"https://{keyVault?.Vault}.vault.azure.net/"));
    clientBuilder.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Database migrations
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<VaultTutorialKVContext>();
    context?.Database.Migrate();
}

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
