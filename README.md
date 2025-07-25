# Entre2Ages

This repository contains a collection of small ASP.NET Core microservices used for the Entre2Ages project. Each service is packaged with Docker and can be launched all together with `docker-compose`.

## Services
- **UserMicroservice** – manages user accounts (PostgreSQL).
- **EventMicroservice** – handles events between users (PostgreSQL).
- **MediaMicroservice** – stores uploaded media information (PostgreSQL).
- **MessageMicroservice** – manages chat messages (MongoDB).
- **Gateway** – Ocelot API gateway that exposes the microservices.
- **BlazorEntre2Ages** – Blazor front‑end.

## Getting started
1. Install [.NET 5 SDK](https://dotnet.microsoft.com/download) and [Docker](https://www.docker.com/).
2. Copy the `*.env.example` files to `*.env` and adjust the credentials.
3. Run `docker-compose up --build` to start the databases and services.

Each microservice exposes a REST API described in its own project. The gateway is available on port `9000` by default.

## Tests
Unit tests are located in the `*.Tests` projects. Execute `dotnet test` from the repository root to run them.

## License
This project is released under the MIT license. See [LICENSE](LICENSE) for details.
