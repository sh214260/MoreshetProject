using AutoMapper;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
              //.AllowCredentials()
              //.SetPreflightMaxAge(TimeSpan.FromSeconds(86400)); ;
    });
});

builder.Services.AddBLServices();

var app = builder.Build();

void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .WithMethods("POST", "OPTIONS")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials()
           .SetPreflightMaxAge(TimeSpan.FromSeconds(86400)); ;
    });
});
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // app.UseCors("AllowAllOrigins");
    app.UseCors("AllowAll");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//void ConfigureServices(IServiceCollection services)
//{
//    services.AddCors(options =>
//    {
//        options.AddPolicy("AllowAllOrigins",
//            builder =>
//            {
//                builder.AllowAnyOrigin();
//            });
//    });
//}
//void ConfigureServices(IServiceCollection services)
//{
//    services.AddCors(options =>
//    {
//        options.AddDefaultPolicy(builder =>
//        {
//            builder.WithOrigins("http://localhost:3000")
//                .AllowAnyHeader()
//                .AllowAnyMethod().AllowAnyOrigin();
//        });
//    });
//}

//services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});
app.UseCors("AllowAll");

    //builder =>
//builder.WithOrigins("http://localhost:3000")
//       .WithMethods("POST", "PUT")
//       .WithHeaders("Content-Type"));
app.Run();
