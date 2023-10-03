using System.Collections;

namespace parkinglot;

class ParkingController : IParkingController
{
	readonly IFloorRepository _floorRepository;

	public ParkingController(IFloorRepository floorRepository)
	{
		_floorRepository = floorRepository;
	}
	
	public void CreateLot(string lotId, int amount, int slotsPerFloor) 
	{
		_floorRepository.AddFloors(lotId, amount, slotsPerFloor);
	}

	public Ticket? Park(VehicleType type, string registrationNumber, string color) 
	{
		(Floor? floor, Slot? slot) = _floorRepository.FindSlot(type);
		if (slot == null) {
			return null;
		}
		return new Ticket($"{floor?.LotId}_{floor?.Number}_{slot.Number}");
	}


	// static void Unpark(int ticketId) {}

	public int GetNumberOfFreeSlots(int floor, VehicleType type) 
	{
		return _floorRepository.GetFreeSlots(floor, type)?.Count() ?? 0;
	}
	
	// static Slot[] GetFreeSlotsPerFloor(VehicleType type) {}
	// static Slot[] GetOccupiedSlotsPerFloor(VehicleType type) {}

}

internal interface IParkingController
{
	void CreateLot(string lotId, int amount, int slotsPerFloor);
	Ticket? Park(VehicleType type, string registrationNumber, string color);
	int GetNumberOfFreeSlots(int floor, VehicleType type);
}