# CDMS - Custody Database Management System
![CDMS](https://github.com/JoeGitHubPro/CDMS/blob/main/Documents/1.png)
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
```
## Usage

### Screens
The application features multiple screens to manage and view custody items, users, and categories. Key screens include:
- **Dashboard**: Overview of the system's current state.
- **Custody Management**: Assign, track, and manage custody items.
- **User Management**: Add, remove, and update user information.
- 
### Devices and Categories
The application supports the management of various devices and their categorization. Admin users can add new devices and categorize them for better organization.

![Devices and Categories Screenshot](https://github.com/JoeGitHubPro/CDMS/blob/main/Documents/2.png)


### User Management
- **Email Verification**: Ensures that users verify their email addresses before gaining full access.
- **Account Lockout**: Protects against unauthorized access by locking out users after multiple failed login attempts.

![User Management](https://github.com/JoeGitHubPro/CDMS/blob/main/Documents/3.png)

## Themes
CDMS allows users to personalize their experience by choosing from over 26 different themes. This feature enhances usability and ensures that the application can cater to different user preferences.

![Theme Screenshot](https://github.com/JoeGitHubPro/CDMS/blob/main/Documents/4.png)

## Email Notifications
Email notifications are sent automatically for key events, such as:
- New custody assignments
- Changes in custody status
- User registration and email verification

## Security Features
- **Email Verification**: Users must verify their email to activate their account.
- **Account Lockout**: The system locks out users after several failed login attempts, adding an extra layer of security.
- **Role-Based Access Control**: Permissions are managed based on user roles, ensuring that only authorized users can perform certain actions.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

