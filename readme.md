# Real-time Collaborative Voting Web Application

This example of real-time collaboration application was created in 22 hours for a client demo (full disclosure, a colleague did the html & css, but all other code, including (basic!) javascript is by me)

<iframe width="560" height="315" src="https://www.youtube.com/embed/1RXKUDqWwJ0" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

This project combines:

* SignalR
* Microsoft Graph
* .Net Core 3.1 razor web app
* Azure Functions
* [Durable Entities](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-entities?tabs=csharp)
* [C#8 Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)

Please bear in mind:

* I've never used Razor Web Apps before
* Coding C# 2.5 years at this stage
* Know very very little javascript
* Only had 3 days to do this in
* (Also my colleague didn't mention the client would actually *use* the site directly until 11pm the night before the demo - so if you notice some last minute `...First().Where(...)` in the code - you'll know why :wink:)
