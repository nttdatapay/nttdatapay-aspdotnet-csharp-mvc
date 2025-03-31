# nttdatapay-aspdotnet-csharp-mvc

## Introduction

This is an example of integration code for ASP.NET-MVC (C#), illustrating the process of incorporating the NTTDATA Payment Gateway into your .NET application.

## Prerequisites

- .NET framwork 4.7.2

## Project Structure

The project contains the following files and folder: 
- Models:Contain classes for require entities/POJO. 
- Views: 
    - Home(index) - contain sample page to initiate the checkout page . Response(response)-Contain sample page to show response. 
- Controllers: 
    - HomeController - Contain the code for initiating payment requests,generating tokens and encryption logic)
    - ResponseController - Contain the code for for capturing payment responses and decryption logic.

## Integration

1. Ensure that the .NET Framework version 4.7.2 is installed . 
2. Add all require namespaces in page. 
3. Copy the code from the controller and view and past to the desired event. 
4. Modify the request and the keys used for encryption and decryption. 
5. Update the UAT and Production CDN link.
