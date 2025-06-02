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

## Using the Docker image

You can build and run the application using Docker:

### Build the Docker image

```sh
docker build -t jsvc:latest .
```

### Run the Docker container

```sh
docker run --rm -v $(pwd):/data jsvc:latest /data/path/to/specification.json
```

- Replace `/data/path/to/specification.json` with the path to your compression specification file (relative to your current directory).
- The `-v $(pwd):/data` flag mounts your current directory into the container at `/data` so the app can access your files.

### Example

```sh
docker run --rm -v $(pwd):/data jsvc:latest /data/Schema/Compression\ Schema.json
```

This will process the specification file using the Dockerized app.