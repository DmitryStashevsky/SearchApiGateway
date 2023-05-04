using Microsoft.AspNetCore.Mvc.Versioning;
using SearchApiGateway.Configuration;
using SearchService;
using SearchService.Providers.One;
using SearchService.Providers.Two;

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

            builder.Services.AddSearchServiceDI();

            builder.Services.Configure<SearchProviderOneSettingsOption>(builder.Configuration.GetSection(SearchProviderOneSettingsOption.SectionName));
            builder.Services.Configure<SearchProviderTwoSettingsOption>(builder.Configuration.GetSection(SearchProviderTwoSettingsOption.SectionName));

            builder.Services.AddSingleton<SearchProviderOneSettings, SearchProviderOneSettingsOption>();
            builder.Services.AddSingleton<SearchProviderTwoSettings, SearchProviderTwoSettingsOption>();

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

            app.Run();
        }
    }
}



