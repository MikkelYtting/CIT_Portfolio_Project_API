## Aflevering: Subproject 2 — Backend (kort, dansk)

Denne fil opsummerer hvordan koden i projektet opfylder kravene i Subproject 2 (backend). Den er skrevet så du kan indsætte den i aflevering eller dokumentation.

## Overordnet konklusion
Koden opfylder i praksis størstedelen af kravene: der er tydelig lagdeling (Web/Controllers → Business/Managers → Repositories → EF/AppDbContext), DTO'er, paging, HATEOAS/self-links, JWT-baseret authentication, EF Core mapping, og både integrationstests og unit-test struktur. Én konkret teknisk sag er markeret som TODO i koden (hardcoded userId til visse søgninger) — se "Kendte undtagelser".

## Kort mapping krav → status
- 2-A (Arkitektur, klasse-/objektstruktur): OPFYLDT
  - Bevis i koden: lagdelingen og AutoMapper-profil (se `Program.cs`, `Infrastructure/Mapping/ApiMappingProfile.cs`, DTO-mappe `Application/DTOs`).

- 2-B (Data Access Layer): OPFYLDT (med lille undtagelse)
  - 2-B.1 (Domain model & ORM-mapping): OPFYLDT — `Infrastructure/Persistence/AppDbContext.cs` indeholder DbSet'er og `OnModelCreating()` med tabel/kolonne-mapping.
  - 2-B.2 (Repositories & CRUD): OPFYLDT — repositories findes i `Infrastructure/Repositories/Interfaces` og `.../Implementations` (fx `MovieRepository`, `UserRepository`, `BookmarkRepository`, `RatingRepository`). IMDB-data behandles som read-only (som ønsket).
  - 2-B.3 (Forbered til autentificering): DELVIST — interface- og DB-kald understøtter bruger-id, men `MovieRepository` bruger i nogle metoder hardcoded `userId = 1` (kommentar i koden). Se "Kendte undtagelser".

- 2-C (Web Service Layer / REST): OPFYLDT (med enkelte CRUD-variationer)
  - 2-C.1/2-C.2 (URI design for IMDB read-only): OPFYLDT — `Web/Controllers/MoviesController.cs`, `PeopleController` etc. tilbyder `/api/movies`, `/api/movies/{id}`, `/api/movies/search`, `/api/movies/structured`.
  - 2-C.3/2-C.4 (CRUD for framework-data): DELVIST OPFYLDT — `UsersController` (POST register, GET), `BookmarksController` (GET/POST/DELETE), `RatingsController` (POST). Create/read/delete er dækket, update (PUT) er ikke nødvendigvis implementeret for alle ressourcer.
  - 2-C.5 (Self-descriptive responses): OPFYLDT — HATEOAS-links genereres af managers (fx `MovieManager`) og `PageDto<T>` har `Links`-felt.
  - 2-C.6 (Paging): OPFYLDT — controllere tager `page`/`pageSize`, `PagingValidationFilter` validerer input, repositories og managers returnerer `PageDto<T>` med `Total`, `Page`, `PageSize` og prev/next links.

- 2-D (Security): OPFYLDT
  - JWT: `Infrastructure/Security/JwtTokenService.cs` genererer JWT, `Program.cs` konfigurerer `AddJwtBearer(...)`. Password hashing findes i `Infrastructure/Security/PasswordHasher.cs` (PBKDF2).
  - `[Authorize]` anvendes i beskyttede controllere (fx `BookmarksController`, `RatingsController`). Claims-udlæsning udføres via `User.GetUserId()` extension i `UserClaimsExtensions.cs`.

- 2-E (Testing): OPFYLDT
  - Der findes integrationstests (`test/CIT_Portfolio_Project_API.IntegrationTests`) som dækker JWT flow og endpoints. Unit-tests-folder er også til stede.

## Kendte undtagelser / områder der bør nævnes
1) Hardcoded userId i `MovieRepository` ved søge-kald
   - Fil: `Infrastructure/Repositories/Implementations/MovieRepository.cs`
   - Problem: Metoder `SearchAsync` og `StructuredSearchAsync` kalder DB-funktioner med `userId = 1`. Kommentaren i koden siger: "TODO: Plumb authenticated user id or delegate to SearchRepository."
   - Effekt: Hvis du ønsker at logge rigtig søgehistorik per bruger i DB-funktionerne, skal den autentificerede bruger-id sendes igennem (fx fra controller via manager -> repository), eller MovieController Search bør delegere til `SearchRepository` som allerede accepterer `userId`.

