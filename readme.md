# RJ ChatBot

This consists of 3 projects (2 net core and 1 angular).
The two net core api's use RabbitMQ as message broker.

## Run the project

RabbitMQ should be running in localhost (this can be changed in appsettings.json in each project);
Also have the .net core 3.0 (https://dotnet.microsoft.com/download/dotnet-core/3.0) installed.
1. Open cmd o bash and start the bot with: 
$ cd romeojovelchatbot && dotnet restore && dotnet run --launch-profile dev
2. Open cmd o bash and start the chat api with: 
$ cd romeojovelchatapi && dotnet restore && dotnet run --launch-profile dev
3. open cmd o bash and start the angular front with:
$ cd romeojovelchatangular && npm install && ng serve

## Custom setting
The angular project uses the enviroment.ts properties for the api url.
The api uses the appsetting.json for JWT secret.
Both the bot and the api uses the appsetting.json for RabbitMQ host.
