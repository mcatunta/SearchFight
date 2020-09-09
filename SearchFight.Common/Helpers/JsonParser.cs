﻿using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SearchFight.Common.Helpers
{
    public static class JsonParser
    {
        public static T Deserialize<T>(string json)
        {
            var instance = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
