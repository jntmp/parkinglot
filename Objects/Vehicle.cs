namespace parkinglot;

class Vehicle
{
	public VehicleType Type {get;set;}
	public string RegistrationNumber {get;set;}
	public string Color {get;set;}
}

enum VehicleType { Car, Bike, Truck }