using Azure.Identity;
using Prompt.Persistence;
using Prompt.WebApi;
using Prompt.WebApi.Configuration;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("")
    .CreateLogger();


try
{
    Log.Information("Starting Web Application");

    var builder = WebApplication.CreateBuilder(args);



    builder.Configuration.AddAzureKeyVault(
           new Uri(builder.Configuration["AzureKeyVaultSettings:Uri"]),
           new ClientSecretCredential(
               tenantId: builder.Configuration["AzureKeyVaultSettings:TenantId"],
               clientId: builder.Configuration["AzureKeyVaultSettings:ClientId"],
               clientSecret: builder.Configuration["AzureKeyVaultSettings:ClientSecret"]
            ));

    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddWebApi();


    builder.Services.AddControllers();

    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerWithVersion();
        app.UseDeveloperExceptionPage();

    }
    app.UseStaticFiles();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{

	throw;
}			
	