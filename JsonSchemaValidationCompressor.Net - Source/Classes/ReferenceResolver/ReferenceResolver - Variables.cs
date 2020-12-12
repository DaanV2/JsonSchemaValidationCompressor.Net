using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    internal sealed partial class ReferenceResolver {
        /// <summary>Returns the reference definitions for the given filepath</summary>
        internal Dictionary<String, String> _ReferenceConverter;

        /// <summary>
        /// 
        /// </summary>
        internal Dictionary<String, JToken>  _Definitions;

        /// <summary></summary>
        internal Uri _Basepath;

        /// <summary>
        /// 
        /// </summary>
        internal List<JToken> References;
    }
}
