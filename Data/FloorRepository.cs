
namespace parkinglot;

class FloorRepository : IFloorRepository
{
	private List<Floor> _floors = new List<Floor>();

	public void AddFloors(string lotId, int amount, int slotsPerFloor)
	{
		var slots = PopulateSlots(slotsPerFloor, (slotNumber) => {
			if (slotNumber == 1) return VehicleType.Truck;
			if (slotNumber <= 3) return VehicleType.Bike;
			return VehicleType.Car;
		});
		
		var floorTemplate = new Floor(lotId, slots);
		
		_floors.AddRange(PopulateFloors(floorTemplate, amount));
	}

	public (Floor? floor, Slot? slot) FindSlot(VehicleType type)
	{
		foreach (var floor in _floors)
		{
			var slot = floor.Slots?.FirstOrDefault(s => !s.Parked && s.Type.Equals(type));
			if (slot != null) 
			{
				return (floor, slot);
			}
		}
		return (null, null);
	}

	public IEnumerable<Slot> GetFreeSlots(int floor, VehicleType type)
	{
		return _floors[floor - 1].Slots.Where(s => !s.Parked && s.Type.Equals(type));
	}

	static IEnumerable<Slot> PopulateSlots(int count, Func<int, VehicleType> assignType) {
		int num = 1;
		while (num <= count) {
			yield return new Slot(num) {
				Type = assignType.Invoke(num)
			};
			num++;
		}
	}

	static IEnumerable<Floor> PopulateFloors(Floor floorTemplate, int count) {
		int num = 1;
		while (num <= count) {
			floorTemplate.Number = num;
			yield return floorTemplate;
			num++;
		}
	}
}

internal interface IFloorRepository
{
	void AddFloors(string lotId, int amount, int slotsPerFloor);
	(Floor? floor, Slot? slot) FindSlot(VehicleType type);
	IEnumerable<Slot> GetFreeSlots(int floor, VehicleType type);
}