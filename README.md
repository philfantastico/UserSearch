# UserSearch
A generic source control user search solution with a repository implemented for searching for users via the github api

## Deployed Instance
You can see the app running and try it out by going to the following URL:

https://usersearchwebapp.azurewebsites.net/

## Exercise

This implementation is a response to an exercise to create a web application that can be used to search for users in github using their login name and display some basic details about the user, including their avatar image and an ordered list of their 5 most popular (i.e. most starred) repos.

## Terminology

**repo** vs **repository** - Repository is ambiguous in this implementation in that Github has repositories (i.e. a unit of source contorl within Github) and in code implementation I normally call a class that implements access to a data store a repository. Throughout the code I have attempted to remove this ambiguity using the following standard:

1. **Repo** - a unit of source control, i.e. a Github Repository like the one this readme file is located in.
2. **Repository** - a class used for data access to a data store, i.e. the Github API.

## Implementation Intent

My intent with this implementation focused on several key points I wanted to show including:

1. **Reusable code** - The class library, containing the source control search functionality and models, is implemented as a .NET Standard class library. This could be reused in applications written in any of the .NET flavours that implement the .NET Standard interfaces including a more modern .NET Core web application or API, a desktop app that could run on windows and apple mac devices, and also directly in a mobile application implemented in Xamarin running on an Android or iOS device.

2. **Abstraction and Encapsulation** - Although the exercise describes a specific Github user search scenario I approached the problem in a very abstract way. The source control search functionality is implemented in a generic user search class library that includes an implementation of a Github Repository (repository here in the sense of a data source rather than a repository of code) which encapsulates the Github specific search functionality and returns generic source control models containing user and repo information. The web application displays the generic source control user and repo models.

3. **Dependency Injection and Unit Testing** - To enable both unit testing and also control by the web application of which source control repository to search in, certain dependencies are injected. The WebClient is injected as a dependency into the Repository which enables unit testing of the GithubRepository without making any actual HTTP calls to the github API. Similarly injection of a Repository into the actual Search class enables unit testing of the Search class without worrying about the Repository implementation, limiting the testing specifically to the units of code we are aiming to test. Injecting the Repository also allows the Web App to control which source control system to use for the search. 

   It is worth noting I did not have time in this exercise to implement a Dependency Injection Container pattern, which would have required using a third party DI Container implementation in this version of .NET, so the actual injection of the dependencies in the web app shows dependency injection in its most basic form.

4. **Security and Validation** - I added validation to the single text box on the web app to whitelist the allowed characters. The purpose of this, as well as to help guide the user, was to stop the user from entering characters that could enable Cross Site Scripting attacks such as "*<script>alert('injection!')</script>*". Also limiting input helps avoid input of characters that could cause exceptions, which can help mitigate DDOS attacks that could try to target multiple problematic calls at the same time that are more costly that standard happy path calls. This validation is implemented both on the server side and on the client. The client side validation is preferable as it avoids anything even getting to the server but it's easy for an attacker to disable javascript, or make calls using something like Postman, making the duplication of validation on the server necessary.

It is worth pointing out that, due to focusing on the items above and having a limited time for the implementation, some things that would have been nice to include with more time but were dropped in this implementations were:

1. CSS and in particular styling of the table in the UI.
2. Better unit test coverage including tests of the controller in the web app.
3. Exception handling and logging, especially around the calls to the external api to enable better understanding of any reported errors.

## Troubleshooting
Note that when I clone the repo locally and try to run UserSearchWeb Web App I get an error like the following:

*"Could not find a part of the path 'C:\Users\phill\source\repos\UserSearch2\UserSearch\UserSearchWeb\bin\roslyn\csc.exe'."*

If you get similar, I find running the following command in the Package Manager Console (VS2017 --> Tools --> NuGet Package Manager --> Package Manager Console) resolves the issue...

**Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r**

This was a solution I found at this stack overflow post:

https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe

