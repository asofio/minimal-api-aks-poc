# Generate a client SDK using OpenAPI Generator

Last but not least, since we have created an API that exposes an OpenAPI definition, lets go through the exercise of generating a client SDK from this API definition.  To do this, we are going to use [OpenAPI Generator](https://openapi-generator.tech/).

### Installation

To install OpenAPI Generator, you can install it as an npm package using the following:

`npm install @openapitools/openapi-generator-cli -g`

NOTE: If you are running on OSX and are a hombrew user, you can execute the following:

`brew install openapi-generator`

### Generate an SDK

At this point, you need to decide what API location you will be pointing at to generate your SDK.  Meaning, in this sample repository, we have run the Minimal API both locally and in AKS.  You can generate a client SDK from either.  You will be utilizing the swagger.json endpoint of the exposed OpenAPI specification on your API.

#### API Hosted in AKS

To generate an SDK from the API hosted in AKS, run the following command:

`openapi-generator generate -i http://[YourKubernetesEXTERNAL-IPHere]:8080/swagger/v1/swagger.json -g csharp -o ./tmp/apisdk`

The previous command will place the generated C# SDK in a /tmp/apidk folder relative to the current location where you ran this command.  Browse the generated SDK to learn more about what OpenAPI Generator generates for you.  Note: OpenAPI Generator also generates documentation specific to your API located in the /docs folder.

#### API Hosted Locally

To generate an SDK while running the API locally, run the following command (fill in the port that your API is running on):

`openapi-generator generate -i https://localhost:[YourPortHere]/swagger/v1/swagger.json -g csharp -o ./tmp/apisdk`

NOTE: If you encounter an error indicating a SSHHandshakeException, you will need to temporarily disable certificate verification by adjusting the following environment variable.

Windows/Powershell

`[System.Environment]::SetEnvironmentVariable('JAVA_OPTS','-Dio.swagger.parser.util.RemoteUrl.trustAll=true -Dio.swagger.v3.parser.util.RemoteUrl.trustAll=true')`

OSX

`export JAVA_OPTS="-Dio.swagger.parser.util.RemoteUrl.trustAll=true -Dio.swagger.v3.parser.util.RemoteUrl.trustAll=true"`

### Additional Information

The above examples generate C# client SDKs, however, the OpenAPI Generator can generate SDKs in many more languages.  Please reference [this documentation](https://openapi-generator.tech/docs/generators/) for further information.
