using System;
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
                try {
                    this.Compress(File);
                }
                catch (Exception ex) {
                    String Message = ex.Message;

                    if (!Message.StartsWith("##[error]")) {
                        Message = Message + "##[error] ";
                    }

                    Console.WriteLine(Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Specification"></param>
        public void Compress(FileSpecification Specification) {
            Console.WriteLine("Compressing json: " + Specification.Source);

            if (!System.IO.File.Exists(Specification.Source)) {
                throw new FileNotFoundException("cannot find source file", Specification.Source);
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
            Debug.WriteLine("Writing json: " + Specification.Destination);
#endif

            Doc.WriteTo(JsonWriter);
            Writer.Close();
        }
    }
}
