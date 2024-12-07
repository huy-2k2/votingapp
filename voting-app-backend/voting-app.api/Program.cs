using voting_app.api.Middleware;

namespace voting_app.api
{
    public class Program
    {

        private static string MY_ALLOW_SPECIFIC_ORIGIN = "_myAllowSpecificOrigins";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // add config
            builder.AddConfig();

            builder.AddOtherService();
            builder.AddInfrastructureRepo();

            // add repo, service
            builder.AddApplicationService();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MY_ALLOW_SPECIFIC_ORIGIN, policy =>
                {
                    policy.AllowAnyOrigin()      // Cho phép tất cả các nguồn
                    .AllowAnyMethod()      // Cho phép tất cả các phương thức HTTP
                    .AllowAnyHeader()
                    .WithExposedHeaders("set-cookie");   // Cho phép tất cả các header
                });
            });


            var app = builder.Build();

            app.UseCors(MY_ALLOW_SPECIFIC_ORIGIN);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<AuthContextMiddleware>();
            // app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}