using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    internal sealed partial class ReferenceResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="referenceData"></param>
        internal void Inherit(String name, JObject referenceData, ReferenceResolver Child) {
            foreach (KeyValuePair<String, String> Item in Child._ReferenceConverter) {
                this._ReferenceConverter[Item.Key] = Item.Value;
            }

            //Copy over definitions
            if (referenceData.ContainsKey("definitions")) {
                JToken definitions = referenceData["definitions"];

                if (definitions is JObject ODefinitions) {
                    foreach (KeyValuePair<String, JToken> Item in ODefinitions) {
                        this._Definitions[Item.Key] = Item.Value;
                    }
                }

                referenceData.Remove("definitions");
            }

            referenceData.Remove("$id");
            referenceData.Remove("$schema");

            //move schema into definitions
            this._Definitions[name] = referenceData;
        }
    }
}
