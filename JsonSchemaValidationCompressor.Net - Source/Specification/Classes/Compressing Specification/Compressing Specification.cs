using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DaanV2.Json.Specification {
    public sealed partial class CompressingSpecification {
        /// <summary>Gets or sets the file that are processed</summary>
        public List<FileSpecification> Files { get; set; }

        /// <summary>Gets or sets the schema</summary>
        [JsonProperty("$schema")]
        public String Schema { get; set; }

        [JsonConstructor]
        public CompressingSpecification()
        {
            this.Files = new List<FileSpecification>();
        }
    }
}
