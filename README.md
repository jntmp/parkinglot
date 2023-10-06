# parkinglot

[parkinglot]: https://workat.tech/machine-coding/practice/design-parking-lot-qm6hwq4wkhp8

A simple parking lot ticketing application, based on the [Design a Parking Lot][parkinglot] coding practice exercise

### Requirements

Create a command-line application for the parking lot system with the following requirements.

The functions that the parking lot system can do:
- Create the parking lot.
- Add floors to the parking lot.
- Add a parking lot slot to any of the floors.
- Given a vehicle, it finds the first available slot, books it, creates a ticket, parks the vehicle, and finally returns the ticket.
- Unparks a vehicle given the ticket id.
- Displays the number of free slots per floor for a specific vehicle type.
- Displays all the free slots per floor for a specific vehicle type.
- Displays all the occupied slots per floor for a specific vehicle type.

### Possible commands

Possible commands:

    create_parking_lot <parking_lot_id> <no_of_floors> <no_of_slots_per_floor>
    park_vehicle <vehicle_type> <reg_no> <color>
    unpark_vehicle <ticket_id>
    display <display_type> <vehicle_type>
        Possible values of display_type: free_count, free_slots, occupied_slots
    exit
