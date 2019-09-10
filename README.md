# ASP.Net Core with React & Redux boilerplate

Boilerplate solution for ASP.Net Core API + React + Redux including user authentication.

This solution contains two projects for back-end and front-end with small implementation of few endpoints and pages to showcase the registration, login and role authentication processes using JWT tokens.

### Requirements

- [.Net Core 2.2 SDK](https://dotnet.microsoft.com/download)
- [NodeJS](https://nodejs.org/en/)


### Getting started

1. Download the solution
2. Go to the back-end project root folder.
3. Run the project. This should open a tab in your default browser pointing to localhost and showing API version in JSON format.
```
dotnet run
```
4. Go to the front-end project root folder.
5. Download required packages.
```
npm install
```
6. Run the project. This should open a tab in your default browser pointing to localhost and showing a simple website.
```
npm start
```

**IMPORTANT NOTE:** In case of the back-end project localhost port is different to 3000, you must change the pointing URL port of the baseUrl variable in the **apiUtils.js** file of the front-end project.


---

## Back-end

The RESTful API for back-end has been done using ASP.Net Core 2.2.

For the data management the project uses Entity Framework Core with the in memory database using [Code First](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database) approach. Replacing this by a real database connection should be straight forward.

[Entity Type Configurations](https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.modelconfiguration.entitytypeconfiguration-1?view=entity-framework-6.2.0) has being used for the definition of database models. 
For each model is recommended to generate and add in the database context class his own configuration file.

User configuration example:

```csharp
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(d => d.UserId);
        builder.Property(d => d.Email).HasMaxLength(150).IsRequired();

        builder.Property(d => d.Password).HasMaxLength(512).IsRequired();

        builder.Ignore(d => d.EncryptionService);
    }
}
```

### Authentication

For the authentication [JWT token](https://jwt.io/) approach has been used.

The implementation is a bit custom using a role system to give different levels of authorization to the endpoints.

Also, the refresh token mechanism is included in the project.

### Layers

The code structure implements a kind of DDD (Domain Driven Design) but in a very custom and personal approach.

The different layers of the back-end project are separated and connected as follows:

Controllers > Commands > Domain objects/Services/Repositories

#### Controllers

Here we have the API endpoints implementation. We don't do any business logic here, we just validate the inputs, convert the data into **DTOs** (Data Transfer Objects) to send to the Commands and return the results as a view model objects.

View models are classes to strongly define the returned data by the endpoint. They must be unique per each endpoint to avoid conflicts or breaking compatibility after changes.

Input models are the classes to strongly define the expected payload by the endpoint. They must be unique per each endpoint to avoid conflicts or breaking compatibility after changes.

Controllers can only use Commands, they are not allowed to use any Service or data management.

#### Commands

The Commands contain the business logic of the application. They can use Services, Repositories and Domain Objects to process the required functionality of the endpoint.

#### Services

Pieces of code implementing something very specific but at the same reusable and generic for any situation. They are not usually related with any specific business logic. Here we can also include integrations with third party APIs.

Some examples of services could be encryption methods, third party API integration for sending SMS messages, or complex math calculations.

#### Repositories

Data Repositories is the layer responsible of data management. They must be used for any database operation such as insert, update, delete, etc... This project contains an abstraction layer on top of Entity Framework in case of someone wants to use a different ORM.

#### Domain Objects

Domain Objects are our data representation. They have some restrictions and protected properties to apply security about how to use them.

Often, they are our the database entities also but they don't need to always necessary be our persistent data representation. They can be used to define our business flowing data.

### Unit test

A simple example of unit test project has been added using the **xunit** framework, **Moq** for mocking-up the data, and **FluentAssertions** library for the assertions.

---

## Front-end

The front-end website has been done using [React](https://reactjs.org/) combined with [Redux](https://redux.js.org/).

In this case [Redux Starter Kit](https://redux-starter-kit.js.org/) was used, because simplifies the actions and reducers structure making cleaner code.

Also, [React-router](https://www.npmjs.com/package/react-router) and [Sass](https://sass-lang.com/) were included in this boilerplate.

### Authentication

The **authSlice** file contains the reducers about login credentials.
To keep the token credentials through the different browser sessions, the project creates a subscription of authSlice state to store the data in the local storage and reading later using persisted states.

```javascript
const persistedState = loadState();

const store = configureStore({
  reducer: rootReducer,
  preloadedState: persistedState
});

store.subscribe(() => {
  saveState({ auth: store.getState().auth });
});
```

On the other hand, **React-router** uses the function **requireAuthentication** to intercepts non authenticated users before components are loaded.

### API requests

A class called **API** was created to execute all HTTP requests using [fetch](https://github.com/github/fetch).

This class will include the bearer token in the headers when users are authenticated. Also, it will detect if the token is expired and will execute a refresh token process seamlessly.

