var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<BadgeMeUp.Db.BadgeContext, BadgeMeUp.Db.BadgeContext>();
builder.Services.AddScoped<BadgeMeUp.Db.BadgeDb, BadgeMeUp.Db.BadgeDb>();
builder.Services.AddScoped<BadgeMeUp.Db.UserDb, BadgeMeUp.Db.UserDb>();
builder.Services.AddScoped<BadgeMeUp.Db.EmailQueueDb, BadgeMeUp.Db.EmailQueueDb>();
builder.Services.AddHttpContextAccessor();



#if DEBUG == true
    builder.Services.AddScoped<BadgeMeUp.ICurrentUserInfo, BadgeMeUp.MockCurrentUser>();
#else
    builder.Services.AddScoped<BadgeMeUp.ICurrentUserInfo, BadgeMeUp.AppServiceCurrentUser>();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


var dbContext = new BadgeMeUp.Db.BadgeContext(config);
BadgeMeUp.Db.DbInitializer.Initialize(dbContext);

app.Run();
