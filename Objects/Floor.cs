namespace parkinglot;

class Floor
{
	public string LotId {get; set;}
	public int SlotsCount => Slots?.Count() ?? 0;
	public List<Slot>? Slots {get;set;}
	public int Number {get;set;}
}
