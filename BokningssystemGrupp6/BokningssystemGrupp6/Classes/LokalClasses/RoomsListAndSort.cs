using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class RoomsListAndSort
    {

        private readonly InputValidation _inputValidation;
        private int sortOption;
        public RoomsListAndSort(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
            //Default value, needed for menu selection validation
            sortOption = -1;
        }

        //Method to list and sort rooms
        public void RoomsListAndSortStart(List<Rooms> rooms)
        {

            //Menu options
            Console.WriteLine("" +
                "Select list option:" +
                "\n1. View all rooms by type in alphabetical order" +
                "\n2. Show halls" +
                "\n3. Show classrooms" +
                "\n4. Show group rooms" +
                "\n5. Show rooms by number of seats, descending order" +
                "\n6. Show rooms by number of seats, rising order" +
                "\n7. Show rooms with projector" +
                "\n8. Show rooms with whiteboard" +
                "\n0. Go back\n");

            string? menuChoice;
            //Reset option before starting the loop
            sortOption = -1;

            //Loop for valid user input
            while (true)
            {
                Console.Write("Enter your choice: ");
                menuChoice = Console.ReadLine();
                //Input validation
                if (_inputValidation.IsEmpty(menuChoice))
                {
                    Console.WriteLine("Field cannot be empty. Try again.");
                    continue;
                }
                else if (!_inputValidation.IsNumber(menuChoice))
                {
                    Console.WriteLine("Input must be a number. Try again.");
                    continue;
                }
                else if (_inputValidation.IsNumberNegative(menuChoice))
                {
                    Console.WriteLine("Input must be a positive number. Try again.");
                    continue;
                }

                //Menu choice
                switch (menuChoice)
                {
                    //View all rooms by type in alphabetical order
                    case "1":
                        sortOption = 1;
                        break;
                    //Show halls
                    case "2":
                        sortOption = 2;
                        break;
                    //Show classrooms
                    case "3":
                        sortOption = 3;
                        break;
                    //Show group rooms
                    case "4":
                        sortOption = 4;
                        break;
                    //Show rooms by number of seats, descending order
                    case "5":
                        sortOption = 5;
                        break;
                    //Show rooms by number of seats, rising order
                    case "6":
                        sortOption = 6;
                        break;
                    //Show rooms with projector
                    case "7":
                        sortOption = 7;
                        break;
                    //Show rooms with whiteboard
                    case "8":
                        sortOption = 8;
                        break;
                    //Going back
                    case "0":
                        sortOption = 0;
                        break;
                    default:
                        Console.WriteLine("Not a valid selection. Press \"Enter\" and try again.");
                        Console.ReadKey();

                        continue;
                }
                if (sortOption != -1) break;
            }
            RoomInfoWithSort(rooms);
        }

        //Method to filter and sort rooms based menu choice
        private void RoomInfoWithSort(List<Rooms> rooms)
        {
            Console.Clear();
            List<Rooms> sortRoomList = rooms;

            //Menu choice options functions
            switch (sortOption)
            {
                case 1: //Show all rooms sorted in category and alphabetical order
                    Console.WriteLine("Show all rooms in category and alphabetical order\n");
                    sortRoomList = rooms
                        //Sort by category: Hall -> Classroom -> GroupRoom
                        .OrderBy(room => room is Hall ? 1 : room is Classroom ? 2 : 3)
                        //Sort the rooms alphabetically
                        .ThenBy(room => room.RoomName) 
                        .ToList();
                    break;
                case 2: //Show halls
                    Console.WriteLine("Show halls\n");
                    sortRoomList = rooms.OfType<Hall>().Cast<Rooms>().ToList();
                    break;
                case 3: //Show classrooms
                    Console.WriteLine("Show classrooms\n");
                    sortRoomList = rooms.OfType<Classroom>().Cast<Rooms>().ToList();
                    break;
                case 4: //Show group rooms
                    Console.WriteLine("Show group rooms\n");
                    sortRoomList = rooms.OfType<GroupRoom>().Cast<Rooms>().ToList();
                    break;
                case 5: //Rooms in order of number of seats, large - small
                    Console.WriteLine("Rooms in order of number of seats, large - small\n");
                    sortRoomList = sortRoomList.OrderByDescending(room => room.SeatAmount).ToList();
                    break;
                case 6: //Rooms in order of number of seats, small - large
                    Console.WriteLine("Rooms in order of number of seats, small - large\n");
                    sortRoomList = sortRoomList.OrderBy(room => room.SeatAmount).ToList();
                    break;
                case 7: //Rooms with projector
                    Console.WriteLine("Rooms with projector\n");
                    sortRoomList = rooms.Where(room => room is Hall hall && hall.HasProjector).Cast<Rooms>().ToList();
                    break;
                case 8: //Rooms with whiteboard
                    Console.WriteLine("Rooms with whiteboard\n");
                    sortRoomList = rooms.Where(room => room is Hall hall && hall.HasWhiteboard).Cast<Rooms>().ToList();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
            //Display the filtered or sorted list of rooms
            Console.WriteLine("ALL ROOMS OF CHOICE");
            Console.WriteLine("{0,-12}{1,-20}{2,-14}{3,-14}{4,-18}{5,-14}", "Type", "Name", "Seat amount", 
                "Seat Limit", "Has Projector", "Has Whiteboard");
            Console.WriteLine(new string('-', 100));

            foreach (var room in sortRoomList)
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
        }
    }
}
