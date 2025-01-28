# Bokningssystem
Our first real group Submission: Booking system for school premises.

Inlämningsuppgift: Bokningssystem för skolans lokaler
Programmering i C#
Introduktion

I denna inlämningsuppgift ska ni utveckla ett bokningssystem för skolans olika lokaler. Systemet ska hantera bokning av både salar och grupprum. Applikationen ska byggas med objektorienterade principer och moderna C#-funktioner. Viss data ska persisteras mellan körningar genom att sparas i filer(detta kommer Fredrik att gå igenom senare).
Tekniska krav

Följande koncept ska implementeras i lösningen:

    Objektorienterad programmering

    Använd inkapsling med properties
    Implementera konstruktorer på lämpligt sätt

    Arv

    Skapa en basklass Lokal med gemensamma egenskaper och metoder
    Skapa minst två ärvande klasser: Sal och Grupprum med specifika egenskaper
    Använd override för att anpassa metoder i ärvande klasser

    Interface

    Skapa ett IBookable interface med metoder för bokning
    Implementera interfacet i relevanta klasser
    Använd interface som returtyp där lämpligt

    DateTime

    Hantera start- och sluttid för bokningar
    Formatera datum och tid på användarvänligt sätt

    TimeSpan

    Längd på bokning

    Listor och Collections

    Använd List<T> för att lagra bokningar och lokaler när programmet körs.
    Implementera operationer för filtrering och sökning
    Hantera sortering av bokningar

    Filhantering

    Implementera persistens genom att spara lokalerna i fil.
    Läs in data när programmet startar

Kundkrav
Bokningar

    Som användare vill jag kunna skapa skapa ny bokningar
    Som användare vill jag kunna lista alla bokningar
    Som användare vill jag kunna uppdatera en bokning
    Som användare vill jag kunna ta bort en bokning
    Som användare vill jag kunna lista bokningar från ett specifikt år.

Lokaler

    Som användare vill jag kunna lista alla salar, med lämpliga egenskaper listade
    Som användare vill jag förhindra att salar har samma namn
    Som användare vill jag att alla salar ska lagras i en fil mellan programstarter
    Som användare vill jag kunna skapa nya salar.

Bedömningskriterier

    Korrekt implementering av alla tekniska krav
    Implementering av alla kundkrav
    Väl strukturerad och läsbar kod
    Väl strukturerad mappstruktur
    Effektiv felhantering och validering
    Dokumentation och kommentarer


Booking System for School's Rooms
Created by: Angelica Bergström, Adam Axelsson-Hedman, Alex Bullerjahn & David Berglin

Overview
Our booking system is designed to handle bookings for the school's various rooms, including classrooms, halls, and group rooms. The system allows users to create, book, list, update, and delete bookings. The program uses object-oriented principles and modern features in C#, ensuring that data is saved between runs by using JSON files for persistence.

Features

    Create Rooms: Ability to create new rooms with specific information, such as name, type, number of seats, and equipment (e.g., projector or whiteboard).
    Book Rooms: Users can book rooms by specifying start and end times, room type, and email address. The system checks that the booking does not overlap with other bookings.
    List All Rooms: Display all rooms and their properties, with options to sort and filter by various criteria.
    List All Bookings: Display all bookings, with options to filter by year or specific room.
    Update Booking: Ability to update an existing booking by overwriting the previous one.
    Delete Booking: Ability to delete a booking based on room and email address.
    Exit the Program: The program exits and saves any changes.

Installation

    Clone or download the project to your local machine.
    Open the project in Visual Studio.
    Run the project in Visual Studio to start the program.

Usage
When the program starts, the main menu with the following options will appear:

