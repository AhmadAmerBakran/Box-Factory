using infrastructure;
using infrastructure.interfaces;
using service;

var builder = WebApplication.CreateBuilder(args);

// Initialize the database connection string
builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<BoxService>();
builder.Services.AddSingleton<IBoxRepository, Repository>();
builder.Services.AddSingleton<CreateDataBase>();

var app = builder.Build();

app.Services.GetRequiredService<CreateDataBase>().SetupDatabase();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>

{ 
    options.SetIsOriginAllowed(origin => true)

        .AllowAnyMethod()

        .AllowAnyHeader()

        .AllowCredentials();

});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
