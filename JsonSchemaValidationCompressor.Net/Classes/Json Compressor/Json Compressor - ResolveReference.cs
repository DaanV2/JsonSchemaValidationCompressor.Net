using System;
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
        private void ResolveReference(JsonCompressorContext Context, JObject Parent, JProperty Ref) {
            String Text = Ref.Value.Value<String>();

            //definitions
            if (Text.StartsWith("#")) {
                return;
            }
            else if (Text.StartsWith("http")) {
                return;
            }
            else {
                Int32 Index = Text.IndexOf(".json");
                String Filepath = Text.Substring(0, Index + 5);
                Filepath = Path.Combine(Context._BaseFolder, Filepath);
                FileInfo FI = new FileInfo(Filepath);

                if (!FI.Exists) {
                    return;
                }

                StreamReader Reader = new StreamReader(Filepath);
                JsonTextReader JsonReader = new JsonTextReader(Reader);

                JObject Object = JObject.Load(JsonReader);
                Reader.Close();

                JsonCompressorContext nContext = new JsonCompressorContext {
                    _BaseFolder = FI.DirectoryName,
                    _Definitions = Context._Definitions,
                    _SourceFilepath = Filepath
                };

                this.Compress(nContext, Object);

                String Name = Text.Replace('/', '_').Replace(".json", String.Empty);

                if (Context._Definitions.ContainsKey(Name)) {
                    Context._Definitions.Remove(Name);
                }

                Context._Definitions.Add(Name, Object);
                Parent.Remove("$ref");
                Parent.Add("$ref", "#/definitions/" + Name);
            }
        }

    }
}
