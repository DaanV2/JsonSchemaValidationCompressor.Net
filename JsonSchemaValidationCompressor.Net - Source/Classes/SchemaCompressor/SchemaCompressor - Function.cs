using System;
using System.IO;
using DaanV2.Json.Specification;

namespace DaanV2.Json {
    public sealed partial class SchemaCompressor {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SpecificationFilepath"></param>
        public static void Compress(String SpecificationFilepath) {
            CompressingSpecification Spec = File.Deserialize<CompressingSpecification>(SpecificationFilepath);

            Spec.Resolve(Directory.GetParent(SpecificationFilepath).FullName);

            var Compressor = new SchemaCompressor();
            Compressor.Compress(Spec);
        }
    }
}
