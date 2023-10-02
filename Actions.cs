using System.Collections;

namespace parkinglot;

class Actions
{
	static IEnumerable<Floor> _floors {get;set;}
	public static void CreateLot(string lotId, int floors, int slotsPerFloor) 
	{
		var floorTemplate = new Floor() {
			LotId = lotId,
			Slots = PopulateSlots(slotsPerFloor)
		};
		_floors =	PopulateFloors(floorTemplate, floors);
	}

	public static Ticket? Park(VehicleType type, string registrationNumber, string color) 
	{
		foreach(var floor in _floors) {
			var openSlot = floor.Slots?.FirstOrDefault(s => !s.Parked && s.Type.Equals(type));
			if (openSlot == null) {
				continue;
			}
			openSlot.Ticket = new Ticket($"{floor.LotId}_{floor.Number}_{openSlot.Number}");
			return openSlot.Ticket;
		}
		return null;
	}

	static IEnumerable<Slot> PopulateSlots(int count) {
		int num = 1;
		while (num <= count) {
			yield return new Slot(num);
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
	// static void Unpark(int ticketId) {}

	// static int GetNumberOfFreeSlotsPerFloor(VehicleType type) {}
	// static Slot[] GetFreeSlotsPerFloor(VehicleType type) {}
	// static Slot[] GetOccupiedSlotsPerFloor(VehicleType type) {}

}