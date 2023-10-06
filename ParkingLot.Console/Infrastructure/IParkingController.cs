using parkinglot.Model;
using parkinglot.Request;

namespace parkinglot.Infrastructure;

internal interface IParkingController
{
	void CreateLot(CreateParkingLotRequest createParkingLotRequest);
	Ticket? ParkVehicle(ParkVehicleRequest parkVehicleRequest);
	void Unpark(string ticketId);
	List<int> GetNumberOfFreeSlots(VehicleTypeEnum vehicleTypeEnum);
	List<int[]> GetSlots(VehicleTypeEnum vehicleTypeEnum, bool parked);
}