using BadgeMeUp;
using BadgeMeUp.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<BadgeContext, BadgeContext>();
builder.Services.AddScoped<BadgeDb, BadgeDb>();
builder.Services.AddScoped<UserDb, UserDb>();
builder.Services.AddScoped<EmailQueueDb, EmailQueueDb>();
builder.Services.AddScoped<BadgeImageDb, BadgeImageDb>();
builder.Services.AddHttpContextAccessor();

#if DEBUG == true
builder.Services.AddScoped<ICurrentUserInfo, MockCurrentUser>();
#else
    builder.Services.AddScoped<BadgeMeUp.ICurrentUserInfo, BadgeMeUp.AppServiceCurrentUser>();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.development.json", false)
    .AddEnvironmentVariables()
    .Build();

var dbContext = new BadgeContext(config);
dbContext.Initialize();

app.Run();