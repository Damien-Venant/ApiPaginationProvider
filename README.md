# ApiPagination

## 📌 Description

This project aims to provide a **simple, flexible, and extensible REST API pagination solution for .NET**, based on **LINQ** and **data providers**.

## 🎯 Goals

- Provide **generic pagination** based on LINQ
- Offer an **extensible and testable** architecture
- Centralize pagination logic (avoid code duplication)

## 🧱 Architecture

The project is built around the following concepts:

- **LINQ Providers**
- **Data source abstraction**

### Main Components

- `SkipTake`: represents pagination parameters (skip, take)
- `ApiPaginationQueryProvider`: Provider to make synchronous and deferable request to an API
- `ApiPaginationQueryProviderAsync`: Provider to map a async request to synchronous deferable request
- LINQ providers:
  - Provider for `IQueryable<T>`

## 🔄 Features

- Page and page-size based pagination
- LINQ support (`Skip`, `Take`)

## 📥 Installation

Add the project to your .NET solution:

```bash
dotnet add reference ApiPagination