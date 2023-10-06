namespace parkinglot.Request;

class CreateParkingLotRequest
{
	public string LotId { get; set; }
	public int NoOfFloors { get; set; }
	public int NoOfSlotsPerFloor { get; set; }
}