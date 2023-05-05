using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using SearchApiGateway.Configuration;
using SearchService;
using SearchService.Providers.One;
using SearchService.Providers.Two;
using SearchApiGateway.Middlewares;

namespace SearchApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddSearchServiceDI();
            builder.Services.AddSearchServiceGatewayDI();

            builder.Services.Configure<SearchProviderOneSettingsOption>(builder.Configuration.GetSection(SearchProviderOneSettingsOption.SectionName));
            builder.Services.Configure<SearchProviderTwoSettingsOption>(builder.Configuration.GetSection(SearchProviderTwoSettingsOption.SectionName));

            // Register providers settings
            builder.Services.AddSingleton<SearchProviderOneSettings>(sp =>
            {
                var options = sp.GetService<IOptions<SearchProviderOneSettingsOption>>();
                return options.Value;
            });
            builder.Services.AddSingleton<SearchProviderTwoSettings>(sp =>
            {
                var options = sp.GetService<IOptions<SearchProviderTwoSettingsOption>>();
                return options.Value;
            });

            builder.Services.AddHttpClient();

            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseExceptionMiddleware();
            app.Run();
        }
    }
}



