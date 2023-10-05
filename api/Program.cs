using infrastructure;
using service;

var builder = WebApplication.CreateBuilder(args);

// Initialize the database connection string
builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BoxService>();
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<CreateDataBase>();

//builder.Services.AddSingleton(new CreateDataBase(Utilities.ProperlyFormattedConnectionString));



var app = builder.Build();

app.Services.GetRequiredService<CreateDataBase>().SetupDatabase();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.MapControllers();
app.Run();
