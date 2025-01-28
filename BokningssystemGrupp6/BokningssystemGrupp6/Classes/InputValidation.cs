using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    //Class responsible for validation of user input
    public class InputValidation
    {

        //Check if input is empty
        public bool IsEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        //Check if input is a number
        public bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        //Check if input is positive
        public bool IsNumberNegative(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result < 0;
            }
            return false;
        }

        //Check if input is larger than seat limit
        public bool IsGreaterThanSeatLimit(string input, int seatLimit)
        {
            if (int.TryParse(input, out int result))
            {
                return result > seatLimit;
            }
            return false;
        }

        //Check if input is zero
        public bool IsGreaterThanZero(string input)
        {
            int zeroBalance = 0;
            if (int.TryParse(input, out int result))
            {
                return result == zeroBalance;
            }
            return false;
        }

        //Check if name is used
        public bool IsNameUsed(List<Rooms> rooms, string input)
        {
            return rooms.Any(a => a.RoomName.ToLower() == input.ToLower());

        }

        //Check if an input number is larger than a compare number
        public bool IsNumberlargerThanCompare(string input, int compare) 
        {
            if (int.TryParse(input, out int result))
            {
                return result > compare;
            }
            return false;
        }

        //Check if email is in a email format
        public bool IsEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            //Regular expression to match a valid email format
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(input, emailPattern, RegexOptions.IgnoreCase);
        }

        //Check if input is a dateTime
        public bool IsDateTime(string input)
        {
            //Regular expression to match the date-time format: YYYY-MM-DD HH:MM
            string pattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$";
            return Regex.IsMatch(input, pattern);
        }

        //Check if datetime is after current time
        public bool IsDateTimeAfterNowTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime parsedDate))
            {
                return parsedDate > DateTime.Now;
            }
            return false;
        }

        //Check if input time is after compare time
        public bool IsDateTimeAfterCompareTime(string input, DateTime compare)
        {
            if (DateTime.TryParse(input, out DateTime inputDate))
            {
                return inputDate > compare;
            }
            return false;
        }

        //Check if the booking is to long
        public bool IsBookingTooLong(string input, DateTime compare)
        {
            TimeSpan twentyFourHours = TimeSpan.FromHours(24);

            if (DateTime.TryParse(input, out DateTime inputDate))
            {
                TimeSpan result = inputDate - compare;
                return result <= twentyFourHours;
            }
            return false;
        }

        //Convert string to int
        public int ConvertToInt(string input)
        {
            return int.Parse(input);
        }

        //Convert string to DateTime
        public DateTime ConvertToDateTime(string input)
        {
            return DateTime.Parse(input);
        }

    }

}
