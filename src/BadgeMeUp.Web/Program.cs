var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<BadgeMeUp.Db.BadgeContext, BadgeMeUp.Db.BadgeContext>();
builder.Services.AddScoped<BadgeMeUp.Db.BadgeDb, BadgeMeUp.Db.BadgeDb>();
builder.Services.AddScoped<BadgeMeUp.Db.UserDb, BadgeMeUp.Db.UserDb>();

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

var dbContext = new BadgeMeUp.Db.BadgeContext();
BadgeMeUp.Db.DbInitializer.Initialize(dbContext);

app.Run();
