# BiblioFinder radar de libros

BiblioFinder es una aplicación web desarrollada con ASP.NET Core 8 bajo una Arquitectura Limpia. La aplicación permite a los usuarios buscar libros por el nombre de un autor utilizando la API pública de Open Library, visualizar los resultados y consultar un historial de búsquedas persistido en una base de datos SQL Server.

---

## 🚀 Tecnologías Utilizadas

- **.NET 8** y **ASP.NET Core MVC**
- **Entity Framework Core 8**
- **SQL Server**
- **Arquitectura Limpia** (Clean Architecture)
- **Patrón Repositorio** y **Casos de Uso (Use Cases)**
- **HTML5**, **CSS3**, **JavaScript**
- **Bootstrap 5**
- **DataTables.net** para tablas interactivas
- **AutoMapper**

---

## ✨ Características Principales

- [x] Búsqueda de libros por autor a través de la API de Open Library.
- [x] Visualización de resultados en una tabla clara.
- [x] Persistencia de cada búsqueda en una base de datos SQL Server.
- [x] Página de historial de búsquedas con paginación, búsqueda y ordenamiento del lado del cliente.
- [x] **(Extra)** Lógica para evitar guardar búsquedas duplicadas realizadas en menos de un minuto.
- [x] **(Extra)** Uso de Entity Framework Core para el acceso a datos.
- [x] **(Extra)** Uso de **Stored Procedures** para todas las operaciones de la base de datos, incluyendo inserciones masivas optimizadas con **Table-Valued Parameters (TVPs)**.
- [x] **(Extra)** Interfaz de usuario estilizada con Bootstrap 5.
- [x] **(Extra)** Validaciones en el frontend con JavaScript para una mejor experiencia de usuario.

---

## 📋 Prerrequisitos

Para ejecutar este proyecto, necesitarás tener instalado:

- .NET 8 SDK
- SQL Server (2019 o superior, incluyendo la edición Express)
- Una herramienta para gestionar la base de datos, como SQL Server Management Studio (SSMS) o Azure Data Studio.

---

## ⚙️ Instalación y Ejecución

Sigue estos pasos para poner en marcha el proyecto en tu entorno local:

1.  **Clona el repositorio:**
    - Abre una terminal y ejecuta:
    ```bash
    git clone https://github.com/ROGUEANOVI/prueba-tecnica-fullstack-semisenior-BiblioFinder.git
    ```
2.  **Abre el proyecto en tu IDE de preferencia (Visual Studio o VSCode):**

    - Abre la Solucion del proyecto que acabas de clonar en Visual Studio. O desde la terminal ingresa a la raiz del proyecto y ejecuta (Esto abrirá VSCoode con tu proyecto):

    ```bash
    code .
    ```

3.  **Configura la Cadena de Conexión:**

    - Abre el archivo `BiblioFinder.Web/appsettings.json o BiblioFinder.Web/appsettings.Development.json`.
    - Agrega la sección `ConnectionStrings` con las credenciales de tu instancia local de SQL Server. Ejemplo:
      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Server=TU_SERVIDOR;Database=BiblioFinderDB;Trusted_Connection=True;TrustServerCertificate=True;"
      }
      ```

4.  **Crea la Base de Datos:**
    Tienes dos opciones para crear la base de datos:

    **Opción A: Usando las Migraciones de EF Core**

    - Abre una terminal en la raíz de la solución.
    - Ejecuta el siguiente comando para aplicar las migraciones:

      ```bash
      dotnet ef database update --project BiblioFinder.Infrastructure --startup-project BiblioFinder.Web
      ```

      O desde el Package Manager Console de Visual Studio selecionando como proyecto por defecto BiblioFinder.Infrastructure:

      ```bash
      update-database
      ```

    - **Opción B: Usando el Script SQL**
      1.  Abre el archivo `script_bibliofinder_db.sql` que se encuentra en la raíz del proyecto.
      2.  Ejecuta el script completo en tu gestor de base de datos (SSMS). Esto creará la base de datos, la tabla, indice, el tipo y todos los Stored Procedures.
    -

5.  **Ejecuta la Aplicación:**
    - Navega a la carpeta del proyecto web: `cd BiblioFinder.Web`
    - Ejecuta el proyecto con el siguiente comando:
      ```bash
      dotnet run
      ```
    - Abre tu navegador y ve a la dirección que te indique la terminal (ej. `https://localhost:7162`).

---

## 🧠 Decisiones de Diseño

- **Arquitectura Limpia:** Elegí esta arquitectura para separar claramente las responsabilidades. El **Dominio** es agnóstico a la tecnología, la **Aplicación** contiene la lógica de negocio y los casos de uso, la **Infraestructura** se encarga de los detalles externos (base de datos, APIs), y la **Web** es solo la capa de presentación. Esto resulta en un código más mantenible, escalable y fácil de probar.

- **Stored Procedures y TVPs:** Aunque EF Core es muy potente, decidí usar Stored Procedures para todas las operaciones de base de datos para cumplir con los puntos extra y demostrar un control más profundo. Específicamente, para la inserción masiva de historiales, implementé un **Table-Valued Parameter (TVP)**, que es la solución más óptima para enviar múltiples registros a SQL Server en una sola llamada, minimizando la latencia de red.

- **Paginación en el Cliente (DataTables.net):** Para la tabla de historial, elegí la paginación del lado del cliente porque ofrece una experiencia de usuario muy rica (búsqueda, ordenamiento, paginación) con un costo de implementación muy bajo, lo cual es ideal para el alcance de esta prueba. En una aplicación con millones de registros, el siguiente paso sería implementar la paginación del lado del servidor.

---

## 🔮 Mejoras a Futuro

- **Implementar Caching:** Añadir una capa de caché (ej. con Redis) para las respuestas de la API de Open Library para reducir la latencia y el número de llamadas a la API externa.
- **Logging Robusto:** Integrar un sistema de logging estructurado como Serilog para registrar errores y eventos importantes de la aplicación.
- **Paginación en el Servidor:** Refactorizar la tabla de historial para usar la paginación del lado del servidor de DataTables, asegurando que la aplicación escale a un volumen de datos mucho mayor.
- **Pruebas Unitarias y de Integración:** Añadir proyectos de pruebas para asegurar la calidad y fiabilidad del código en las capas de Dominio y Aplicación.
