# Json Schema Validation Compressor.Net

A program that can compress json files as well as solving relative file call. This program takes in a json specification file to compress a whole set of schemas

## Compression file format


```json
{
  "$schema": "https://raw.githubusercontent.com/DaanV2/JsonSchemaValidationCompressor.Net/master/Schema/Compression%20Schema.json",
  "Files": [
    {
      "Source": "./skinpacks/skins.json",
      "Destination": "../skinpacks/skins.json"
    }
  ]
}
```