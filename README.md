# Softinn Hotel Booking API
by Fathur

This project is a simple Hotel Room Booking API built as part of a technical assessment for a Junior Back-end C# Developer position.

## How to Run the Project
1. Open the solution in **Visual Studio 2022** or later.
2. Run the application. Swagger UI will open automatically (`(https://localhost:7115/swagger/index.html)`).

## Features
- List all available rooms (`GET /api/rooms`)
- Book a room (`POST /api/bookings`)
- Get all bookings (`GET /api/bookings`)
- Prevent booking if the room is not available

## Bonus Feature: Prevent Overlapping Bookings
Implemented a custom feature to prevent double bookings for the same room on overlapping dates.
If a user tries to book a room that's already booked for the selected date range, an error will be returned.

## Unit Tests
Unit tests for booking logic are located in `HotelBookingAPI.Tests`. Run them with:

## Assumptions
- Once a room is booked, it's marked as unavailable.
- Each room can only have one active booking at a time.
- Bookings must have non-overlapping date ranges.

## Folder Structure
- `Controllers/` — API endpoints
- `Services/` — Business logic
- `Repositories/` — Data access logic
- `Models/` — Data models

## Author
Fathur Rahman  
[GitHub Repository](https://github.com/FathurLaw/HotelBookingAPI---Technical-Assessment)
