# Advertising Platform Selector (.NET Web API)

A fast and lightweight web service designed to help advertisers find relevant advertising platforms for a specific geographic region.
This project demonstrates clean REST API design, in-memory data storage, and efficient prefix-based search.

> This project is intended for educational and demonstration purposes. It is not production-ready without further validation, persistence, and security hardening.

---

## Table of Contents

- Features
- How Location Matching Works
- Getting Started
- Building from Source
- API Usage
- Under the Hood
- Testing
- License
- Acknowledgements

---

## Features

- Upload advertising platforms with associated location paths
- Query platforms by location, including nested region support
- Fast in-memory search optimized for frequent queries
- Robust handling of malformed input
- RESTful API with Swagger documentation
- Unit tests for core logic

---

Endpoint: POST /api/AdPlatform/load-text

Description: This method replaces all previously stored advertising platform data. 
You must send a JSON array of strings, where each string contains a platform name followed by a colon : and a comma-separated list of location paths.

Request Body (JSON):
[
  "Яндекс.Директ:/ru",
  "Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik",
  "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl",
  "Крутая реклама:/ru/svrd"
]


## How Location Matching Works

Each advertising platform is associated with one or more location paths, such as:


- Locations are hierarchical: `/ru/svrd/revda` is nested within `/ru/svrd`, which is nested within `/ru`.
- A platform is considered valid for a location if it is explicitly listed for that location or any of its parent paths.
- For example:
  - `/ru/msk` →  "Яндекс.Директ","Газета уральских москвичей"
  - `/ru/svrd` → "Яндекс.Директ","Крутая реклама"
  - `/ru/svrd/revda` →  "Яндекс.Директ", "Крутая реклама", "Ревдинский рабочий"
  - `/ru` → "Яндекс.Директ"
- Just input it in location field in Get method /api/AdPlatform/search. 
---

##  Getting Started

### Requirements

- .NET 6.0 SDK or newer

### Build and Run

```bash
git clone https://github.com/LiubovMtsvch/AdvertisingTask.git
cd AdvertisingTask
dotnet restore
dotnet run
