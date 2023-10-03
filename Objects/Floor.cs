namespace parkinglot;

class Floor
{
	public string LotId {get; set;}
	public int SlotsCount => Slots?.Count() ?? 0;
	public IEnumerable<Slot>? Slots {get;set;}
	public int Number {get;set;}

	public Floor(string lotId, IEnumerable<Slot> slots)
	{
		LotId = lotId;
		Slots = slots;
	}
}
