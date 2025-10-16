# StellarIncidents

API RESTful para la gestión de incidentes técnicos dentro de una organización.  
Incluye control de categorías, usuarios, comentarios, validaciones con **FluentValidation**, documentación con **Swagger**, y pruebas unitarias con **xUnit + Moq**.

---

## 🚀 Tecnologías principales
- .NET 8
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- Swagger + Swashbuckle
- Serilog
- xUnit + Moq

---

## ⚙️ Configuración del entorno

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/leo7962/stellar-incidents.git
   cd StellarIncidents
   ```

2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

3. Aplicar las migraciones:
   ```bash
   dotnet ef database update
   ```

4. Ejecutar la API:
   ```bash
   dotnet run
   ```

5. Abrir Swagger en el navegador:
   ```
   https://localhost:5001
   ```

---

## 🧪 Pruebas unitarias

Para ejecutar los tests:

```bash
dotnet test
```

Ubicación de los tests:  
📁 `/UnitTests/IncidentsControllerTests.cs`  
📁 `/UnitTests/IncidentRepositoryTests.cs`

---

## 📘 Endpoints principales

| Método | Ruta | Descripción |
|--------|-------|-------------|
| GET | `/api/incidents` | Lista todos los incidentes |
| GET | `/api/incidents/{id}` | Obtiene un incidente por ID |
| POST | `/api/incidents` | Crea un nuevo incidente |
| PUT | `/api/incidents/{id}` | Actualiza un incidente existente |
| DELETE | `/api/incidents/{id}` | Elimina un incidente |
| POST | `/api/incidents/{id}/comments` | Agrega un comentario a un incidente |

---

## 🧾 Logs
Los registros de ejecución se almacenan automáticamente en:
```
/logs/log-YYYYMMDD.txt
```

---

## 👨‍💻 Autor

**Leonardo Hernández**  
Desarrollador .NET | FullStack | SQL Server  
📧 contacto: *ingenieroleonardo@outlook.com*  
