# OpenId Connect Example in ASP.Net Core
0. download and install .net sdk if you don't have a Visual Studio

https://dotnet.microsoft.com/download/dotnet-core/2.2

1. register your applications in Google, Facebook and Github

https://console.developers.google.com (redirect url is /google-signin)

https://developers.facebook.com/apps/ (redirect url is /facebook)

https://github.com/settings/applications/new (redirect url is /github)


2. edit Startup.cs, put your clientIds and clientSecrets

3. edit Scripts\index.ts, put your google client id

4. open the OpenIdConnectInDotNetCore\WebApplication6\Scripts in console
```
run "npm init" command (install nodejs if this command doesn't exist)
run "npm install -g webpack"
run "webpack"
```

5. run the application
you can run from Visual Studio or open the folder "C:\temp\OpenIdConnectInDotNetCore\WebApplication6" in command line
and run "dotnet run ." command.

open /Home/ (for Authorization Code Flow) or /Home2/ (for Implicit flow)
