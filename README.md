# .Net 8 Web Api For Driver 

# Tech Stack Used
- .Net 8
- SqLite
- Dapper
- Serilog with File Logging


# Running Instructions
- clone the project
- open using vs 2022 latest version to support .net 8
- rebuild solution
- select Api project as launch profile
- start the api , swagger should be showing
- to add a driver use the add api
- to add 10 drivers use addRandomDrivers api

# Features
- CRUD api to add , update , delete , list all drivers
- Api to generate 10 random drivers with random names , emails , phones
- Api to return Driver name Alphabetized by driver id
- Generic Repository Pattern
- Layered Clean Architecture
- Full Unit Test Code Coverage for all layers
- Exception And Error Handling Middleware
- Api Versioning (only v1 there, we can add multiple versions as needed)
- Serilog logging to file provider for simplicity
- Dependency Injection

# Layers
- Api layer ( contains the api exposed to the consumers )
- Application Layer ( contains the business logic of driver feature )
- Common Layer ( Contains Abstraction interfaces to connect Application layer with Infrastructure and domain layer  , other helpers for the application)
- Domain Layer ( Contains the domain entities )
- Infrastructure Layer ( Datbase access layer )

![image](https://github.com/mhmoudalaskalany/Driver-Backend/assets/45127300/9b6497a6-d1dc-4818-94b2-dcfa86086f8f)


# Code Coverage Result
![image](https://github.com/mhmoudalaskalany/Driver-Backend/assets/45127300/ade5e7a2-a45b-4f93-b83e-f59d511018f8)


