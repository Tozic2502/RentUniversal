RentUniversal

RentUniversal er et 3. semester gruppeprojekt, der demonstrerer et fuldt rentalsystem med backend-API, frontend-klienter og Docker-opsætning.

Projektet er bygget med ASP.NET Core, frontend i TypeScript, og kan køres samlet via Docker Compose.

Krav

Før projektet kan køres, skal følgende være installeret:

Docker

Docker Compose

.NET SDK (hvis projektet køres lokalt uden Docker)

Node.js (kun hvis frontend køres lokalt)

Sådan køres programmet (Docker – anbefalet)

Klon projektet:

git clone https://github.com/Tozic2502/RentUniversal.git
cd RentUniversal


Start hele systemet:

docker compose up --build


Når containerne kører:

Backend API starter automatisk

Frontend-klienter starter automatisk

Systemet er nu klar til brug

Stop systemet igen:

docker compose down

Kørsel uden Docker (valgfrit)
Backend API
dotnet run --project RentUniversal.api

Frontend
cd rentuniversal-client
npm install
npm run dev

Tests

Tests kan køres med:

dotnet test

Projektstruktur (kort)
RentUniversal/
├── RentUniversal.api        # Backend API
├── RentUniversal.Domain     # Domænemodeller
├── RentUniversal.Application# Services og forretningslogik
├── rentuniversal-client     # Frontend
├── RentUniversal.Tests      # Tests
└── compose.yaml             # Docker setup

Udviklet af

semester gruppeprojekt af
Mikkel, Youssef, Broder og Sebastian
