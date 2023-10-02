using parkinglot;

bool awaitingUserInput = true;

while(awaitingUserInput) {
	string? input = Console.ReadLine();
	string[]? arr = input?.Split(' ');
	string? command = arr[0];

	switch(command) {
		case "create_parking_lot":
			Actions.CreateLot(arr[1], Int32.Parse(arr[2]), Int32.Parse(arr[3]));
			break;
		case "park_vehicle":
			Ticket? ticket = Actions.Park(Enum.Parse<VehicleType>(arr[1]), arr[2], arr[3]);
			if (ticket == null) {
				Console.WriteLine("Parking Lot Full");
				break;
			}
			Console.WriteLine($"Ticket ID: {ticket.Id}");
			break;
		case "unpark_vehicle":
			break;
		case "display":
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
