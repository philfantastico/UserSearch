# UserSearch
A generic source control user search solution with a repository implemented for searching for users via the github api

## Troubleshooting
Note that when I clone the repo locally and try to run UserSearchWeb Web App I get an error like the following:

Could not find a part of the path 'C:\Users\phill\source\repos\UserSearch2\UserSearch\UserSearchWeb\bin\roslyn\csc.exe'.

If you get similar, I find running the following command in the Package Manager Console (VS2017 --> Tools --> NuGet Package Manager --> Package Manager Console) resolves the issue...

Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r

This was a solution I found at this stack overflow post:

https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe

