using AdvertisingTask.Services;
using AdvertisingTask.Swagger;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Настройка Swagger для поддержки загрузки файлов
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AdPlatform API",
        Version = "v1",
        Description = "Сервис для подбора рекламных площадок по локации"
    });

    options.OperationFilter<FileUploadOperationFilter>();
});


builder.Services.AddSingleton<AdPlatformStorage>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdPlatform API V1");
        c.RoutePrefix = "swagger"; 
                                  
    });

    app.UseDeveloperExceptionPage(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
