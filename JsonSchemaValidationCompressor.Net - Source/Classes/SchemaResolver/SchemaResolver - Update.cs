using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaResolver {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Resolver"></param>
        private void TraverseAppendDefinition(JObject Element, String Name) {
            var Names = new List<String>(Element.Count);

            foreach (KeyValuePair<String, JToken> Child in Element) {
                Names.Add(Child.Key);
            }

            for (Int32 I = 0; I < Names.Count; I++) {
                JToken Item = Element[Names[I]];

                if (Item is JObject JChild) {
                    this.AppendDefinition(JChild, Name);
                }
                else if (Item is JArray A) {
                    this.TraverseAppendDefinition(A, Name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Elements"></param>
        /// <param name="Resolver"></param>
        private void TraverseAppendDefinition(JArray Elements, String Name) {
            foreach (JToken Item in Elements) {
                if (Item is JObject JChild) {
                    this.AppendDefinition(JChild, Name);
                }
                else if (Item is JArray A) {
                    this.TraverseAppendDefinition(A, Name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schema"></param>
        /// <param name="Name"></param>
        private void AppendDefinition(JObject Schema, String Name) {
            JToken Ref = Schema["$ref"];

            if (Ref != null) {
                String Value = Ref.Value<String>();

                if (Value.StartsWith("#/definitions/")) {
                    Value = Value.Replace("#/definitions/", "#/definitions/" + Name + "_");
                    Schema["$ref"] = new JValue(Value);
                }
            }

            JToken Definitions = Schema["definitions"];

            if (Definitions is JObject oDefinition) {
                var NewDefinition = new JObject();

                foreach (KeyValuePair<String, JToken> Item in oDefinition) {
                    String ItemName = Name + "_" + Item.Key;
                    NewDefinition[ItemName] = Item.Value.DeepClone();
                }

                Schema["definitions"] = NewDefinition;
            }

            this.TraverseAppendDefinition(Schema, Name);
        }
    }
}
