
# NASA NeoWs - WinForms Application

A C# Windows Forms application that integrates with the **NASA Near Earth Object Web Service (NeoWs) API**. The application fetches data regarding asteroids and their close approaches to Earth, visualizes statistics using **ScottPlot**, and automatically initializes a local **SQL Server / LocalDB** database at runtime upon first launch.

---

## 🏗️ Architecture & Features

- **Automated Database Setup:** Programmatically creates the `NasaDatabase` database and all required tables (`Asteroidi`, `OrbitClass`, `CloseApproach`, `OrbitalData`) using ADO.NET if they do not exist.
- **Relational Data Model:** 
  - 1-to-1 relationship with shared primary key for `Asteroidi` and `OrbitalData`.
  - 1-to-Many relationship between `Asteroidi` and `CloseApproach` (using composite PK: `AsteroidId`, `CloseApproachDate`).
  - Foreign key constraints with `ON DELETE CASCADE`.
- **Data Visualization:** Uses **ScottPlot.WinForms** for rendering interactive pie charts and statistics (e.g., hazardous vs. non-hazardous asteroids).
- **Asynchronous Processing:** Asynchronous API calls and database operations using `async/await` and `Task.WhenAll` to maintain a responsive UI.

---

## 🛠️ Prerequisites

- **.NET 6.0 / .NET 8.0 SDK** (or later)
- **SQL Server LocalDB** (included with Visual Studio Data Storage workload) or **SQL Server Express**
- A **NASA API Key** (Get yours at [api.nasa.gov](https://api.nasa.gov/))

---

## ⚙️ Configuration & Setup

Before running the application, you need to configure your **NASA API Key**:

1. Open the `App.config` file in the root of the project.
2. Locate the `<appSettings>` section and insert your API key:

```xml
<configuration>
  <appSettings>
    <add key="NasaApiKey" value="YOUR_NASA_API_KEY_HERE" />
  </appSettings>
</configuration>
