using patching_api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var system_data_manager = new SystemDataManager();

app.MapGet("/regions", () =>
{
    system_data_manager.buildFullData();
    return Results.Ok(system_data_manager.full_data);
});

app.MapGet("/region/{ name:string }", (string name) =>
{
    var region = system_data_manager.systems_by_region[name];
    return Results.Ok(region);
});

app.MapGet("/health", () =>
{
    system_data_manager.buildFullData();
    return Results.StatusCode(StatusCodes.Status200OK);
});

app.Run();

