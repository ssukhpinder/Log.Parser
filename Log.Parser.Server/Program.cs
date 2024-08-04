using Log.Parser.BL.Extensions;
using Log.Parser.BL.Interface;
using Log.Parser.BL.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Override to make api routes lowercase on swagger UI
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Apply CORS policy so that APIs are accessible only to the Angular app domain i.e. https://localhost:4200
// To make it visible for other apps just provide domain names comma separated or add into the appsettings.json file
builder.Services.UseCorsPolicyHandler(new string[] { "https://localhost:4200" });

// Added to handle basic REST API Versioning
builder.Services.UseAppVersioningHandler();

//Dependency Injections
builder.Services.AddScoped<IParserService, FileParserService>();
builder.Services.AddScoped<IFileManageService, FileManageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Custom handler to update swagger for versioning
    app.UseSwaggerHandler();
}

// Custom global exception handler to handle any exception in the complete C# codebase.
// Create a common point to handle/log exceptions
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

// Apply above defined CORS policy across application
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
