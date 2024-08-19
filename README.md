# CDMS - Custody Database Management System
## Overview
CDMS (Custody Database Management System) is a .NET Core 8.0 MVC application developed to manage custody items within an organization efficiently. The system allows users to track, assign, and monitor custody items, ensuring that all relevant data is organized and accessible.

## Table of Contents
- [Features](#features)
- [Technical Details](#technical-details)
- [Architecture](#architecture)
- [Database Schema](#database-schema)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
  - [Devices and Categories](#devices-and-categories)
  - [Screens](#screens)
  - [User Management](#user-management)
- [Themes](#themes)
- [Email Notifications](#email-notifications)
- [Security Features](#security-features)
- [Contributing](#contributing)
- [License](#license)

## Features
- **Multi-Theme Support**: Users can select from over 26 different themes to personalize their experience.
- **Email Notifications**: Automatic notifications for various events, such as new custody assignments.
- **User Management**: Including email verification and account lockout for enhanced security.
- **N-Tier Architecture**: Separation of concerns is maintained by organizing the application into multiple layers.

## Technical Details
- **Framework**: .NET Core 8.0 MVC
- **Database**: Entity Framework Core
- **Identity Management**: ASP.NET Core Identity for user authentication and authorization.
- **Architecture**: N-Tier, utilizing layers for data access, business logic, and presentation.
- **UI/UX**: Bootstrap and other front-end technologies to create a responsive and user-friendly interface.

## Architecture
The application follows the N-Tier architecture, with a clear separation of concerns across the following layers:
1. **Presentation Layer**: Handles the user interface, including MVC controllers and views.
2. **Business Logic Layer (BLL)**: Contains the core business logic.
3. **Data Access Layer (DAL)**: Manages data retrieval and storage, using EF Core for ORM.
4. **Common Layer**: Includes shared models and utility functions used across the application.

## Database Schema
The database schema is designed to handle the relationships between users, devices, categories, and custody items. Entity Framework Core is used to manage database migrations and updates.

### Key Tables:
- **Users**: Stores user information and roles.
- **Devices**: Catalog of devices available for custody.
- **Categories**: Classification of devices into categories.
- **CustodyItems**: Tracks the assignment of devices to users.

## Setup and Installation
To set up the application on your local machine:
1. Clone the repository.
2. Restore NuGet packages.
3. Apply migrations to your database using EF Core.
4. Run the application using Visual Studio or the .NET CLI.

```bash
git clone https://github.com/YourUsername/CDMS.git
cd CDMS
dotnet restore
dotnet ef database update
dotnet run
