using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpLibrary.Models;

namespace SharpLibrary
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(_configuration["Data:SharpLibrary:ConnectionString"]));
            services.AddTransient<IGenreRepository, GenreDBRepository>();
            services.AddTransient<IAuthorRepository, AuthorDBRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseDeveloperExceptionPage();
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "paginationGenre",
                    template: "Administrator/Genres/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Genre", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationAuthor",
                    template: "Administrator/Authors/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Author", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{area=Admin}/{controller=Genre}/{action=Index}/{id?}");
            });
        }
    }
}
