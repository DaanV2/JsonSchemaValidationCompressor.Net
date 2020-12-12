using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaResolver {
        /// <summary>
        /// 
        /// </summary>
        private void Simplify(JObject Doc, ReferenceResolver Resolver) {

        }

        internal static void SimplifyCount(JObject Doc, ReferenceResolver Resolver) {
            var Count = new Dictionary<String, Int32>(Resolver._Definitions.Count);

            foreach (KeyValuePair<String, JToken> Item in Resolver._Definitions) {
                Count[Item.Key] = 0;
            }

            foreach (JToken Item in Resolver.References) {
                String Ref = Item["$ref"].Value<String>();

                if (Ref.StartsWith("#/definitions/")) {
                    Ref = Ref[14..];

                    if (Count.TryGetValue(Ref, out Int32 Value)) {
                        Count[Ref] = Value + 1;
                    }
                    else {
                        Count[Ref] = 1;
                    }
                }
            }

            foreach (KeyValuePair<String, Int32> Item in Count) {
                if (Item.Value == 0) {
                    Resolver._Definitions.Remove(Item.Key);
                    Resolver._ReferenceConverter.RemoveValue(Item.Key);
                }
                else if (Item.Value == 1) {
                    JToken Definitions = Resolver._Definitions[Item.Key];
                }
            }
        }
    }
}