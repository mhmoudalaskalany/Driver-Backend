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
