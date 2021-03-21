using System;
using DaanV2.Json;

namespace JsonSchemaValidationCompressor.Console.Net {
    public class Program {
        private static void Main(String[] args) {
            System.Console.WriteLine("==== Processing compression specifications ====");

            if (args.Length == 0) {
                System.Console.WriteLine("Compression specification: ");
                String File = System.Console.ReadLine();

                args = new String[] { File };
            }

            foreach (String arg in args) {
                if (System.IO.File.Exists(arg)) {
                    try {
                        SchemaCompressor.Compress(arg);
                    }
                    catch(Exception ex) {
                        System.Console.WriteLine(ex.Message);
                        System.Console.WriteLine(ex.StackTrace);
                        Environment.ExitCode = 1;
                    }                    
                }
                else {
                    System.Console.WriteLine("Couldn't find: " + arg);
                    Environment.ExitCode = 1;
                }
            }

            System.Console.WriteLine("==== Done ====");
        }
    }
}
