using parkinglot.Model;

namespace parkinglot.Request;

public class ParkVehicleRequest
{
	public VehicleTypeEnum VehicleType {get;set;}
	public string RegistrationNumber {get;set;}
	public string Color {get;set;}
}
