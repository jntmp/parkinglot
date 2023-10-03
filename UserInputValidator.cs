namespace parkinglot;

class UserInputValidator
{
	public (string lotId, int floors, int slots) ValidateCreateParkingLot(string lotId, string floorsVal, string slotsVal)
	{
		if (String.IsNullOrEmpty(lotId)
			|| !Int32.TryParse(floorsVal, out int floors)
			|| !Int32.TryParse(slotsVal, out int slots)) {
				throw new ArgumentException("Invalid argument");
			}
			return (lotId, floors, slots);
	}

	public (VehicleType vehicleType, string registrationNumber, string color) ValidateParkVehicle(string vehicleType, string registrationNumber, string color)
	{
		if (!Enum.IsDefined(typeof(VehicleType), vehicleType)
			|| string.IsNullOrEmpty(registrationNumber)
			|| string.IsNullOrEmpty(color)) {
				throw new ArgumentException("Invalid argument");
			}
			return (Enum.Parse<VehicleType>(vehicleType), registrationNumber, color);
	}
}