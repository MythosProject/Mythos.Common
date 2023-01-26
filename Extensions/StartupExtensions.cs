using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Mythos.Common.Users;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.Builder;

public static class StartupExtensions
{
    public static WebApplicationBuilder MythosBuilderStartup(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(config =>
        {
            config.SchemaFilter<EnumSchemaFilter>();
        });

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

        // app.MapUsers();

        return app;
    }
}

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            model.Enum.Clear();
            foreach (string enumName in Enum.GetNames(context.Type))
            {
                MemberInfo? memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
                EnumMemberAttribute? enumMemberAttribute = memberInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();
                string label = enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
                 ? enumName
                 : enumMemberAttribute.Value;
                model.Enum.Add(new OpenApiString(label));
            }
        }
    }
}