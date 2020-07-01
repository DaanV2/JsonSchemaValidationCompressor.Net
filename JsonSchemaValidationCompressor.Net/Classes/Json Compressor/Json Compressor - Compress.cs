using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonSchemaValidationCompressor.Net {
    public sealed partial class JsonCompressor {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceFilepath"></param>
        /// <param name="DestinationFilepath"></param>
        public void Compress(String SourceFilepath, String DestinationFilepath) {
            JsonCompressorContext Context = new JsonCompressorContext {
                _BaseFolder = Path.GetDirectoryName(SourceFilepath),
                _DestinationFilepath = DestinationFilepath,
                _SourceFilepath = SourceFilepath
            };

            this.Compress(Context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        private void Compress(JsonCompressorContext Context) {
            Console.WriteLine("Compressing:\t" + Context._SourceFilepath);
            StreamReader Reader = new StreamReader(Context._SourceFilepath);
            JsonTextReader JsonReader = new JsonTextReader(Reader);

            JObject Object = JObject.Load(JsonReader);
            Reader.Close();

            if (!Object.ContainsKey("definitions")) {
                Object.Add("definitions", new JObject());
            }

            Context._Definitions = (JObject)Object["definitions"];
            this.Compress(Context, Object);

            Console.WriteLine("Writing:\t" + Context._DestinationFilepath);
            FileInfo Destination = new FileInfo(Context._DestinationFilepath);
            Destination.Directory.Create();
            StreamWriter Writer = new StreamWriter(Context._DestinationFilepath);
            JsonTextWriter JsonWriter = new JsonTextWriter(Writer);

            Object.WriteTo(JsonWriter);
            Writer.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="Object"></param>
        private void Compress(JsonCompressorContext Context, JToken Object) {
            switch (Object) {
                case JObject Jo:
                    this.Compress(Context, Jo);
                    return;
            }

            foreach (JToken Item in Object) {
                this.Compress(Context, Item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="Object"></param>
        private void Compress(JsonCompressorContext Context, JObject Object) {
#if !DEBUG
            Object.Remove("$schema");
#endif

            if (Object.ContainsKey("$ref")) {
                this.ResolveReference(Context, Object, Object.Property("$ref"));
            }

            if (Object.ContainsKey("definitions")) {
                JObject Definition = (JObject)Object["definitions"];

                if (!System.Object.ReferenceEquals(Definition, Context._Definitions)) {
                    foreach (KeyValuePair<String, JToken> Item in Definition) {
                        String Name = Item.Key;

                        if (Context._Definitions.ContainsKey(Name)) {
                            Context._Definitions.Remove(Name);
                        }

                        Context._Definitions.Add(Name, Item.Value);
                    }

                    Object.Remove("definitions");
                }
            }

            foreach (KeyValuePair<String, JToken> Item in Object) {
                this.Compress(Context, Item.Value);
            }
        }
    }
}
