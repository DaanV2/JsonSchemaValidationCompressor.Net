using System.IO;
using System.Text;
using DaanV2.Json.Specification;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DaanV2.Json {
    public sealed partial class SchemaCompressor {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Specification"></param>
        public void Compress(CompressingSpecification Specification) {
            foreach (FileSpecification File in Specification.Files) {
                this.Compress(File);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Specification"></param>
        public void Compress(FileSpecification Specification) {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Compressing json: " + Specification.Source);
            System.Console.WriteLine("Compressing json: " + Specification.Source);
#endif

            if (!System.IO.File.Exists(Specification.Source)) {
                throw new FileNotFoundException("Cannot find source file", Specification.Source);
            }

            JObject Doc = File.Load(Specification.Source);

            //Resolve reference.
            this.Resolver.Resolve(Doc, Specification.Source);

            Directory.GetParent(Specification.Destination).Create();
            var Writer = new StreamWriter(Specification.Destination, false, Encoding.UTF8);
            var JsonWriter = new JsonTextWriter(Writer) {
                Formatting = Formatting.None
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine("Writing json: " + Specification.Destination);
#endif

            Doc.WriteTo(JsonWriter);
            Writer.Close();
        }
    }
}
