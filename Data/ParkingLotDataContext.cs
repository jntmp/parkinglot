
namespace parkinglot;

class ParkingLotDataContext : IParkingLotDataContext
{
	private List<Floor> _floors = new List<Floor>();

	public void AddFloors(string lotId, int amount, int slotsPerFloor)
	{
		for (int i = 1; i <= amount; i++) {
			var floorSlots = PopulateSlots(slotsPerFloor, (slotNumber) => {
					if (slotNumber == 1) return VehicleTypeEnum.Truck;
					if (slotNumber <= 3) return VehicleTypeEnum.Bike;
					return VehicleTypeEnum.Car;
				});

			var floor = new Floor {
				LotId = lotId,
				Slots = floorSlots.ToList(),
				Number = i
			};
			_floors.Add(floor);
		}
	}

	public Ticket? Park(VehicleTypeEnum type)
	{
		foreach (var floor in _floors)
		{
			var slot = floor.Slots?.FirstOrDefault(s => !s.Parked && s.Type.Equals(type));

			if (slot != null) {
				slot.Ticket = new Ticket($"{floor.LotId}_{floor.Number}_{slot.Number}");
				return slot.Ticket;
			}
		}
		return null;
	}

	public void Unpark(string ticketId)
	{
		var slot = _floors.SelectMany(f => f.Slots).FirstOrDefault(s => s.Ticket?.Id == ticketId);

		if (slot != null) {
			slot.Ticket = null;
		}
	}

	public int GetFloorCount() => _floors.Count;

	public IEnumerable<Slot> GetFreeSlots(int floor, VehicleTypeEnum type)
	{
		return _floors[floor - 1].Slots.Where(s => !s.Parked && s.Type.Equals(type));
	}

	static IEnumerable<Slot> PopulateSlots(int count, Func<int, VehicleTypeEnum> assignType) {
		int num = 1;
		while (num <= count) {
			yield return new Slot(num) {
				Type = assignType.Invoke(num)
			};
			num++;
		}
	}
}

internal interface IParkingLotDataContext
{
	void AddFloors(string lotId, int amount, int slotsPerFloor);
	Ticket? Park(VehicleTypeEnum vehicleTypeEnum);
	void Unpark(string ticketId);
	IEnumerable<Slot> GetFreeSlots(int floor, VehicleTypeEnum vehicleTypeEnum);
	int GetFloorCount();
}