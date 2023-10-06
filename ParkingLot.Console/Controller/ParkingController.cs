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

	public List<int> GetNumberOfFreeSlots(VehicleTypeEnum vehicleTypeEnum) 
	{
		List<int> counts = new List<int>();
		for(int i = 1; i <= _parkingLotDataContext.GetFloorCount(); i++) 
		{
			counts.Add(_parkingLotDataContext.GetSlots(i, vehicleTypeEnum)?.Count() ?? 0);
		}
		return counts;
	}

	public List<int[]> GetSlots(VehicleTypeEnum vehicleTypeEnum, bool parked) 
	{
		List<int[]> slots = new List<int[]>();
		for(int i = 1; i <= _parkingLotDataContext.GetFloorCount(); i++) 
		{
			slots.Add(_parkingLotDataContext.GetSlots(i, vehicleTypeEnum, parked).Select(s => s.Number).ToArray());
		}
		return slots;
	}
}

internal interface IParkingController
{
	void CreateLot(CreateParkingLotRequest createParkingLotRequest);
	Ticket? ParkVehicle(ParkVehicleRequest parkVehicleRequest);
	void Unpark(string ticketId);
	List<int> GetNumberOfFreeSlots(VehicleTypeEnum vehicleTypeEnum);
	List<int[]> GetSlots(VehicleTypeEnum vehicleTypeEnum, bool parked);
}