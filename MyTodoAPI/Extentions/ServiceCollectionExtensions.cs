using MyTodoAPI.Poviders;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MyTodoAPI.Extentions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
     
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="environment">Hosting environment</param>
        /// <returns>Configured engine and app settings</returns>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {


            //add configuration parameters
    

          
            Configuration = configuration;


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(o => AddSwaggerDocumentation(o));

            services.AddSwaggerGen();

            //add options feature
            services.AddOptions();

            //add caching
            services.AddMemoryCache();

            services.AddAspNetCoreRateLimiting();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //add logging
            //services.AddLogger();

        
            services.AddServices();

            //add automapper
            //services.AddAutoMapper();

            //add auth
            //services.AddAuthentication();

            ////add versioning
            //services.AddApiVersioning();

            //add routing
            services.AddRouting(options => options.LowercaseUrls = true);

         
        }

        

        public static void AddAspNetCoreRateLimiting(this IServiceCollection services)
        {
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

    


        }



        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }



        /// <summary>
        /// Add custom services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddServices(this IServiceCollection services)
        {


            // Services
            services.AddScoped<ITodoItemProvider, TodoItemProvider>();
       
        }



       

        private static void AddSwaggerDocumentation(SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }



        


    }
}
