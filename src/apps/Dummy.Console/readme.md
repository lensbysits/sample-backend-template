This app show how to easily setup a console app.
It uses dependency injection,configuration and logging as one expects in a dotnet app.

We take a depency on CoreApp.Console.
We derive our program from ProgramBase.
We supply a method in which we can add our own service. In this case we add a background service that will print-out the time every 5 seconds.