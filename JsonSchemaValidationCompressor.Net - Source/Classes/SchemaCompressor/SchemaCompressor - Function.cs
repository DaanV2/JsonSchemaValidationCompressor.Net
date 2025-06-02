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
            var jObj = File.Load(SpecificationFilepath);

            var spec = new CompressingSpecification();
            spec.Schema = jObj["$schema"]?.ToString();

            var filesArray = jObj["Files"] as Newtonsoft.Json.Linq.JArray;
            if (filesArray != null) {
                foreach (var fileObj in filesArray) {
                    var fileSpec = new FileSpecification {
                        Source = fileObj["Source"]?.ToString(),
                        Destination = fileObj["Destination"]?.ToString()
                    };
                    spec.Files.Add(fileSpec);
                }
            }

            spec.Resolve(Directory.GetParent(SpecificationFilepath).FullName);

            var Compressor = new SchemaCompressor();
            Compressor.Compress(spec);
        }
    }
}
