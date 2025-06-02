using System.Collections.Generic;
using Newtonsoft.Json;

namespace DaanV2.Json.Specification {
    ///DOLATER <summary>add description for class: CompressingSpecification</summary>
    public sealed partial class CompressingSpecification {
        /// <summary>Creates a new instance of <see cref="CompressingSpecification"/></summary>
        [JsonConstructor]
        public CompressingSpecification() {
            this.Files = new List<FileSpecification>();
        }
    }
}
