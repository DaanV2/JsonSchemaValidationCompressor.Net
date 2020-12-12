using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    internal sealed partial class ReferenceResolver {
        /// <summary>Returns the reference definitions for the given filepath</summary>
        internal Dictionary<String, String> _ReferenceConverter;

        /// <summary>
        /// 
        /// </summary>
        internal JObject _Definitions;

        /// <summary></summary>
        internal Uri _Basepath;
    }
}
