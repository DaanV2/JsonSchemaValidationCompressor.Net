using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Old"></param>
        /// <param name="New"></param>
        public void ReplaceDefinition(JObject Element, String Old, String New) {
            JToken Ref = Element["$ref"];

            if (Ref != null) {
                String Value = Ref.Value<String>();

                if (Value == Old) {
                    Element["$ref"] = new JValue(New);
                }
            }

            var Names = new List<String>(Element.Count);
            foreach (KeyValuePair<String, JToken> Child in Element) {
                Names.Add(Child.Key);
            }

            for (Int32 I = 0; I < Names.Count; I++) {
                JToken Item = Element[Names[I]];

                if (Item is JObject JChild) {
                    this.ReplaceDefinition(JChild, Old, New);
                }
                else if (Item is JArray A) {
                    this.ReplaceDefinition(A, Old, New);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schema"></param>
        /// <param name="Old"></param>
        /// <param name="New"></param>
        public void ReplaceDefinition(JArray Schema, String Old, String New) {
            foreach (JToken Item in Schema) {
                if (Item is JObject JChild) {
                    this.ReplaceDefinition(JChild, Old, New);
                }
                else if (Item is JArray A) {
                    this.ReplaceDefinition(A, Old, New);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Old"></param>
        /// <param name="New"></param>
        private void ReplaceDefinition(JObject Element, List<ReplaceData> Replace) {
            JToken Ref = Element["$ref"];

            if (Ref != null) {
                String Value = Ref.Value<String>();

                for (Int32 I = 0; I < Replace.Count; I++) {
                    ReplaceData Item = Replace[I];
                    if (Value == Item.Old) {
                        Element["$ref"] = new JValue(Item.New);
                        break;
                    }
                }
            }

            var Names = new List<String>(Element.Count);
            foreach (KeyValuePair<String, JToken> Child in Element) {
                Names.Add(Child.Key);
            }

            for (Int32 I = 0; I < Names.Count; I++) {
                JToken Item = Element[Names[I]];

                if (Item is JObject JChild) {
                    this.ReplaceDefinition(JChild, Replace);
                }
                else if (Item is JArray A) {
                    this.ReplaceDefinition(A, Replace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schema"></param>
        /// <param name="Old"></param>
        /// <param name="New"></param>
        private void ReplaceDefinition(JArray Schema, List<ReplaceData> Replace) {
            foreach (JToken Item in Schema) {
                if (Item is JObject JChild) {
                    this.ReplaceDefinition(JChild, Replace);
                }
                else if (Item is JArray A) {
                    this.ReplaceDefinition(A, Replace);
                }
            }
        }
    }
}
