 
# **ChatProject API**  
A real-time chat application built with **ASP.NET Core Web API**, implementing **CQRS, MediatR, and Clean Architecture** for maintainability, scalability, and performance.

## **Features**  
✅ Real-time messaging system  
✅ Clean Architecture for better separation of concerns  
✅ CQRS pattern using **MediatR** for handling requests  
✅ Secure authentication and authorization with **JWT**  
✅ **Entity Framework Core** with SQL Server  
✅ **SignalR** for real-time communication  
✅ Unit testing for core services  

## **Technologies Used**  
- **Backend:** ASP.NET Core Web API, CQRS, MediatR, SignalR  
- **Database:** SQL Server, Entity Framework Core  
- **Authentication:** JWT, Identity  
- **Architecture:** Clean Architecture, Repository Pattern  
 

## **Project Structure** (Clean Architecture)  
```
/ChatProject  
  ├── Application   # Business logic (CQRS, Handlers, DTOs)  
  ├── Domain        # Core entities and interfaces  
  ├── Infrastructure # Data access, repositories, and external services  
  ├── API           # Web API controllers and configurations  
  
```

