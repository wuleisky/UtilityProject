using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UtilityResolution.Utility
{
   public static class Object
    {
        public static T Clone<T>(this T RealObject)
        {

            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(stream, RealObject);

                stream.Seek(0, SeekOrigin.Begin);

                return (T)serializer.Deserialize(stream);

            }

        }


        //public static T Clone<T>(this T data)
        //{
        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.All
        //    });
        //    var clone = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
        //    {
        //        TypeNameHandling = TypeNameHandling.Auto
        //    });
        //    return clone;
        //}

    }

   //public static class NewtonJSONHelper
   //{
   //    public static string SerializeObject(this object obj)
   //    {
   //        return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
   //        {
   //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
   //        });
   //    }

   //    public static T DeserializeObject<T>(this string data)
   //    {
   //        return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
   //        {
   //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
   //        });
   //    }
   //}
}