Main Menu:

    Create a Room
        Enter the room name (if the name already exists, a new name must be provided).
        Select the room type (e.g., group room, classroom, hall).
        Enter the number of seats (e.g., group rooms have a maximum of 15 seats, classrooms have a maximum of 60 seats, and halls have a maximum of 120 seats).
        For classrooms or halls, you can also specify if the room has a projector and whiteboard.

    Book a Room
        Choose the room type (group room, classroom, or hall) to book.
        Enter your email address, start date and time, and end date and time for the booking.
        The booking cannot be made if the start or end time has already passed or if the booking exceeds 24 hours.
        If the booking overlaps with an existing booking, the system will display information about the conflicting bookings and allow the user to attempt the booking again.

    List All Rooms
        List all rooms and their properties, with the following sorting options:
            Show all rooms by type in alphabetical order
            Show halls
            Show classrooms
            Show group rooms
            Show rooms by number of seats (descending order)
            Show rooms by number of seats (ascending order)
            Show rooms with a projector
            Show rooms with a whiteboard
        Go back

    List All Bookings
        Display all bookings, including email address, booking time, and room type.
        Submenu to filter by:
            Show bookings for a specific year.
            Show bookings for a specific room.

    Update Booking
        List all current bookings.
        Choose which booking to update.
        Choose a new room for the booking.
        Enter your email address.
        Enter the new times for the booking.

    Delete Booking
        Choose room.
        Choose which booking to delete.

    Exit
        Exit the program.

Known Limitations

    When a booking is updated through the "Update Booking" menu option, the old booking is deleted before the new booking is saved. If you change your mind during the update, the original booking will be lost.
    RoomsConverter could be modified to use the IRoom interface instead of the Rooms class.

Implementation Choices and Rationale

    The program uses an input validation method to ensure that the user does not enter incorrect or invalid data, which prevents the program from crashing or producing incorrect results.

File Format and Structure

    All rooms and bookings are stored in JSON files to ensure data persists between runs.

Project Folder Structure

    BookingSystemGroup6/: Main folder with all files and documents in the project.
    BookingSystemGroup6/: Folder containing the project's code files used to run the project.
    Classes/: Folder with classes that perform various functions in the booking system.
    RoomClasses/: Folder with classes related to rooms and facilities.
        Classroom.cs: Contains code for classrooms, inherits from the Rooms class.
        GroupRoom.cs: Contains code for group rooms, inherits from the Rooms class.
        Hall.cs: Contains code for halls, inherits from the Rooms class.
        Rooms.cs: Base class with properties and methods for rooms (e.g., creating, viewing rooms). Room data is saved in a JSON file.
        RoomsListAndSort.cs: Code for sorting and displaying rooms in different orders, such as number of seats or room properties.
        Bookings.cs: Contains methods for booking, modifying, deleting, and displaying bookings. Saves bookings in a JSON file.
        InputValidation.cs: Methods for input validation, e.g., checking numerical values, input format, etc.
        Menu.cs: Main menu that is displayed when the program starts and shows the functions that can be performed.
        RoomsConverter.cs: Handles polymorphic serialization and deserialization to create a list where different subclasses, which inherit from a common class, can store varying amounts of data. It is used to read and write to JSON files.
        Save.cs: Handles reading and saving object lists (rooms and bookings) to JSON files.
    Interfaces: Folder for interface files.
        IListable.cs: Interface that defines methods used in several classes.
        IRoom.cs: Interface that defines properties for rooms.
    BookingSystemGroup6.csproj: Project file for the build process.
    Programs.cs: Start file, creates objects and lists used in other files. Deserializes the JSON files and then starts the MainMenu method.
    .gitignore: Gitignore file with default settings for a Visual Studio project.
    BookingSystemGroup6.sln: Solution file for the entire C# project.
    README.md: Project description and instructions for running the program.

JSON Files

    RoomList.json: Rooms and their data.
    BookingList.json: Bookings and their data.

Which Student Had Primary Responsibility for Which Parts
With great teamwork, we tackled most of it together.

Thank You and Goodbye!
Thank you for using our booking system! We hope it will be useful for managing the school's rooms in an efficient and simple way.
