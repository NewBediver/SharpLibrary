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
            services.AddTransient<IPublishingRepository, PublishingDBRepository>();
            services.AddTransient<ILiteratureTypeRepository, LiteratureTypeDBRepository>();
            services.AddTransient<IStatusRepository, StatusDBRepository>();
            services.AddTransient<ITransactionTypeRepository, TransactionTypeDBRepository>();
            services.AddTransient<ISubscriptionTypeRepository, SubscriptionTypeDBRepository>();
            services.AddTransient<IRoleRepository, RoleDBRepository>();
            services.AddTransient<ILibraryRepository, LibraryDBRepository>();
            services.AddTransient<IRackRepository, RackDBRepository>();
            services.AddTransient<IShelfRepository, ShelfDBRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionDBRepository>();
            services.AddTransient<ILiteratureRepository, LiteratureDBRepository>();
            services.AddTransient<IUserRepository, UserDBRepository>();
            services.AddTransient<ITransactionRepository, TransactionDBRepository>();

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
                    name: "paginationPublishing",
                    template: "Administrator/Publishings/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Publishing", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationLiteratureType",
                    template: "Administrator/LiteratureTypes/Page{page}",
                    defaults: new { Area = "Admin", Controller = "LiteratureType", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationStatus",
                    template: "Administrator/Statuses/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Status", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationTransactionType",
                    template: "Administrator/TransactionTypes/Page{page}",
                    defaults: new { Area = "Admin", Controller = "TransactionType", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationRole",
                    template: "Administrator/Roles/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Role", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationLibrary",
                    template: "Administrator/Libraries/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Library", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationRack",
                    template: "Administrator/Racks/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Rack", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationShelf",
                    template: "Administrator/Shelves/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Shelf", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationSubscription",
                    template: "Administrator/Subscriptions/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Subscription", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationLiterature",
                    template: "Administrator/Literatures/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Literature", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationUser",
                    template: "Administrator/Users/Page{page}",
                    defaults: new { Area = "Admin", Controller = "User", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "paginationTransaction",
                    template: "Administrator/Transactions/Page{page}",
                    defaults: new { Area = "Admin", Controller = "Transaction", Action = "Index" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{area=Admin}/{controller=Genre}/{action=Index}/{id?}");
            });
        }
    }
}
