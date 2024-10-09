# LibraryManagement API

## Introduction
This project is a Library API Management Solution designed to manage all the books, customers and reservations in the library which also provides authentication mechanism to the API using OAuth Identity Server Authentication (Server Centralized Authentication).


## 1. Prequesite
To launch the application properly on a dev environment will require typically Visual Studio 2022 and MSSQL Server 2022, the application is built primarily on .NET Core 8.0.


## 2. Project DB Set Up
Before the application can run, the database needs to be set up first. There are primarily 2 database back up files on this location: `src\main\resources\DbBackup`. The back up file `IdentityServerAuth.bak` is for the IdentityServer4 Authentication Mechanism (which is also an API), this is used to secure the API from public access. The second back up file is the actual application db `LibraryManagement.bak` which contains the data of the library (customers, books etc) that is being managed by the application. 


## 3. Project Start Up and Environment
The Operating System will require .NET Core 8.0 installed on it for the application to run properly, if you plan to run the application on Visual Studio (2022 ideally) then multiple projects start up needs to be configured which are the IdentityServer application that secures and generates token for the API and the LibraryManagement.Api application which is the REST API that manages the library resources and the API other clients (front-end) needs to consume.

Alternatively, the projects (IdentityServer and LibrayManagement.Api) can be ran without using a visual studio IDE but by navigating to the runtime location of the project after building the release version of  both project. For the IdentityServer, the executable file of the application after release build will be on: `src\main\dotnet\IdentityServer\bin\Release\net8.0` Then execute `IdentityServer.exe` (this can be ran on any Operating System). Same goes for the LibraryManagement.Api, the location after release build is: `src\main\dotnet\LibraryManagement.Api\bin\Release\net8.0` Then execute `LibraryManagement.Api.exe`.


## 4. Api Usage
i. Once the application is running, the Library Api launches the API documentation (Swagger Docs) or it can be accessed on `https://localhost:7189/swagger/index.html` once its been confirmed it is running. This provides all the endpoints and their usages for consumption.

ii. Under the resources folder in this location: `src\main\resources` their is the postman collection file with name: `LibraryManagement.postman_collection`. This contains the endpoints for calling the Library API. Basically, there is the endpoint to generate token, the book and customer folders are just endpoints to manage these entities while the BookReservation folder is where customers can reserve, borrow or return books. The Available Book Notification folder is where all available notifications can be seen that have been sent to customers.

iii. Lastly, below is a sample curl request that can be used to generate the OAuth bearer token which will be used to authenticate and call the library API:

`curl --location --request POST 'https://localhost:7182/connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id=library_app' \
--data-urlencode 'client_secret=console_app_secret' \
--data-urlencode 'scope=library_api' \
--data-urlencode 'grant_type=client_credentials'`


