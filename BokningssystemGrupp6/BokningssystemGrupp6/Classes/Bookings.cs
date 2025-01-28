using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes
{
    //Bookings inherits interface IListable
    public class Bookings : IListable
    {
        //Properties for Bookings
        public string Mail { get; set; }
        public string RoomName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }

        //Input validations
        private readonly InputValidation _inputValidation;
        public Bookings(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
        }

        //Construktor for Bookings      
        public Bookings()
        {

        }
        public Bookings(string mail, string roomName, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            Mail = mail;
            RoomName = roomName;
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
        }

        //Method to create booking
        public void BookARoom(List<Bookings> booked, List<Rooms> rooms)
        {            
            Console.WriteLine("Select room, or type 'quit' to cancel:");
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}, {r.RoomType}");
                i++;


            }
            if (i == 1)
            {
                Console.WriteLine("No room is available. Press a key to go back to continue.");
                Console.ReadKey();
                return;
            }
            int roomNumber;
            
            //Room select input
            string roomNumberStr = Console.ReadLine().Trim();

            // Check if input is "quit" and if it is cancel the booking
            if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

            //Room selection validation
            while (true)
            {

                try
                {
                    if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                    //Check if input is empty
                    if (_inputValidation.IsEmpty(roomNumberStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is a number
                    else if (!_inputValidation.IsNumber(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a number. Try again.");
                    }
                    else if (_inputValidation.IsGreaterThanZero(roomNumberStr))
                    {
                        Console.WriteLine("Input cannot be 0");
                    }
                    //Check if the number i negative
                    else if (_inputValidation.IsNumberNegative(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a positive number. Try Again");
                    }
                    //Check if seats is larger than seat limit
                    else if (_inputValidation.IsNumberlargerThanCompare(roomNumberStr, i - 1))
                    {
                        Console.WriteLine("Input cannot be larger the amount of rooms, Try Again");
                    }
                    else
                    {
                        //Convert string to int
                        roomNumber = _inputValidation.ConvertToInt(roomNumberStr) - 1;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    //Check if input is "quit" and if it is cancel the booking
                    if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                }
                roomNumberStr = Console.ReadLine().Trim();
            }

            string roomName = rooms[roomNumber].RoomName;

            //Email input
            Console.Write("Enter your email, or type 'quit' to cancel:");
            string? mailStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (mailStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            string mail;
            while (true)
            {
                try
                {
                    //Check if the email is in the correct format (ex. abc@mail.com) or empty 
                    if (!_inputValidation.IsEmail(mailStr))
                    {
                        Console.WriteLine("Email is incorrect, Try again");
                    }
                    else
                    {
                        mail = mailStr;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (mailStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                mailStr = Console.ReadLine().Trim();
            }
            //Booking start time
            Console.Write("Input start date and time for booking.\n(YYYY-MM-DD HH:MM):");
            string startTimeStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (startTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            DateTime dateTimeStart;
            while (true)
            {
                try
                {
                    //Check if input is empty
                    if (_inputValidation.IsEmpty(startTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(startTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    //Check if the datetime is after the current time
                    else if (!_inputValidation.IsDateTimeAfterNowTime(startTimeStr))
                    {
                        Console.WriteLine("Start time must be after the current time. Try Again");
                    }
                    else
                    {
                        //Convert string to datetime
                        dateTimeStart = _inputValidation.ConvertToDateTime(startTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (startTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                startTimeStr = Console.ReadLine().Trim();
            }
            //Booking end time
            Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
            string endTimeStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (endTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            DateTime dateTimeEnd;
            while (true)
            {
                try
                {
                    //Check if input is empty
                    if (_inputValidation.IsEmpty(endTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(endTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    else if (!_inputValidation.IsDateTimeAfterCompareTime(endTimeStr, dateTimeStart))
                    {
                        Console.WriteLine("End time must be after the start time. Try Again");
                    }
                    //Check if the booking is longer than 24 hours
                    else if (!_inputValidation.IsBookingTooLong(endTimeStr, dateTimeStart))
                    {
                        Console.WriteLine("Booking can be a maximum of 24 hours. Try Again");
                    }
                    else
                    {
                        //Convert string to datetime
                        dateTimeEnd = _inputValidation.ConvertToDateTime(endTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (endTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                endTimeStr = Console.ReadLine().Trim();
            }
            Console.Clear();
            if (booked.Count == 0)
            {
                //Adds the booking to the list
                booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                Save.SaveFile(booked);

                //Is printed when the booking is completed
                Console.WriteLine("Your booking is noted with the following information: ");

                //Calculates and prints the bookings duration and the last booking made                
                Console.WriteLine(booked.Last());
                ListAll(booked);
                
            }
            else
            {
                //Checks if the day is free from previous bookings in that venue
                foreach (Bookings book in booked)
                {
                    try
                    {
                        bool check = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                        //Free from previous bookings
                        if (check == false)
                        {
                            //Adds the booking to the list
                            booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                            Save.SaveFile(booked);
                            if (booked.Count > 0)
                            {
                                //Is printed when the booking is completed
                                Bookings lastBooking = booked[booked.Count - 1];
                                Console.WriteLine($"\nYour booking is noted with the following information: ");
                                //Skriver ut det sista objektet
                                ListSpecific(lastBooking);
                            }
                            else
                            {
                                Console.WriteLine("List is empty.");
                            }
                        }
                        //If the booking conflicts with a previously made booking
                        else if (check != false)
                        {
                            Console.WriteLine("Unfortunately, your selected time & date clashes with an previous booking");
                            //Prints the booking it conflicts with
                            ListSpecific(book);
                            Menu.BackToMenu();
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    }
                }
            }
        }

        //Method to list all bookings
        public static void ListAll(List<Bookings> bookingInfo)
        {
            Console.WriteLine("ALL BOOKINGS");
            Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", "", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
            Console.WriteLine(new string('-', 100));
            int i = 1;
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", i + ".",
                    booking.Mail, booking.RoomName, booking.DateTimeStart, booking.DateTimeEnd, booking.DateTimeEnd - booking.DateTimeStart);
                i++;
            }
        }

        //Method to list data from specific booking feed into it
        public static void ListSpecific(Bookings booking)
        {
            Console.WriteLine($"" +
                $"\nEmail: {booking.Mail}" +
                $"\nRoom: {booking.RoomName}" +
                $"\nBooking starts at: {booking.DateTimeStart} " +
                $"\nBooking ends at: {booking.DateTimeEnd} " +
                $"\nTotal duration for this booking is: {booking.DateTimeEnd - booking.DateTimeStart}");
        }

        //Method to list all bookings
        public static void ListAllBookingsByYearOrRoom(List<Bookings> bookingInfo, List<Rooms> listOfRoom)
        {
            //Show all bookings
            ListAll(bookingInfo);

            //Menu selection depending on how the user wants the information to display
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("" +
                "\nSelect:" +
                "\n1. Show all bookings from specific year" +
                "\n2. Show all bookings from specific room\n" +
                "\nEnter the number for the corresponding option");

            //Input choise
            string choise = Console.ReadLine();
            Console.Clear();

            //New list with only bookings with the right parameters
            List<Bookings> specificBookings = new List<Bookings>();

            switch (choise)
            {
                //Show all bookings from year 
                case "1":
                    Console.WriteLine("Enter the year of the bookings you want to display in the format YYYY");
                    String yearInputString = Console.ReadLine();

                    //Convert input to datetime
                    if (DateTime.TryParseExact(yearInputString, "yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate))
                    {
                        DateTime endDate = new DateTime(startDate.Year, 12, 31, 23, 59, 59);

                        foreach (Bookings yearBooking in bookingInfo)
                        {
                            if (yearBooking.DateTimeStart >= startDate && yearBooking.DateTimeEnd <= endDate)
                            //Adds bookings to list if it meet the requirmets
                            {
                                specificBookings.Add(yearBooking);
                            }
                        }
                        ListAll(specificBookings);
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                    }
                    break;
                //Show all bookings from specific room 
                case "2":

                    String specificRoom = Rooms.ChooseASpecificRoom(listOfRoom);
                    Console.Clear();
                    foreach (Bookings roomBooking in bookingInfo)
                    {
                        if (roomBooking.RoomName == specificRoom)
                        {
                            specificBookings.Add(roomBooking);
                        }
                    }
                    ListAll(specificBookings);
                    break;
                //If user selects invalid option.
                default:
                    Console.WriteLine("Invalid option, try again");
                    break;
            }
        }

        //Method to update an already existing booking
        public static void UpdateBooking(List<Bookings> bookingInfo, List<Rooms> roomList)
        {

            //Check if ok to save list at the last step, makes sure it doesn't remove anything from the list
            Boolean isNewBookingSuccess = false;

            //Used to temporary save the booking that is going to be removed
            Bookings bookingToRemove = new Bookings();

            //Used to end loops to end method if all conditions are fulfilled 
            Boolean isValidInput = false;
            //Used to check if booking overlaps
            Boolean checkIfBookingOverlaps = false; 

            do
            {
                //Checks if there is no bookings in list and breaks if it cant find any
                if (bookingInfo.Count == 0)
                {
                    Console.WriteLine("No existing booking that can be updated");
                    isValidInput = true;
                    break;
                }
                Console.WriteLine("Existing bookings: \n");
                //List bookings
                Console.WriteLine("ALL EXISTING BOOKINGS");
                Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", "", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
                Console.WriteLine(new string('-', 100));
                int i = 1;
                foreach (Bookings booking in bookingInfo)
                {
                    Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", i + ".",
                        booking.Mail, booking.RoomName, booking.DateTimeStart, booking.DateTimeEnd, booking.DateTimeEnd - booking.DateTimeStart);
                    i++;
                }

                Console.WriteLine("\nEnter the number for the corresponding option " +
                    "or type 'quit' to cancel");
                //Input choice
                String? choiceString = Console.ReadLine();

                if (choiceString.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                //Check if choice is valid character and convert
                if (int.TryParse(choiceString, out int choiceInt)) 
                {
                    //Check if inside list range
                    if (choiceInt <= bookingInfo.Count && choiceInt > 0)
                    {
                        //Have to shrink by 1 to actually match index for list
                        choiceInt--;
                        Console.WriteLine("Choose room you want to book \n");
                        String roomName = Rooms.ChooseASpecificRoom(roomList);
                        //Creates a new list so a list without the booking to be change so it dosen't create a booking conflict with dates
                        List<Bookings> withoutChosenBookingOnlyAndSpecificRoom = new List<Bookings>();
                        bookingToRemove = bookingInfo[choiceInt];
  
                        //Adds only bookings for the chosen room, else it might check for conflicts in rooms that aren't relevant
                        foreach (Bookings booking in bookingInfo)
                        {
                            if (booking.RoomName == roomName)
                                withoutChosenBookingOnlyAndSpecificRoom.Add(booking);
                        }
                        withoutChosenBookingOnlyAndSpecificRoom.Remove(bookingToRemove);

                        Console.Write("\nInput email: ");
                        String mail = Console.ReadLine();

                        Console.Write("Input start date and time for booking. \n(YYYY-MM-DD HH:MM):");
                        String startTime = Console.ReadLine();
                        Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
                        string endTime = Console.ReadLine();
                        Console.Clear();

                        if (startTime.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                        if (endTime.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                        try
                        {
                            DateTime dateTimeS = DateTime.Parse(startTime);
                            DateTime dateTimeE = DateTime.Parse(endTime);

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect format, please enter the date and time in the correct format.");
                        }
                        //Converts the input to a DateTime object                                
                        DateTime dateTimeStart = DateTime.Parse(startTime);
                        DateTime dateTimeEnd = DateTime.Parse(endTime);

                        TimeSpan totalTime = dateTimeEnd - dateTimeStart;

                        if (withoutChosenBookingOnlyAndSpecificRoom.Count == 0)
                        {
                            //Adds the booking to the list
                            withoutChosenBookingOnlyAndSpecificRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));

                            //Is printed when the booking is completed
                            Console.WriteLine("Your booking is noted with the following information: ");

                            //Prints the last item
                            ListSpecific(withoutChosenBookingOnlyAndSpecificRoom[withoutChosenBookingOnlyAndSpecificRoom.Count - 1]);
                            
                            isNewBookingSuccess = true;
                            isValidInput = true;
                        }
                        else
                        {
                            //Check if the day is free from previous bookings in that venue
                            foreach (Bookings book in withoutChosenBookingOnlyAndSpecificRoom)
                            {
                                checkIfBookingOverlaps = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                                //Checks that the booking conflicts with an already made booking
                                if (checkIfBookingOverlaps != false)
                                {
                                    Console.WriteLine("Unfortunately, your selected time & date clashes with an previous booking");
                                    //Print the booking it conflicts with
                                    ListSpecific(book);
                                    isValidInput = true;

                                    break;
                                }
                            }
                            if (checkIfBookingOverlaps == false)
                            {
                                //Adds the booking to the temp list
                                withoutChosenBookingOnlyAndSpecificRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                                Bookings newest = withoutChosenBookingOnlyAndSpecificRoom[withoutChosenBookingOnlyAndSpecificRoom.Count - 1];

                                //Printed when the booking is completed
                                Console.WriteLine("Your booking is noted with the following information: ");

                                //Prints the last item
                                ListSpecific(newest);

                                //It is allowed to save
                                isNewBookingSuccess = true; 
                            }
                        }
                        //Checks if the new booking fulfills all requirements
                        if (isNewBookingSuccess == true)
                        {
                            foreach (Bookings bookingRemove in bookingInfo)
                            {
                                //Remove booking
                                if (bookingRemove == bookingToRemove)
                                {
                                    bookingInfo.Remove(bookingRemove);
                                    break;
                                }
                            }
                            //Add to and save bookinglist
                            bookingInfo.Add(withoutChosenBookingOnlyAndSpecificRoom[withoutChosenBookingOnlyAndSpecificRoom.Count - 1]);
                            isValidInput = true;
                            Save.SaveFile(bookingInfo);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid option \n Press any key to try again");
                        Console.ReadKey();
                        Console.Clear();

                        continue; 
                    }
                }
            }
            while (isValidInput == false);
        }

        //Method to delete previous booking
        public static void DeleteBooking(List<Bookings>bookingInfo, List<Rooms>roomList)
        {
   
            while (true)
            {
                //Checks if there is no bookings in list and breaks if it cant find any
                if (bookingInfo.Count == 0)
                {
                    Console.WriteLine("There is no existing booking that can be removed");
                    Console.ReadKey();
                    break;
                }
                //List bookings
                Console.WriteLine("ALL EXISTING BOOKINGS");
                Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", "", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
                Console.WriteLine(new string('-', 100));
                int i = 1;
                foreach (Bookings booking in bookingInfo)
                {
                    Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", i + ".",
                        booking.Mail, booking.RoomName, booking.DateTimeStart, booking.DateTimeEnd, booking.DateTimeEnd - booking.DateTimeStart);
                    i++;
                }

                Console.WriteLine("\nEnter the number for the corresponding option " +
                    "or type 'quit' to cancel");

                //Input choice form list
                String choiceString = Console.ReadLine();
                if (choiceString.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                //Check if choiceString is valid character and convert if it is
                if (int.TryParse(choiceString, out int choiceInt)) 
                {
                    //Check if inside list range
                    if (choiceInt <= bookingInfo.Count && choiceInt > 0) 
                    {
                        //List booking to be removed
                        Console.WriteLine("You have removed booking:");
                        ListSpecific(bookingInfo[choiceInt - 1]);

                        //Remove and save booing
                        bookingInfo.RemoveAt(choiceInt - 1);
                        Save.SaveFile(bookingInfo);

                        break;
                    }
                }  
                //If choice is not valid, loop and input choice again
                else
                {
                    Console.WriteLine("Not a valid choice \nPress any key to try again");
                    Console.ReadKey();
                    Console.Clear();

                    continue;
                }
            }
        }
    }
}
