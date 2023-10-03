using System.Net.Http.Headers;

namespace parkinglot;

class UserInputHandler
{
	readonly IParkingController _parkingController;

	public UserInputHandler(IParkingController parkingController)
	{
		_parkingController = parkingController;
	}

	public void Init() {
		bool awaitingUserInput = true;

		while(awaitingUserInput) {
			string? input = Console.ReadLine();
			string[]? arr = input?.Split(' ');
			string? command = arr[0];
			var validator = new UserInputValidator();

			switch(command) {
				case "create_parking_lot":
					(string lotId, int floors, int slots) = validator.ValidateCreateParkingLot(arr[1], arr[2], arr[3]);
					_parkingController.CreateLot(lotId, floors, slots);
					break;
				case "park_vehicle":
					(VehicleType vehicleType, string registrationNumber, string color) = validator.ValidateParkVehicle(arr[1], arr[2], arr[3]);
					Ticket? ticket = _parkingController.Park(vehicleType, registrationNumber, color);
					if (ticket == null) {
						Console.WriteLine("Parking Lot Full");
						break;
					}
					Console.WriteLine($"Ticket ID: {ticket.Id}");
					break;
				case "unpark_vehicle":
					break;
				case "display" when arr[1] == "free_count":
					Console.WriteLine(_parkingController.GetNumberOfFreeSlots(Int32.Parse(arr[2]), Enum.Parse<VehicleType>(arr[3])));
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