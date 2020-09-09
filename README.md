# SearchFight

Console App that queries search engines an return results, for Example:

```
C:\SearchFight.exe .net java
```

```
.net: Google: 4450000000 MSN Search: 12354420
java: Google: 966000000 MSN Search: 94381485
Google winner: .net
MSN Search winner: java
Total winner: .net
```
### Prerequisites

We are using API from google and bing, then the application needs some keys to work.
When you have it, Just Update the following keys in the App.Config

```
    <add key="Google.ApiKey" value="GOOGLE_API_KEY" />
    <add key="Google.ContextId" value="GOOGLE_CONTEXT_ID" />
    <add key="Bing.ApiKey" value="BING_API_KEY" />
    <add key="Bing.CustomId" value="BING_CUSTOM_ID"/>
```

## Deployment

Compile the solution with visual studio and the exe will be generated.

## License

This project is licensed under the MIT License
