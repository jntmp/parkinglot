namespace parkinglot;

class ParkingController : IParkingController
{
	readonly IParkingLotDataContext _parkingLotDataContext;

	public ParkingController(IParkingLotDataContext parkingLotDataContext)
	{
		_parkingLotDataContext = parkingLotDataContext;
	}
	
	public void CreateLot(CreateParkingLotRequest createParkingLotRequest) 
	{
		_parkingLotDataContext.AddFloors(
			createParkingLotRequest.LotId, 
			createParkingLotRequest.NoOfFloors, 
			createParkingLotRequest.NoOfSlotsPerFloor);
	}

	public Ticket? ParkVehicle(ParkVehicleRequest parkVehicleRequest) 
	{
		return _parkingLotDataContext.Park(parkVehicleRequest.VehicleType);
	}

	public void Unpark(string ticketId)
	{
		_parkingLotDataContext.Unpark(ticketId);
	}

	public int[] GetNumberOfFreeSlots(VehicleTypeEnum vehicleTypeEnum) 
	{
		List<int> counts = new List<int>();
		for(int i = 1; i <= _parkingLotDataContext.GetFloorCount(); i++) 
		{
			counts.Add(_parkingLotDataContext.GetFreeSlots(i, vehicleTypeEnum)?.Count() ?? 0);
		}
		return counts.ToArray();
	}

	// static Slot[] GetFreeSlotsPerFloor(VehicleType type) {}
	// static Slot[] GetOccupiedSlotsPerFloor(VehicleType type) {}

}

internal interface IParkingController
{
	void CreateLot(CreateParkingLotRequest createParkingLotRequest);
	Ticket? ParkVehicle(ParkVehicleRequest parkVehicleRequest);
	void Unpark(string ticketId);
	int[] GetNumberOfFreeSlots(VehicleTypeEnum vehicleTypeEnum);
}