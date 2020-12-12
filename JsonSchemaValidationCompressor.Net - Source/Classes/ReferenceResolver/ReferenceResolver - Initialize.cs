using System;
using System.Collections.Generic;

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
            this._Definitions = null;
            this._ReferenceConverter = new Dictionary<String, String>();

            if (Parent != null) {
                foreach (KeyValuePair<String, String> Item in Parent._ReferenceConverter) {
                    this._ReferenceConverter[Item.Key] = Item.Value;
                }
            }
        }
    }
}
