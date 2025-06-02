# Use the official .NET 9 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the rest of the source code
COPY . .

# Restore dependencies
RUN dotnet restore JsonSchemaValidationCompressor.Console.Net/JsonSchemaValidationCompressor.Console.Net.csproj

# Publish the application as a single, self-contained, trimmed file
RUN dotnet publish JsonSchemaValidationCompressor.Console.Net/JsonSchemaValidationCompressor.Console.Net.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:PublishReadyToRun=true -o /app/publish

# Build minimal runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0
WORKDIR /app
COPY --from=build /app/publish/JsonSchemaValidationCompressor.Console.Net .

# Set the entrypoint
ENTRYPOINT ["/app/JsonSchemaValidationCompressor.Console.Net"]
