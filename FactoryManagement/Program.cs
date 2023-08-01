using EmployeeManagement_Sql.Extensions;


var builder = WebApplication.CreateBuilder();

// add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();

// i added this methods to "services property" myself
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureResponseBase();
builder.Services.ConfigureCors();

// add logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// set development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("miarmely");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();