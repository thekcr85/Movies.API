# Movies.API

Minimal API do zarządzania filmami, zbudowane w .NET 9.

## Technologie
- .NET 9
- Entity Framework Core
- AutoMapper
- FluentValidation

## Instalacja i uruchomienie
1. **Sklonuj repozytorium:**
   ```bash
   git clone https://github.com/uzytkownik/Movies.API.git
   cd Movies.API
   ```
2. **Uruchom aplikację:**
   ```bash
   dotnet run
   ```

## Endpointy
- `GET /movies` – Pobiera listę filmów
- `GET /movies/{id}` – Pobiera film po ID
- `POST /movies` – Dodaje nowy film
- `PUT /movies/{id}` – Aktualizuje film
- `DELETE /movies/{id}` – Usuwa film
- `GET /health` – Sprawdza stan aplikacji

## Konfiguracja bazy danych
Aplikacja domyślnie używa SQL Server. Połączenie można skonfigurować w pliku `appsettings.json`:

```json
"ConnectionStrings": {  
  "DefaultConnection": "Server=YOUR_SERVER;Database=MoviesDb;Trusted_Connection=True;TrustServerCertificate=True"  
}
```

## Wymagania
- .NET 9 SDK
- SQL Server
- Opcjonalnie: Postman lub inny klient API do testowania endpointów

## Licencja
Projekt dostępny na licencji MIT.
