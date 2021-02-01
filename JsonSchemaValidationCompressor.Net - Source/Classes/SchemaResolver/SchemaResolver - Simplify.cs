using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schema"></param>
        public void DefinitionSimplfier(JObject Schema) {
            Console.WriteLine("Simplyfing definitions");
            var Definitions = (JObject)Schema["definitions"];

            var Names = new List<String>(Definitions.Count);
            foreach (KeyValuePair<String, JToken> Child in Definitions) {
                Names.Add(Child.Key);
            }

            UInt32 Count = 0;

            var Data = new List<ReplaceData>();

            var NewDefinitions = new JObject();

            foreach (String Name in Names) {
                var Item = (JObject)Definitions[Name];

                String NewName = ReferenceResolver.CreateName(Count);
                ++Count;

                //Replace the definition with a shorter one
                NewDefinitions[NewName] = Item;
                Data.Add(new ReplaceData("#/definitions/" + Name, "#/definitions/" + NewName));
            }

            Schema["definitions"] = NewDefinitions;

            this.ReplaceDefinition(Schema, Data);
        }

        private struct ReplaceData {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Old"></param>
            /// <param name="New"></param>
            public ReplaceData(String Old, String New) {
                this.Old = Old;
                this.New = New;
            }

            public String New;
            public String Old;
        }
    }
}