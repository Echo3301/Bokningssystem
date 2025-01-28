using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    //Rooms inherits interface IRoom and IListable
    public class Rooms : IRoom, IListable
    {
        private readonly InputValidation _inputValidation;
        public Rooms(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
        }

        //Properties for Rooms
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int SeatAmount { get; set; }

        //Constructor for Rooms, Json deserializer requiers an empty constructor
        protected Rooms()
        {

        }
        protected Rooms(string roomName, string roomType, int seatAmount)
        {
            RoomName = roomName;
            RoomType = roomType;
            SeatAmount = seatAmount;
        }

        //Method to create a room
        public void CreateARoom(List<Rooms> rooms)
        {

            Console.Clear();

            //Room name
            string roomName = RoomNameInput(rooms);
            //Cancels and returns to the menu if the input is "quit"
            if (roomName == null) return;


            //Room type int / list type / seat limit
            var (roomSizeSelect, roomType, seatLimit) = RoomSize();
            //Cancels and returns to the menu if the input is "quit"
            if (roomType.ToLower() == "quit") return;

            //Seats
            int seats = SeatsInput(rooms, seatLimit);
            //Cancels and returns to the menu if the input is "quit"
            if (seats == -1) return;

            //Projector and whiteboard select
            bool hasProjector = false;
            bool hasWhiteboard = false;


            //If user selects group room skip asking user for projector/whiteboard.
            if (roomSizeSelect != 3)
            {
                Console.WriteLine("Does the venue have a projector?");
                bool? projectorBool = AskUser();
                //Cancels and returns to the menu if the input is "quit"
                if (projectorBool == null) return;
                //If it is not null convert to non-null bool
                hasProjector = projectorBool.Value;

                Console.WriteLine("Does the venue have a whiteboard?");
                bool? whiteboardBool = AskUser();
                //Cancels and returns to the menu if the input is "quit"
                if (whiteboardBool == null) return;
                //If it is not null convert to non-null bool
                hasWhiteboard = whiteboardBool.Value;
            }
            //Adds to list th choosen type of venue
            if (roomSizeSelect == 1)
            {
                Console.WriteLine("\nCongratulations, a new hall has been added!");

                rooms.Add(new Hall(roomName, roomType, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSizeSelect == 2)
            {
                Console.WriteLine("\nCongratulations, a new classroom has been added!");

                rooms.Add(new Classroom(roomName, roomType, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSizeSelect == 3)
            {
                Console.WriteLine("\nCongratulations, a new group room has been added!");

                rooms.Add(new GroupRoom(roomName, roomType, seats, seatLimit));
                Save.SaveFile(rooms);
            }
        }

        //Method to take input about roomname
        private string RoomNameInput(List<Rooms> rooms)
        {
            Console.WriteLine("Enter name of the room, or type 'quit' to cancel: ");
            string tempName = Console.ReadLine().Trim();
            // If input is "quit" cancel and go back to the emnu
            if (tempName.ToLower() == "quit") return null;

            //Input validation
            while (true)
            {
                //Check if input is empty
                if (_inputValidation.IsEmpty(tempName))
                {
                    Console.WriteLine("Field cannot be empty, Try again");
                }
                //Check if input is already used
                else if (_inputValidation.IsNameUsed(rooms, tempName))
                {
                    Console.WriteLine("Name is alread used. Try again.");
                }
                else
                {
                    break;
                }
                //If input is wrong, a new input can be input
                tempName = Console.ReadLine();

                if (tempName.ToLower() == "quit") return null;
            }
            return tempName;
        }

        //Method to determine roomsize for cleaner code
        private static (int, string, int) RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Hall, 120 seat limit \n2.Classroom, 60 seat limit \n3.Group Room, 15 seat limit, or type 'quit' to cancel:");

            string? option;
            int roomSelect;
            string sizeName = "";
            int seatLimit;
            switch (option = Console.ReadLine().ToLower())
            {
                case "1":
                    roomSelect = 1;
                    sizeName = "Hall";
                    seatLimit = 120;
                    break;
                case "2":
                    roomSelect = 2;
                    sizeName = "Classroom";
                    seatLimit = 60;
                    break;
                case "3":
                    roomSelect = 3;
                    sizeName = "Group room";
                    seatLimit = 15;
                    break;
                case "quit":
                    roomSelect = 0;
                    sizeName = "quit";
                    seatLimit = 0;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please choose again.");
                    return RoomSize();
            }
            return (roomSelect, sizeName, seatLimit);
        }

        //Method for seat input for cleaner code
        private int SeatsInput(List<Rooms> rooms, int seatLimit)
        {
            Console.WriteLine($"Enter seats (Cant exceed: {seatLimit}), or type 'quit' to cancel:");
            int seatsOk;
            string tempSeatsStr = Console.ReadLine().Trim();
            if (tempSeatsStr.ToLower() == "quit") return -1;


            while (true)
            {
                //Check if input is empty

                if (_inputValidation.IsEmpty(tempSeatsStr))
                {
                    Console.WriteLine("Field cannot be empty, Try again");
                }
                //Check if input is a number
                else if (!_inputValidation.IsNumber(tempSeatsStr))
                {
                    Console.WriteLine("Input must be a number. Try again.");
                }
                //Check if the number i negative
                else if (_inputValidation.IsNumberNegative(tempSeatsStr))
                {
                    Console.WriteLine("Input must be a positive number. Try Again");
                }
                //Check if seats is larger than seat limit
                else if (_inputValidation.IsGreaterThanSeatLimit(tempSeatsStr, seatLimit))
                {
                    Console.WriteLine("Input cannot be larger than seat limit, Try Again");
                }
                else
                {
                    //Convert string to int
                    seatsOk = _inputValidation.ConvertToInt(tempSeatsStr);
                    break;
                }
                tempSeatsStr = Console.ReadLine();
                if (tempSeatsStr.ToLower() == "quit") return -1;

            }
            return seatsOk;
        }

        //Method to ask user yes or no
        private static bool? AskUser()
        {
            Console.WriteLine("Enter Y for yes, N for no, or 'quit' to cancel");
            while (true)
            {
                string? response = Console.ReadLine().ToLower();
                if (response == "quit")
                {
                    return null;
                }
                if (response == "y")
                {
                    return true;
                }
                else if (response == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Enter Y, N, or 'quit'.");
                }
            }
        }

        //Method to show all rooms + properties
        internal static void ListAll(List<Rooms> rooms, RoomsListAndSort roomsListAndSort)
        {
            Console.WriteLine("ALL ROOMS");
            Console.WriteLine("{0,-12}{1,-20}{2,-14}{3,-14}{4,-18}{5,-14}", "Type", "Name", "Seat amount", "Seat Limit", "Has Projector", "Has Whiteboard");
            Console.WriteLine(new string('-', 100));

            foreach (var room in rooms)
            {
                Console.Write($"\n{room.GetType().Name,-12}");
                Console.Write($"{room.RoomName,-20}");
                Console.Write($"{room.SeatAmount,-15}");

                if (room is Hall hall)
                {
                    Console.Write($"{hall.SeatLimit,-15}");
                    Console.Write($"{hall.HasProjector,-18}");
                    Console.Write($"{hall.HasWhiteboard,-18}");
                }
                else if (room is Classroom classroom)
                {
                    Console.Write($"{classroom.SeatLimit,-15}");
                    Console.Write($"{classroom.HasProjector,-18}");
                    Console.Write($"{classroom.HasWhiteboard,-18}");
                }
                else if (room is GroupRoom grouproom)
                {
                    Console.Write($"{grouproom.SeatLimit,-15}");
                    Console.Write($"{false,-18}");
                    Console.Write($"{false,-18}");
                }
            }
            Console.WriteLine("\n");
            roomsListAndSort.RoomsListAndSortStart(rooms);
        }

        //Method to show specific rooms
        public static String ChooseASpecificRoom(List<Rooms> rooms)
        {
            //String that gets name and is returned
            String roomName;
            //Used to display each room numbered from 1 to rooms.count
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            while (true)
            {
                Console.WriteLine("\nEnter the number for the corresponding option");
                //Input number to choose room
                string roomNum = Console.ReadLine();

                //Convert input to int
                if (int.TryParse(roomNum, out int choice))
                {
                    //Makes sure the choice of room is within the lenght span of room
                    if (choice > 0 && choice <= rooms.Count)
                    {
                        //-1 on choice so it matches indexing of lists
                        roomName = rooms[choice - 1].RoomName;
                        return roomName;
                    }
                    else { Console.WriteLine($"\n{roomNum} is not a valid option, please try again"); continue; }
                }
                else { Console.WriteLine($"\n{roomNum} is not a valid option, please try again"); continue; }
            }
        }

    }
}
