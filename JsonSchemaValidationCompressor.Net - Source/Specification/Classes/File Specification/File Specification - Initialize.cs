using System;
using System.Text.Json.Serialization;

namespace DaanV2.Json.Specification {
    ///DOLATER <summary>add description for class: FileSpecification</summary>
    public partial class FileSpecification {
        /// <summary>Creates a new instance of <see cref="FileSpecification"/></summary>
        [JsonConstructor]
        public FileSpecification() {
            this.Destination = String.Empty;
            this.Source = String.Empty;
        }

        // Temporarily comment out to rule out ambiguity
        // public FileSpecification(String source, String destination) {
        //     this.Source = source;
        //     this.Destination = destination;
        // }
    }
}
