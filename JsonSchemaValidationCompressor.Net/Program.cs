using System;
using System.IO;

namespace JsonSchemaValidationCompressor.Net {
    internal class Program {
        private static void Main(String[] args) {

#if DEBUG
            args = new String[] {
                @"F:\Projects\Blockception\VSCode-Bedrock-Development-Extension\source schemas\minecraft-general\schemas\resource\entity\entity.json",
                @"F:\Projects\Blockception\VSCode-Bedrock-Development-Extension\minecraft-general\schemas\resource\entity\entity.json"
            };
#endif

            if (args.Length != 2) {
                Console.WriteLine("Error: format: <source file> <outputfile>");
                return;
            }

            String SourceFilepath = args[0];
            String DestinationFilepath = args[1];

            if (!File.Exists(SourceFilepath)) {
                Console.WriteLine("Error: source file doesn't exist");
            }

            JsonCompressor Compressor = new JsonCompressor();
            Compressor.Compress(SourceFilepath, DestinationFilepath);
        }
    }
}
