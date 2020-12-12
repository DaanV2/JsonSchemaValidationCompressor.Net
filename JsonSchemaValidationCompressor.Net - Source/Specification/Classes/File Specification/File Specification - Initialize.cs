using System;

namespace DaanV2.Json.Specification {
    ///DOLATER <summary>add description for class: FileSpecification</summary>
    public sealed partial class FileSpecification {
        /// <summary>Creates a new instance of <see cref="FileSpecification"/></summary>
        public FileSpecification() {
            this.Destination = String.Empty;
            this.Source = String.Empty;
        }

        /// <summary>Creates a new instance of <see cref="FileSpecification"/></summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        public FileSpecification(String Source, String Destination) {
            this.Destination = Source;
            this.Source = Destination;
        }
    }
}