2) CRUD-coverage
   - Ikke alle ressourcer har eksplicit `PUT`/update endpoints. Hvis opgaven absolut kræver fuld CRUD for samtlige framework-ressourcer, skal enkelte `PUT` endpoints implementeres.

3) XML output
   - Projektet leverer JSON som standard. Hvis eksaminator kræver XML som alternativ content-type, kan man tilføje XML-formatters i `Program.cs`.

4) Dokumentation/UML
   - Koden indeholder DTO-klasser, men ikke vedlagte UML-diagrammer eller et skrevet afsnit med JSON-eksempler. Opgaven bad om klasse-diagrammer og dokumentation af JSON-objekterne (det er dokumentationsarbejde, ikke kode). Jeg kan generere et simpelt diagram og JSON-eksempler hvis du ønsker det.

## Henvisninger til de vigtigste filer (brug som bevis i aflevering)
- Arkitektur og DI: `Program.cs`
- JWT token + konfiguration: `Infrastructure/Security/JwtTokenService.cs` og `appsettings.json` (sektion `Jwt`)
- Password hashing: `Infrastructure/Security/PasswordHasher.cs`
- DbContext, ORM-mapping og DB-funktioner: `Infrastructure/Persistence/AppDbContext.cs`
- Repositories: `Infrastructure/Repositories/Interfaces/*` og `Infrastructure/Repositories/Implementations/*` (fx `MovieRepository.cs`, `UserRepository.cs`)
- Business layer / managers: `Application/Managers/Implementations/*` (fx `MovieManager.cs`, `SearchManager.cs`)
- Controllers (Web Service Layer): `Web/Controllers/*` (fx `MoviesController.cs`, `AuthController.cs`, `UsersController.cs`, `BookmarksController.cs`, `RatingsController.cs`, `SearchController.cs`)
- Paging: `Web/Filters/PagingValidationFilter.cs` og `Application/DTOs/PageDto.cs`
- Tests: `test/CIT_Portfolio_Project_API.IntegrationTests/*`

## Forslag til små next-steps (ingen kodeændringer nødvendige for aflevering, men nyttige at notere)
- Kort og vigtig: Fix/implementér at MovieRepository bruger den autentificerede user-id i søge-kald (eller lad MovieController Search delegere til `SearchRepository`). Dette er lavrisiko og fjerner en tydelig TODO fra koden.
- Hvis ønsket: tilføj PUT/UPDATE endpoints for resources hvor nødvendigt (fx opdatering af bookmark-note eller opdatering af rating i stedet for kun POST).
- Tilføj en kort dokumentationssektion (README eller PDF) med: UML diagram (høj-niveau), JSON-eksempel på `PageDto<MovieDto>` inkl. links, og liste over vigtige filer (de ovenfor nævnte). Jeg kan generere dette for dig.

## Forslag til tekst du kan indsætte i afleveringsdokument
Her er et kort udkast (dansk) du kan kopiere ind i afleveringens rapport:

> Backend-arkitektur: Projektet er opdelt i Web (Controllers), Business (Managers), Infrastructure (Repositories + EF Core DbContext) og Application (DTOs). Kommunikation mellem lag sker via DTO'er (AutoMapper), og afhængigheder håndteres via DI konfigureret i `Program.cs`.
>
> Dataadgang: Entity Framework Core anvendes med en `AppDbContext` der kortlægger IMDB-tabeller og framework-tabeller. Repositories udstiller paginerede læseoperationer (Movie/Person) og CRUD for framework-data (Users, Bookmarks, Ratings).
>
> Webservice: RESTful endpoints følger resource-principper (fx `/api/movies`, `/api/movies/{id}`, `/api/users/{userId}/bookmarks`). Alle listemetoder understøtter paging med validering, og svarene indeholder HATEOAS-links (self/prev/next + per-item self link).
>
> Sikkerhed: Authentication er baseret på JWT. `JwtTokenService` genererer tokens, middleware validerer dem, og endpoints sikres med `[Authorize]`. Passwords hashes med PBKDF2.
>
> Tests: Der er integrationstests der dækker JWT-flow og key endpoints.
>
> Kendte begrænsninger: I `MovieRepository` er søgehistorik kald i øjeblikket plumbet med en hardcoded `userId=1`; dette bør rettes hvis korrekt bruger-søgehistorik ønskes. Derudover mangler eventuelle `PUT` endpoints hvis fuld CRUD forventes for alle resources.

---

Hvis du ønsker, kan jeg også oprette en kort README i repo med denne tekst samt et JSON-eksempel og et simpelt UML-billede (SVG eller ASCII) — vil du have at jeg tilføjer filen `docs/backend-assignment-report.md` direkte i repo'en? (Jeg har allerede oprettet denne fil.)

