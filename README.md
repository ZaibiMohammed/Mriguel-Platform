# Mriguel

A .NET 9 backend for a neighborhood sharing platform. The application enables users to rent items to and from other users in their neighborhood.

## Architecture

This project follows Clean Architecture principles with Domain-Driven Design:

### Core Layer
- **Domain**: Contains business entities, value objects, domain events, and business logic
- **Application**: Contains business use cases, commands/queries (CQRS), and interfaces for infrastructure

### Infrastructure Layer
- **Persistence**: Database access using Entity Framework Core 9
- **Identity**: Authentication and authorization using Identity Server
- **Infrastructure**: External services integration (payments, emails, etc.)

### Presentation Layer
- **API**: REST API controllers and GraphQL endpoints
- **SignalR**: Real-time communication hubs

## Key Features

- User management with profiles, ratings, and verifications
- Item management with images, categories, and availability
- Rental system with comprehensive lifecycle (request, accept/decline, payment, pickup, return)
- Real-time messaging between renters and owners
- Real-time notifications
- Rating and review system
- Search and filtering of items

## Technical Stack

- **.NET 9**: The latest version of .NET framework
- **Entity Framework Core 9**: ORM for database access
- **MediatR**: Implementation of CQRS and Mediator patterns
- **FluentValidation**: For input validation
- **AutoMapper**: For object-to-object mapping
- **SignalR**: For real-time communications
- **Identity Server**: For authentication and authorization
- **JWT Authentication**: For secure API access
- **Serilog**: For structured logging
- **xUnit**: For unit and integration testing
- **AWS S3**: For file storage
- **SMTP**: For email notifications

## Design Patterns

- **Clean Architecture**: For separation of concerns and maintainability
- **Domain-Driven Design (DDD)**: For complex business logic modeling
- **Command Query Responsibility Segregation (CQRS)**: For separating read and write operations
- **Repository Pattern**: For data access abstraction
- **Unit of Work**: For transaction management
- **Mediator Pattern**: For decoupling request handlers
- **Specification Pattern**: For encapsulating query criteria
- **Domain Events**: For loose coupling between domain operations

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server or PostgreSQL
- Visual Studio 2025 or VS Code
- AWS Account (for S3 file storage)
- SMTP Server (for email notifications)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/Mriguel.git
cd Mriguel
```

2. Update the connection strings in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Your_Connection_String_Here",
  "IdentityConnection": "Your_Identity_Connection_String_Here"
}
```

3. Update the JWT settings in `appsettings.json`:
```json
"JwtSettings": {
  "Secret": "Your_Super_Secret_Key_Here_At_Least_32_Characters",
  "Issuer": "Mriguel",
  "Audience": "MriguelApi",
  "ExpirationDays": 7
}
```

4. Update AWS S3 settings in `appsettings.json`:
```json
"AWS": {
  "S3": {
    "BucketName": "your-bucket-name",
    "BucketUrl": "https://your-bucket-url"
  }
}
```

5. Update Email settings in `appsettings.json`:
```json
"Email": {
  "From": "noreply@yourdomain.com",
  "SmtpServer": "your-smtp-server",
  "Port": 587,
  "Username": "your-username",
  "Password": "your-password"
}
```

6. Run the migrations:
```bash
dotnet ef database update --project src/Infrastructure/Persistence --startup-project src/Presentation/API
dotnet ef database update --project src/Infrastructure/Identity --startup-project src/Presentation/API
```

7. Build and run the project:
```bash
dotnet build
dotnet run --project src/Presentation/API/Mriguel.API.csproj
```

## Project Structure

```
Mriguel/
├── src/
│   ├── Core/
│   │   ├── Domain/
│   │   │   ├── Common/
│   │   │   ├── Entities/
│   │   │   ├── Enums/
│   │   │   ├── Events/
│   │   │   ├── Exceptions/
│   │   │   └── ValueObjects/
│   │   └── Application/
│   │       ├── Common/
│   │       ├── Items/
│   │       ├── Rentals/
│   │       └── Users/
│   ├── Infrastructure/
│   │   ├── Persistence/
│   │   ├── Identity/
│   │   └── Infrastructure/
│   └── Presentation/
│       ├── API/
│       └── SignalR/
└── tests/
    ├── Mriguel.UnitTests/
    └── Mriguel.IntegrationTests/
```

## Testing

### Running Unit Tests
```bash
dotnet test tests/Mriguel.UnitTests
```

### Running Integration Tests
```bash
dotnet test tests/Mriguel.IntegrationTests
```

## API Documentation

Once the application is running, you can access the Swagger documentation at:
```
https://localhost:5001/swagger
```

## Key Endpoints

### Authentication
- POST `/api/auth/login`: Login with email and password
- POST `/api/users`: Register a new user

### Users
- GET `/api/users/{id}`: Get user details
- PUT `/api/users/{id}`: Update user details
- POST `/api/users/{id}/avatar`: Upload user avatar

### Items
- GET `/api/items`: Get items with filtering and pagination
- GET `/api/items/{id}`: Get item details
- POST `/api/items`: Create a new item
- PUT `/api/items/{id}`: Update an item
- POST `/api/items/{id}/publish`: Publish an item
- POST `/api/items/{id}/images`: Upload item images

### Rentals
- GET `/api/rentals/{id}`: Get rental details
- POST `/api/rentals`: Create a new rental request
- POST `/api/rentals/{id}/accept`: Accept a rental request
- POST `/api/rentals/{id}/decline`: Decline a rental request
- POST `/api/rentals/{id}/cancel`: Cancel a rental
- POST `/api/rentals/{id}/payment`: Confirm payment
- POST `/api/rentals/{id}/pickup`: Confirm pickup
- POST `/api/rentals/{id}/return`: Confirm return
- POST `/api/rentals/{id}/review`: Add a review

## SignalR Hubs

### Chat Hub
- `/chatHub`: Real-time messaging between users
  - `JoinRentalChat(rentalId)`: Join a rental chat room
  - `LeaveRentalChat(rentalId)`: Leave a rental chat room
  - `SendMessage(rentalId, message)`: Send a message to a rental chat room
  - `NotifyTyping(rentalId)`: Notify when a user is typing

### Notification Hub
- `/notificationHub`: Real-time notifications
  - Client receives: `ReceiveNotification(notification)`: When a notification is sent

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/my-new-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/my-new-feature`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
