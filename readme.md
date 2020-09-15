# Real-time Collaborative Voting Web Application

This example of real-time collaboration application was created in 22 hours for a client demo *(full disclosure, a colleague did the html & css, but all other code, including (basic!) javascript is by me)*. Users can log in, see specific events from their calendar, start a collaborative session. Then, they see details pulled from a json end point (origin of data being Microsoft Dynamics), a chat, and can real-time vote (centrally) before pushing a final Accept/Reject decision to a Json endpoint.

Many thanks to my employer [Felinesoft Ltd](https://www.felinesoft.com) *(note: a Microsoft Gold Partner)* for allowing this project to become open source.

## Video Introduction

[![Video Introduction](https://img.youtube.com/vi/1RXKUDqWwJ0/0.jpg)](https://www.youtube.com/watch?v=1RXKUDqWwJ0)

## Technology Used

This project combines:

* [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)
* [Microsoft Graph](https://docs.microsoft.com/en-us/graph/overview) (with [@christosmatskas](https://twitter.com/christosmatskas) [blog post](https://cmatskas.com/call-ms-graph-apis-from-asp-net-core-3-1/) - super thanks!)
* [.Net Core 3.1 razor web app](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start?view=aspnetcore-3.1&tabs=visual-studio)
* [Azure Functions](https://azure.microsoft.com/en-gb/services/functions/)
* [Durable Entities](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-entities?tabs=csharp)
* [C#8 Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
* [System.Net.Http.Json NugetPackage](https://www.nuget.org/packages/System.Net.Http.Json) *(what a timesaver!)*

## Points to Note

Please bear in mind:

* I've never used Razor Web Apps before
* Coding C# 2.5 years at this stage
* Know very very little javascript
* Only had 3 days to do this in
* (Also my colleague didn't mention the client would actually *use* the site directly until 11pm the night before the demo - so if you notice some last minute `...First().Where(...)` in the code - you'll know why :wink:)

## Video: The Experience

[![The Experience](https://img.youtube.com/vi/TM7q8O_Ps94/0.jpg)](https://www.youtube.com/watch?v=TM7q8O_Ps94)