# RentUniversal

RentUniversal er et 3. semester gruppeprojekt, der demonstrerer et fuldt rentalsystem med backend-API, frontend-klienter og Docker-opsætning.

Projektet er bygget med ASP.NET Core, frontend i TypeScript, og kan køres samlet via Docker Compose.

Semester gruppeprojektet er udviklet af:
```
Broder
Mikkel
Youssef
Sebastian
```

---

## Forudsætninger

RentUniversal er et Docker-baseret projekt, der gør det nemt at starte, stoppe og administrere alle services via **Docker Compose**.
Før du går i gang, skal følgende være installeret:

- Docker

- Docker Compose

- .NET SDK (hvis projektet køres lokalt uden Docker)

- Node.js (kun hvis frontend køres lokalt)

---

## Projektstruktur (kort)

```
Projektstruktur (kort)
RentUniversal/
├── RentUniversal.api        # Backend API
├── RentUniversal.Domain     # Domænemodeller
├── RentUniversal.Application# Services og forretningslogik
├── rentuniversal-client     # Frontend
├── RentUniversal.Tests      # Tests
└── compose.yaml             # Docker setup
```

---

## Kom i gang

### Klon projekt
```bash
git clone https://github.com/Tozic2502/RentUniversal.git
cd RentUniversal
```

### Start Docker og genbyg containere
Starter alle services og genbygger images:
```bash
docker compose up --build
```

Når containerne kører:

- Backend API starter automatisk

- Frontend-klienter starter automatisk

- Systemet er nu klar til brug

---

## Genstart

### Start Docker (uden rebuild)
```bash
docker compose up
```

### Genstart kun API-servicen (Eksempelvis)
```bash
docker compose restart api
```

---

## Stop & Oprydning

### Stop Docker og slet volumes
```bash
docker compose down --v
```

---

## Debugging

## Se logs
```bash
docker compose logs
```

## Se aktive containers
```bash
docker ps
```
---

## Kørsel uden Docker (valgfrit)

### Backend API
```bash
dotnet run --project RentUniversal.api
```

### Frontend
```bash
cd rentuniversal-client
npm install
npm run dev
```

---

## Tests

Tests kan køres med:
```bash
dotnet test
```




