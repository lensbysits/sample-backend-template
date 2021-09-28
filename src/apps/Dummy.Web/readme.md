This app show how to easily setup a web api app.
It uses dependency injection,configuration and logging as one expects in a dotnet app out of the box and well configured.

We take a depency on CoreApp.Web.
We derive our program from ProgramBase.
We derive our startup from StartupBase.

We add a controller.
We add a service.
We add the service to the dependency injection framework in the startup class (using an extentionmethod).