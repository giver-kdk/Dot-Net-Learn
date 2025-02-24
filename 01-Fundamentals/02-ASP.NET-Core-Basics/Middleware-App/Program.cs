var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// *************** Custom Middleware 1 for Home Page ***************
app.Use(async (context, next) => {
    if (context.Request.Path == "/")        // Log for home page only
    {
        DateTime startTime = new DateTime();

        await next.Invoke(context);         // Call the next middleware

        // Logic executed after returning back from other follow-up middleware
        TimeSpan duration = DateTime.Now - startTime;
        app.Logger.LogInformation($"Home Request path: {context.Request.Path}");
        app.Logger.LogInformation($"Home Request took: {duration}");
    }
    else
    {
        await next.Invoke(context);         // Skip logging for other requests
    }
});
// *************** Custom Middleware 2 for News and Blog Page ***************
app.Use(async (context, next) => {
    bool isBlog = context.Request.Path.StartsWithSegments("/blog");
    bool isNews = context.Request.Path.StartsWithSegments("/news"); 
    if (isBlog || isNews)                   // Log for blog or news page only
    {
        DateTime startTime = new DateTime();

        await next.Invoke(context);         // Call the next middleware

        // Logic executed after returning back from other follow-up middleware
        TimeSpan duration = DateTime.Now - startTime;
        app.Logger.LogInformation($"Request path: {context.Request.Path}");
        app.Logger.LogInformation($"Request took: {duration}");
    }
    else
    {
        await next.Invoke(context);         // Skip logging for other requests
    }
});


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ************* Custom Conventional Routing *************
app.MapControllerRoute(
    name: "news",
    pattern: "news/article",
    defaults: new { controller = "News", action = "Article"
 });
// ************* Custom Conventional Routing *************
app.MapControllerRoute(
    name: "blog",
    // The pattern {*article} accepts 0 or more characters and maps to 'Article' action
    pattern: "blog/{*article}",
    defaults: new
    {
        controller = "Blog",
        action = "Article"
    });

app.Run();
