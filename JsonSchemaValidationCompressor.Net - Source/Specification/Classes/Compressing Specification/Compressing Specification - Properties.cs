using System.Collections.Generic;

namespace DaanV2.Json.Specification {
    public sealed partial class CompressingSpecification {
        /// <summary>Gets or sets the file that are processed</summary>
        public List<FileSpecification> Files { get; set; }
    }
}
