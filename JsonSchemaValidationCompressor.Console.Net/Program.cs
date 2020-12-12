using System;
using DaanV2.Json;

namespace JsonSchemaValidationCompressor.Console.Net {
    public class Program {
        private static void Main(String[] args) {
            System.Console.WriteLine("Processing compression specifications");

#if DEBUG
            args = new String[] { @"F:\Projects\Blockception\VSCode-Bedrock-Development-Extension\minecraft-bedrock-schemas\compress specification.json" };
#endif 

            foreach (String arg in args) {
                if (System.IO.File.Exists(arg)) {
                    SchemaCompressor.Compress(arg);
                }
            }
        }
    }
}
