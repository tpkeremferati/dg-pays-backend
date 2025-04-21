
# Dg Pays Case B1 Backend

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

To run the project, first clone the repository:

```bash
git clone https://github.com/your-repository-url/project-name.git
cd project-name
```

Open the solution in **Visual Studio** by selecting the `.sln` file. Visual Studio will automatically restore the necessary NuGet packages. If not, you can manually restore them by running:

```bash
dotnet restore
```

Next, update the database connection string in the `appsettings.json` file with your SQL Server details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YourDatabaseName;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;MultipleActiveResultSets=true"
}
```

If you’re using **Entity Framework Core**, apply the database migrations by opening **Package Manager Console** in Visual Studio and running:

```bash
Update-Database
```

Finally, build and run the project by pressing `Ctrl + F5` to run without debugging, or `F5` to run with debugging. The project will launch in your browser at `https://localhost:7189` depending on your configuration.

## Candidate
Name: Kerem Ferati

Email: kerem.ferati@technoperia.com