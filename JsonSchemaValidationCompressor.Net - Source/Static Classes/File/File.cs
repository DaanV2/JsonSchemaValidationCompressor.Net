using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    ///DOLATER <summary>add description for class: File</summary>
    public static partial class File {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filepath"></param>
        /// <returns></returns>
        public static JObject Load(String Filepath) {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Loading json: " + Filepath);
            Console.WriteLine("Loading json: " + Filepath);
#endif

            var Reader = new StreamReader(Filepath);
            var JsonReader = new JsonTextReader(Reader);
            var Out = JObject.Load(JsonReader);

            Reader.Close();

            return Out;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Filepath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(String Filepath) {
        #if DEBUG
            System.Diagnostics.Debug.WriteLine("Deserializing json: " + Filepath);
            Console.WriteLine("Deserializing json: " + Filepath);
        #endif

            var jsonSerializer = new JsonSerializer();

            var Reader = new StreamReader(Filepath);
            var JsonReader = new JsonTextReader(Reader);
            T Out = jsonSerializer.Deserialize<T>(JsonReader);
            Reader.Close();

            return Out;
        }
    }
}
