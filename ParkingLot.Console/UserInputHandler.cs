using parkinglot.Infrastructure;
using parkinglot.Model;
using parkinglot.Request;

namespace parkinglot;

class UserInputHandler
{
	readonly IParkingController _parkingController;
	const string CREATE_PARKING_LOT = "create_parking_lot";
	const string PARK_VEHICLE = "park_vehicle";
	const string UNPARK_VEHICLE = "unpark_vehicle";
	const string DISPLAY = "display";
	const string FREE_COUNT = "free_count";
	const string FREE_SLOTS = "free_slots";
	const string OCCUPIED_SLOTS = "occupied_slots";

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
						PrintInvalidArguments();
						continue;
					}
					_parkingController.CreateLot(createParkingLotRequest);
					break;
				case PARK_VEHICLE:
					if (!ArgValidator.ForParkVehicle(out ParkVehicleRequest parkVehicleRequest, args))
					{
						PrintInvalidArguments();
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
						PrintInvalidArguments();
						continue;
					}
					_parkingController.Unpark(ticketId);
					break;
				case DISPLAY:
					if (!ArgValidator.ForDisplayFreeSlots(out VehicleTypeEnum vehicleTypeEnum, args[1]))
					{
						PrintInvalidArguments();
						continue;
					}
					DisplaySlotStatuses(args, vehicleTypeEnum);
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

	private void DisplaySlotStatuses(string[] args, VehicleTypeEnum vehicleTypeEnum)
	{
		switch (args[0])
		{
			case FREE_COUNT:
				int i = 1;
				_parkingController.GetNumberOfFreeSlots(vehicleTypeEnum).ForEach(s =>
				{
					Console.WriteLine($"No. of free slots for {args[1]} slots on Floor {i}: {s}");
					i++;
				});
				break;
			case FREE_SLOTS:
			case OCCUPIED_SLOTS:
				int j = 1;
				bool occupied = args[0] == OCCUPIED_SLOTS;
				_parkingController.GetSlots(vehicleTypeEnum, occupied).ForEach(slots =>
				{
					Console.WriteLine($"{(occupied ? "Occupied" : "Free")} slots for {vehicleTypeEnum} on Floor {j}: {String.Join(",", slots)}");
					j++;
				});
				break;
		}
	}

	private static void PrintInvalidArguments()
	{
		Console.WriteLine("Invalid argument(s)");
	}
}