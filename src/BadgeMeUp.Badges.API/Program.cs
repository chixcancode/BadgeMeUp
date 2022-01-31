
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BadgeMeUpDb");


builder.Services.AddSqlServer<BadgeDbContext>(connectionString);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BadgeMeUp API v1");
    c.RoutePrefix = string.Empty;
});
