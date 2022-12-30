# BlazorDevOps
This is a sample Blazor hybrid app demonstrating how to use Azure DevOps Services SDKs

## Problem statement
Create a Blazor web-assembly app that can access data from Azure DevOps Services via client SDKs or REST APIs.

## Challenges
* REST API calls from Blazor WASM fail due to CORS errors - have not found a way to allow-list origins other than `dev.azure.com`
* Azure DevOps client SDKs are not supported on web-assembly platform

## Solution
Use a Blazor WASM client hosted by a dotnet web app. The web app is used to proxy requests to the ADO REST API or SDK clients.