using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DaanV2.Json.Specification {
    public sealed partial class CompressingSpecification {
        /// <summary>Gets or sets the file that are processed</summary>
        public List<FileSpecification> Files { get; set; }

        /// <summary>Gets or sets the schema</summary>
        [JsonPropertyName("$schema")]
        public String Schema { get; set; }

        public CompressingSpecification() {
            this.Files = new List<FileSpecification>();
        }
    }
}
