using parkinglot;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(o => {
	o.AddSingleton<IFloorRepository, FloorRepository>();
	o.AddScoped<IParkingController, ParkingController>();
	o.AddScoped<UserInputHandler>();
});

var app = builder.Build();

var userInputHandler = app.Services.GetRequiredService<UserInputHandler>();

userInputHandler.Init();

