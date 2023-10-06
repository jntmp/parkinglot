using parkinglot;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using parkinglot.Controller;
using parkinglot.Infrastructure;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(o => {
	o.AddSingleton<IParkingLotDataContext, ParkingLotDataContext>();
	o.AddScoped<IParkingController, ParkingController>();
	o.AddScoped<UserInputHandler>();
});

var app = builder.Build();

var userInputHandler = app.Services.GetRequiredService<UserInputHandler>();

userInputHandler.Init();
