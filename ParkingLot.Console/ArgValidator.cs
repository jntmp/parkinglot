namespace parkinglot;

class ArgValidator
{
	public static bool ForCreateLot(out CreateParkingLotRequest createParkingLotRequest, params string[] args)
	{
		if (String.IsNullOrEmpty(args[0])
			|| !Int32.TryParse(args[1], out int floors)
			|| !Int32.TryParse(args[2], out int slots)) {
				createParkingLotRequest = null;
				return false;
			}
			createParkingLotRequest = new CreateParkingLotRequest { LotId = args[0], NoOfFloors = floors, NoOfSlotsPerFloor = slots };
			return true;
	}

	public static bool ForParkVehicle(out ParkVehicleRequest parkVehicleRequest, params string[] args)
	{
		if (!Enum.IsDefined(typeof(VehicleTypeEnum), args[0])
			|| string.IsNullOrEmpty(args[1])
			|| string.IsNullOrEmpty(args[2])) {
				parkVehicleRequest = null;
				return false;
			}
			parkVehicleRequest = new ParkVehicleRequest {
				VehicleType = Enum.Parse<VehicleTypeEnum>(args[0]),
				RegistrationNumber = args[1],
				Color = args[2]
			};
			return true;
	}

	public static bool ForDisplayFreeSlots(out VehicleTypeEnum vehicleTypeEnum, string vehicleType)
	{
		if (!Enum.IsDefined(typeof(VehicleTypeEnum), vehicleType)) {
			vehicleTypeEnum = VehicleTypeEnum.Car;
			return false;
		}
		vehicleTypeEnum = Enum.Parse<VehicleTypeEnum>(vehicleType);
		return true;
	}

	public static bool ForUnparkVehicle(out string ticketId, string arg)
	{
		if (string.IsNullOrWhiteSpace(arg)) {
			ticketId = "";
			return false;
		}
		ticketId = arg;
		return true;
	}
}