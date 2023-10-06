using parkinglot.Model;

namespace parkinglot.Infrastructure;

internal interface IParkingLotDataContext
{
	void AddFloors(string lotId, int amount, int slotsPerFloor);
	Ticket? Park(VehicleTypeEnum vehicleTypeEnum);
	void Unpark(string ticketId);
	IEnumerable<Slot> GetSlots(int floor, VehicleTypeEnum vehicleTypeEnum, bool parked = true);
	int GetFloorCount();
}