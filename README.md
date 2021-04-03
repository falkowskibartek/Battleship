# Battleship
Battleship Game with simple Console UI.

# Architecture Decision Records
- Console App in .NET Core 3.1
- Three projects in the solution:
  - Battleship - application root and application logic - managing the ui and interaction with user
  - Battleship.Domain - domain logic
  - Battleship.Tests - unit tests for domain logic
- No external configuration (db or file). Grid size is managed by Grid type. Fleet size is managed by OceanGrid type. I made an assumption that width cannot be greater than two digit number.
- Flattening layers - no dedicated UI or presentation layer. No infrastructure layer.
- UI is as simple as possible.
- No external libraries for validation, dependency injection container etc. Application is really simple.
- Domain logic is based on https://www.hasbro.com/common/instruct/Battleship.PDF. Ubiquitous language used from this document (Holes, Pegs, Coordinate, TargetGrid, OceanGrid etc).
- Coordinate type consists of two equivalent objects CoordinateValue and CoordinateIndexes. It was much easier to create and manipulate coordinates with them. Coordinate types ensures that both of them point to the same location.
    - CoordinateValue - value presented on UI (eg "A1"). User creates such coordinate when entering a shot location.
    - CoordinateIndexes is a set of indexes that describe that value (eg {0,0}). Positioners of fleet use it to randomly create ships locations.
 - I divided domain into two different areas - OceanGrid and TargetGrid. Both of them wrap Coordinate into Hole type (OceanGridHole, TargetGridHole).
    - OceanGrid - it's a grid that consists of randomly positioned computer's fleet. User's goal is to shoot the entire fleet. Object takes the shots and returns the information about result.
    - TargetGrd - it's a grid that consists of pegs with user's shots. Each shot is marked on this object. It also delivers a method for visualising the grid on console.
 - I didn't add any specific factorie for creating objects. Each object knows it's data and invariants so I believe that by default this is the best place to create operation. The logic is quite simple and I used "static factory methods".
 - There are two types that are reponsible for positioning on OceanGrid - FleetPositioner and ShipPositioner. The algorithms are simple and do not aim for any high efficiency results :)
 - I didn't create any integration tests - there are no integrations :) Only unit tests.
 - Testing with XUnit framework and FluentAssertion library. Test methods names in given-when-then approach.
 
# Getting Started
- Install .NET Core 3.1 SDK - https://dotnet.microsoft.com/download/dotnet/3.1
- Build project Battleship (you can do it in Visual Studio, Visual Studio Code or manually by dotnet build)
- Run Battleship.exe

# Usage
- The program creates a 10x10 grid and place three ships on the grid at random. Your goal is to sink all the ships.
- Call out the shot location by entering it on the console - place height and width coordinates, eg A1 or J10.
- Program will tell you when the entire fleet is sunk :)

 
