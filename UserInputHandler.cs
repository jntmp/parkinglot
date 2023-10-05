using System.Net.Http.Headers;

namespace parkinglot;

class UserInputHandler
{
	readonly IParkingController _parkingController;
	const string CREATE_PARKING_LOT = "create_parking_lot";
	const string PARK_VEHICLE = "park_vehicle";
	const string UNPARK_VEHICLE = "unpark_vehicle";
	const string DISPLAY = "display";
	const string FREE_COUNT = "free_count";

	public UserInputHandler(IParkingController parkingController)
	{
		_parkingController = parkingController;
	}

	public void Init() {
		bool awaitingUserInput = true;

		while(awaitingUserInput) {
			string[]? inputArgs = Console.ReadLine()?.Split(' ');
			
			string? command = inputArgs.First();
			string[]? args = inputArgs.Skip(1).ToArray();

			switch(command) {
				case CREATE_PARKING_LOT:
					if (!ArgValidator.ForCreateLot(out CreateParkingLotRequest createParkingLotRequest, args)) {
						Console.WriteLine("Invalid argument(s)");
						continue;
					}
					_parkingController.CreateLot(createParkingLotRequest);
					break;
				case PARK_VEHICLE:
					if (!ArgValidator.ForParkVehicle(out ParkVehicleRequest parkVehicleRequest, args)) {
						Console.WriteLine("Invalid argument(s)");
						continue;
					}
					var ticket = _parkingController.ParkVehicle(parkVehicleRequest);
					if (ticket == null) {
						Console.WriteLine("Parking Lot Full");
						break;
					}
					Console.WriteLine($"Ticket ID: {ticket.Id}");
					break;
				case UNPARK_VEHICLE:
					if (!ArgValidator.ForUnparkVehicle(out string ticketId, args[0])) {
						Console.WriteLine("Invalid argument(s)");
						continue;
					}
					_parkingController.Unpark(ticketId);
					break;
				case DISPLAY when args.Length > 0 && args[0] == FREE_COUNT:
					if (!ArgValidator.ForDisplayFreeSlots(out VehicleTypeEnum vehicleTypeEnum, args[1])) {
						Console.WriteLine("Invalid argument(s)");
						continue;
					}
					int i = 1;
					Array.ForEach(_parkingController.GetNumberOfFreeSlots(vehicleTypeEnum), s => {
						Console.WriteLine($"No. of free slots for {args[1]} slots on Floor {i}: {s}");
						i++;
					});
					break;
				case "exit":
					awaitingUserInput = false;
					break;
				default:
					Console.WriteLine($"Unknown command '{command}'");
					break;
			}
		}

		Console.WriteLine($"Program exited");
	}
}