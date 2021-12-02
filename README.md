# .NET 6 Minimal API, AKS and More...

## Overview

With the release of .NET 6 comes the ability for API developers to create true "minimal" APIs in .NET.  This repo serves as an example that will introduce you to .NET 6 Minimal APIs in a simple, straightforward way.  As you work your way through the content below, you will also be guided through taking this simple API and deploying it to Azure Kubernetes Service (AKS) as well as a few other topics to help you get fully up and running using Minimal APIs.

## Goals

- Run your first .NET 6 Minimal API
- Containerize and deploy a .NET 6 Minimal API to AKS
- Create a new template for 'dotnet new' using the code sample in this repo
- Generate a client SDK using OpenAPI Generator

## Prerequisites

- [VSCode](https://code.visualstudio.com/download)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Azure Subscription (if you would like to try out deploying to AKS)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli#install)
- [kubectl](https://kubernetes.io/docs/tasks/tools/#kubectl)
- [Docker](https://docs.docker.com/get-docker/)
- Rest client of choice ([Postman](https://www.postman.com/downloads/) or VSCode [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension)

## Getting Started

Clone this repository and open the cloned folder in VSCode.  Upon opening the project, go ahead and restore any dependencies as VSCode prompts you to do so.  Before diving in too deep, lets take a tour of the project structure:

