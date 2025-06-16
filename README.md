# MyEventApp

A simple ticketing platform showcasing:

* **Event Listing**: View upcoming events in customizable time windows.
* **Sales Summary**: Toggle between top 5 events by ticket **quantity** or **revenue**, with search and pagination.

Built with:

* **Backend**: ASP.NET Core 8, NHibernate (SQLite)
* **Frontend**: Angular 19+, Tailwind CSS, Angular Material

---

## 📂 Repository Structure

```text
/MyEventApp
  /MyEventApp.Core                   # Domain models & interfaces
  /MyEventApp.Data                   # NHibernate mapping & repositories
  /MyEventApp.Api                    # ASP.NET Core Web API
    /Db/skillsAssessmentEvents.db    # Provided SQLite DB
    /Client
      /my-event-ui                   # Angular app
  /MyEventApp.Tests                  # NUnit tests         
  README.md                          # This file
  documents/
    Caching.md                       # System design & caching strategy
```

---

## ⚙️ Setup & Run

### 1. Backend (ASP.NET Core API)

**Prerequisites**: .NET 8 SDK, SQLite DB at `/MyEventApp.Api/Db/skillsAssessmentEvents.db`

### Using Visual Studio

### Open the Solution
- Launch Visual Studio 2022, open `MyEventApp.sln`, and ensure `MyEventApp.Api` is set as the startup project.
- Go to **Build** → **Build Solution** (or press `Ctrl+Shift+B`).
- Press `F5` (or `Ctrl+F5` to run without debugging).
 
### Using Command Line
- Open a Developer PowerShell.
- Run the following commands:
```bash
# Update connection string in appsettings.json if needed
# Build & Run
dotnet restore
dotnet build
dotnet run

```

* API: `https://localhost:7134`
* Swagger UI: `https://localhost:7134/swagger`

### 2. Run Unit Tests

You can run the unit tests using either Visual Studio or the command line.

### Using Visual Studio

- Open **Test Explorer** (**Test** → **Windows** → **Test Explorer**).
- Click **Run All**.

### Using Command Line

- Run the following command:

```
cd MyEventApp.Tests
dotnet test

```

### 3. Frontend (Angular App)

**Prerequisites**: Node.js v16+, Angular CLI

```bash
# Navigate to UI folder
cd client/my-event-ui

```
## Verify Proxy Configuration

- Ensure your `proxy.conf.json` is present in the frontend folder.
- Verify that your `package.json` has the correct start script:

```json
"scripts": {
  "start": "ng serve --proxy-config proxy.conf.json",
  // …
}

```
```
# Install & serve
npm install
npm run start
```

* Frontend: `http://localhost:4200`
  (All `/api` calls proxy to the backend.)



---

## 🛠️ Approach

1. **Clean Architecture**: Separate projects for Core, Data, API, Tests.
2. **NHibernate + Fluent Mapping**: Lightweight ORM, SQLite for demo.
3. **Dependency Injection**: Built‑in .NET DI for repositories & services.
4. **Angular Standalone Components**: Leverage Angular 19’s standalone feature for modularity.
5. **Styling**: Tailwind for utility first + Angular Material for accessible components.
6. **Caching & Design**: Documented in `documents/Caching.md` using Mermaid diagrams.

---

## 📌 Assumptions

* Event **dates** stored in UTC; frontend displays in local timezone.
* SQLite DB is pre‑populated; no migrations in code.
* No user authentication required for assignment scope.
* Sales summary queries precompute top 5 on request (no long‑running jobs).

---

## 📑 Best Practices & Documentation

* All public methods include XML/JsDoc comments.
* Code follows SOLID principles and consistent naming conventions.


---





*Happy coding!*
