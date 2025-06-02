# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY JsonSchemaValidationCompressor.Console.Net/*.csproj ./JsonSchemaValidationCompressor.Console.Net/
COPY JsonSchemaValidationCompressor.API.Net/*.csproj ./JsonSchemaValidationCompressor.API.Net/
COPY JsonSchemaValidationCompressor.Net\ -\ Source/*.shproj ./JsonSchemaValidationCompressor.Net - Source/
COPY JsonSchemaValidationCompressor.Net\ -\ Source/*.projitems ./JsonSchemaValidationCompressor.Net - Source/

# Copy the rest of the source code
COPY . .

# Restore dependencies
RUN dotnet restore JsonSchemaValidationCompressor.Console.Net/JsonSchemaValidationCompressor.Console.Net.csproj

# Publish the application to a self-contained directory
RUN dotnet publish JsonSchemaValidationCompressor.Console.Net/JsonSchemaValidationCompressor.Console.Net.csproj -c Release -r linux-x64 --self-contained false -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "JsonSchemaValidationCompressor.Console.Net.dll"]
