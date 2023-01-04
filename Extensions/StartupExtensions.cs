using Mythos.Common.Authentication;
using Mythos.Common.Authorization;
using Mythos.Common.Users;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.AspNetCore.Builder;

public static class StartupExtensions
{
    public static WebApplicationBuilder MythosBuilderStartup(this WebApplicationBuilder builder)
    {
        // Configure auth
        builder.AddAuthentication();
        builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

        // Add the service to generate JWT tokens
        builder.Services.AddTokenService();

        var connectionString = builder.Configuration.GetConnectionString("Mythos") ?? "Data Source=.db/Mythos.db";
        builder.Services.AddSqlite<MythosDbContext>(connectionString, b => b.MigrationsAssembly("Mythos.Common"));

        builder.Services.AddIdentityCore<MythosUser>()
            .AddEntityFrameworkStores<MythosDbContext>();

        builder.Services.AddCurrentUser();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

        return builder;
    }

    public static WebApplication MythosAppStartup(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(ui =>
        {
            ui.ConfigObject = new()
            {
                DeepLinking = true,
                DisplayRequestDuration = true,
                ShowCommonExtensions = true,
                TryItOutEnabled = true,
                PersistAuthorization = true,
            };
        });

        app.UseHttpsRedirection();

        app.MapUsers();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<MythosDbContext>();
            context.Database.EnsureCreated();
            // DbInitializer.Initialize(context);
        }

        return app;
    }
}
