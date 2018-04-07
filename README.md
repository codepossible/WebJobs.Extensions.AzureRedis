### ** Built on pre-release version of WebJobs Extensions SDK 3.0.0-beta5. NOT recommended for production use **

# Azure Redis Cache WebJobs Extension for Azure Functions v2 (.NET Core)

## Introduction 
This WebJobs Extension allows Azure Functions Apps to interact with Azure Redis Cache declaratively as a Cache Database  and as Async Collector to write batch of cached items to the store.

## Getting Started
To use the code, you will need Microsoft Visual Studio 2017 configured Azure Functions and WebJobs SDK. 

The code contains two sample Azure Functions projects which demonstrate the use of the extension as a cache database and async collector.

Clone the project on your machine locally and use Visual Studio to build and run the sample projects.

### Visual Studio Tooling Issue and Workaround
To be able to use the code successfully in Visual Studio, you **MUST** upgrade the Azure Functions and WebJobs Extension to **Version 15.0.40405.0 or higher**.

### Azure Redis Cache Configuration
The Azure Redis Cache binding requires a connection string to the Azure Redis Cache instance to work. This can be specified inline in the function or enclosing the name of AppSettings key within the "%" signs.

If not specified, the code looks for a value for the Redis Cache connection string in AppSettings under the name of - "AzureWebJobsAzureRedisConnectionString"

## Feedback and Sharing
If you find the code useful, please share it with your fellow developers, Twitter followers, Facebook groups, Google+ Circle and other social media networks.

Since this is built on a beta release of the SDK, updates and breaking changes are expected.
If that happens, please raise an issue in the GitHub project. 

## Noteworthy
Another colleague of mine at Microsoft - Francisco Beltrao in Switzerland, has also written WebJobs Extensions which include one for Redis Cache. Do check out Francisco's work at https://github.com/fbeltrao/AzureFunctionExtensions

