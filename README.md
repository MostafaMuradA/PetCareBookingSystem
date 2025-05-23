# Pet Care Booking System

## Project Overview
A full-stack web application for pet care service booking, built with Angular (Frontend) and .NET (Backend). The system allows users to book various pet care services, manage their pets, and interact with service providers.

## Tech Stack
### Frontend
- Angular (Latest version)
- Standalone Components Architecture
- Angular Material UI
- TypeScript
- RxJS for state management
- Angular Router for navigation

### Backend
- .NET Core
- Entity Framework Core
- SQL Server
- RESTful API architecture
- JWT Authentication

## Features

### Authentication & Authorization
- User registration with form validation
- Login functionality with JWT token
- Protected routes and services
- Form state persistence management
- Automatic form clearing on component destruction

### Service Management
- Display of pet care services with images
- Support for both backend-stored and frontend images
- Service details viewing
- Service booking functionality

### User Interface
- Responsive design
- Modern and intuitive dashboard
- Clean navigation between components
- Form validation with user feedback
- Image display and management

## Project Structure

```
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   ├── services/
│   │   │   ├── interceptors/
│   │   │   └── models/
│   │   ├── assets/
│   │   └── environments/
│   └── package.json
│
└── backend/
    └── PetCareBookingSystem/
        ├── Controllers/
        ├── Models/
        ├── Services/
        └── DTOs/
```

## Recent Updates
1. Migrated to standalone components architecture
   - Removed traditional NgModule approach
   - Updated HTTP client and animations configuration
   - Reconfigured app.config.ts and auth interceptor

2. Enhanced Authentication Flow
   - Improved navigation between login and register forms
   - Added proper module imports (CommonModule, FormsModule, etc.)
   - Implemented secure token handling

3. Form Management Improvements
   - Added form state clearing functionality
   - Implemented OnDestroy interface for cleanup
   - Created dedicated form initialization methods
   - Enhanced form state management

## Getting Started

### Prerequisites
- Node.js and npm
- .NET SDK
- SQL Server

### Frontend Setup
```bash
cd frontend
npm install
ng serve
```

### Backend Setup
```bash
cd backend/PetCareBookingSystem
dotnet restore
dotnet run
```

## Contributing
1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License
This project is licensed under the MIT License - see the LICENSE file for details.