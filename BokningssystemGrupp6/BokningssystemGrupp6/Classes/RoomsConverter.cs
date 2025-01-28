using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    //JSON converter for converting/handling polymorphic serialization and deserialization of the "Rooms"-class
    public class RoomsConverter : JsonConverter<Rooms>
    {
        public override Rooms Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Parse the JSON data into a JSON document for accessesing the data
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                //Extract the RoomType to determine the subclass type (Hall, Classroom, Group room)
                string roomType = root.GetProperty("RoomType").GetString();

                /* Using switch to select the correct subclass based on RoomType
                 Deserialize JSON to the correct subclass based on roomType */
                Rooms? room = roomType switch
                {
                    "Hall" => JsonSerializer.Deserialize<Hall>(root.GetRawText(), options),
                    "Classroom" => JsonSerializer.Deserialize<Classroom>(root.GetRawText(), options),
                    "Group room" => JsonSerializer.Deserialize<GroupRoom>(root.GetRawText(), options),
                    // Throw an exception error if RoomType is not known
                    _ => throw new JsonException($"Unknown room type: {roomType}")
                };
                //Return the deserialized room object
                return room;
            }
        }

        //Overriding Write method to control serialization of "Rooms" objects into JSON
        public override void Write(Utf8JsonWriter writer, Rooms value, JsonSerializerOptions options)
        {
            //Serialize "Rooms" object, with data based from its subclass (Hall, Classroom, Group room)
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}