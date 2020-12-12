using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    internal sealed partial class ReferenceResolver {
        /// <summary>
        /// 
        /// </summary>
        public ReferenceResolver(String BasePath, ReferenceResolver Parent = null) {
            if (!BasePath.EndsWith("/")) {
                BasePath += "/";
            }

            this._Basepath = new Uri(BasePath);
            this._Definitions = new Dictionary<String, JToken>();
            this._ReferenceConverter = new Dictionary<String, String>();

            if (Parent != null) {
                foreach (KeyValuePair<String, String> Item in Parent._ReferenceConverter) {
                    this._ReferenceConverter[Item.Key] = Item.Value;
                }

                foreach (KeyValuePair<String, JToken> Item in Parent._Definitions) {
                    this._Definitions[Item.Key] = null;
                }
            }

            this.References = new List<JToken>();
        }
    }
}
