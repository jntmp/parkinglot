namespace parkinglot;

class Slot
{
	public VehicleType Type {get;set;}
	public Ticket? Ticket {get;set;}
	public bool Parked => Ticket != null;
	public int Number { get; set; }

	public Slot(int number)
	{
		Number = number;
	}
}
