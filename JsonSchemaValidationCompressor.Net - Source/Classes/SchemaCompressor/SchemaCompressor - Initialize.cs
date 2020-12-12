using System.Text.Json;

namespace DaanV2.Json {
    ///DOLATER <summary>add description for class: SchemaCompressor</summary>
    public sealed partial class SchemaCompressor {
        /// <summary>Creates a new instance of <see cref="SchemaCompressor"/></summary>
        public SchemaCompressor() {
            this.Resolver = new SchemaResolver();
        }
    }
}
