
# Cargo Data Collector

This application merges the sensors data file coming from two related devices and creates a single list of merged response.

# Tech stack
Application is build using .NET5 and .NetStandard libraries. Unit tests were written using NUnit library.

# Usage
Clone/download the project, open, compile and run it using Visual Studio 2019 or 2022.


# Project Structure
This solution is designed in Layer architecture and contains following projects.
- CargoDataCollector 
    - A .NET5 executeable project, contains Program.cs file. 
    - Configurations/settings will also come under this project
- CargoDataCollector.Services
    - A .NetStandard project, contains Service(s) classes/Business logic for Data Collector
    - ServiceRunner class processes the input file path, deserialize the data polymorphically, prepare the response, serialize it and writes the content in provided output file.
- CargoDataCollector.Models
    - A .NetStandard project contains the classes/models structure of the solution
    - Models are designed in a way that verdors classes should implement IVendor interface and every vendor will provide their concrete implementations (e.g. Company and Partner)
    - Similarly Devices needs to implement from IDevice interface and can provide their concrete implementation (e.g. Device and Tracker)
    - Calculating average values, count, start and last date of of sensors is the responsibility of Devices and preparing results comes under the reponsibility of Vendor classes.
- CargoDataCollector.Tests
    - A .NET5 test project that uses NUnit for unit testing. Currently unit tests written for ServiceRunner class to test the business logic of application and it covers 80+ code coverage. and 
    - TODO: Tests for Devices and Tracker classes (to verify calculated averages, count and datetime values)

# Docker
- TODO

# CI/CD
- TODO



