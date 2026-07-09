using RentalDesk.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRentalService, RentalService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

SeedData(app.Services);

app.Run();

static void SeedData(IServiceProvider services)
{
    var rentalService = services.GetRequiredService<IRentalService>();

    rentalService.BookRental(new RentalDesk.Models.CreateRentalRequest
    {
        CustomerName = "Dana Whitfield",
        VehicleClass = "Compact",
        PickupDate = DateOnly.FromDateTime(DateTime.Today),
        ReturnDate = DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
        DailyRate = 42.00m
    });

    rentalService.BookRental(new RentalDesk.Models.CreateRentalRequest
    {
        CustomerName = "Marcus Lindberg",
        VehicleClass = "SUV",
        PickupDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
        ReturnDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
        DailyRate = 78.50m
    });

    rentalService.BookRental(new RentalDesk.Models.CreateRentalRequest
    {
        CustomerName = "Priya Anand",
        VehicleClass = "Van",
        PickupDate = DateOnly.FromDateTime(DateTime.Today),
        ReturnDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
        DailyRate = 95.00m
    });
}
