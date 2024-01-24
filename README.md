Currency Rate Application
=========================

Overview
--------

Welcome to the Currency Rate Application, a robust and efficient .NET 8.0-based web API developed to provide up-to-date currency rate information. This application is designed to fetch and compute currency rate changes, offering a user-friendly API interface for accessing and managing this financial data. Developed with Clean Architecture, it ensures a high degree of separation of concerns, making the application scalable and easy to maintain.

Key Features
------------

*   **Real-Time Currency Rate Fetching**: Retrieves the latest currency rates and calculates their changes based on specific dates.
*   **In-Memory Data Storage (Work in Progress)**: Incorporates an in-memory database for storing and managing currency rate data, currently under active development.
*   **Currency Rate by Code**: Facilitates fetching currency rates by specific currency codes, like EUR, USD.
*   **Clean Architecture**: The application is neatly organized into Domain, Application, Infrastructure, and Presentation (Web API) layers.
*   **Robust Exception Handling**: Implements comprehensive error handling and response formatting through middleware.

How It Works
------------

*   **Data Retrieval**: The application sources currency data from an external service (`www.lb.lt/webservices/ExchangeRates/ExchangeRates.asmx`) and processes it to deduce rate changes.
*   **In-Memory Database**: Initially populated with hardcoded data, which is planned to be replaced by live data from the external service as the in-memory database development progresses.
*   **API Endpoints**: Provides endpoints for retrieving currency rate changes by date and for obtaining individual currency rates by their codes.

Getting Started
---------------

### Prerequisites

*   .NET 8.0 SDK
*   A preferred Integrated Development Environment (IDE), such as Visual Studio or Visual Studio Code

### Running the Application

1.  **Clone the Repository**: Use `git clone` to clone this repository to your local machine.
    
2.  **Project Directory**: Change your directory to the root of the cloned project.
    
3.  **Build the Application**: Execute `dotnet build` to build the application.
    
4.  **Run the Application**: Run `dotnet run` within the `Exchange.WebAPI` directory.
    
5.  **API Access**: Upon running, the web API will be hosted locally. Access it via your browser or API client at `http://localhost:[port]/`.
    

### Using the API

*   To **Fetch Currency Rate Changes**: Make a GET request to `/currencyRates/changes?date={date}`
*   To **Get a Currency Rate by Code**: Make a GET request to `/currencyRates/{code}`

Replace `{date}` with your desired date and `{code}` with the currency code you are inquiring about.

API Documentation
-----------------

Explore detailed Swagger documentation for all API endpoints by navigating to `http://localhost:[port]/swagger` when the application is running.

* * *

**Note**: Be sure to replace `[port]` with the actual port number where your application runs. Additionally, adjust any specific details such as API routes or special instructions according to your application's actual implementation and configuration.
