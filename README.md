# Tk.BlazorGrid
> Since Blazor is relatively new and finding free, customizable grid component proved difficult. This is a venture to give the dev complete customization capabilty in implementing a grid component which uses server side paged data using httpget requests.This is a work in progress and for the most part works as expected, however improvements can be made and code can be organized better. 

> A simple low conf grid implementation using WASM Blazor. Includes paging, sorting and searching.

The application is written in the **Blazor WASM** **Asp.Net Core MVC - using .NET Core 3.1** **AspNetCore Hosted**

## Requirements
Visual Studio 2019 with .Net Core 3.1 installed

## Installation
- Application uses InMemory Database so no installation required to run this application

## How to use in your project
# Shared
- Copy as is from **Tk.BlazorGrid.Shared** folders 1.Common 2.Extensions 3.Helpers
- Paged Dto needs to be in the form as shown in **WeatherForecastsDto** located in **Tk.BlazorGrid.Shared**
# Server/ Api
- Customize HttpGet request for paged data as shown in **WeatherForecastController** in **Tk.BlazorGrid.Server** project.
# WASM Client
- In the Shared folder add the **Common** which contain 1.PagerDisplayColumnHeader 2.PagerDisplayFooter 3.PagerDisplayHeader  
- Add refrences for above files to _Imports.razor.
- Customize UI per fetchdata.razor

## Acknowledgements
This web application is based on these projects:

- ASP.NET Core
- IdentityServer4.EntityFramework
- ASP.NET Core Identity
- Bootstrap 4

## Roadmap & Vision
- 1.0.0 Create a self supported standalone component

## Contributors
Contributors welcome
