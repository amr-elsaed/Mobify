# Mobify — Mobile Phone Store (ASP.NET MVC)

A mobile phone e-commerce catalog application built with ASP.NET Core MVC (.NET 8). The project covers product management, category and brand administration, user authentication with role-based access, and a public-facing storefront with filtering and pagination. Built as a learning project to practice layered architecture with Entity Framework Core and ASP.NET Core Identity.

---

## Overview

Mobify is a web application where:

- **Regular users** can register, log in, browse a product catalog, filter by brand/category/price, sort by price, search by name, and view a full product detail page.
- **Admins** have a dedicated management panel to create, edit, and delete products, brands, and categories, as well as set or update promotional offers (discounted prices) on individual products.

The application is not a full shopping platform — there is no cart, checkout, or payment flow. It is a catalog + content management system for mobile phones.

---

## Features

### Authentication
- User registration (name, email, address, password)
- Login with email and password, with a "Remember Me" option
- Logout
- Role-based redirect on login: Admin users go to the product management dashboard; regular users go to the home catalog
- Cookie-based authentication via ASP.NET Core Identity
- Anti-forgery token protection on all POST forms

### User Roles
- Two roles are seeded automatically on application startup: **Admin** and **User**
- A default admin account is seeded: `admin@gmail.com` / `Admin@123`
- The `ProductController` is fully protected with `[Authorize(Roles = "Admin")]`
- The `HomeController` requires `[Authorize]` (any authenticated user)

### Product Catalog (Public)
- Grid layout of product cards on the home page, each showing name, brand, CPU, RAM, original price, and offer price/percentage when applicable
- Clicking a product card opens a full product detail page showing all specifications, multiple photos, advantages, and disadvantages
- Products can have multiple photos uploaded and stored on disk

### Product Management (Admin Only)
- **Add Product**: Full form with name, description, CPU, screen, camera, battery, stock quantity, price, color, storage, RAM, category, brand, multiple photo uploads, and a dynamic list of advantage and disadvantage properties
- **Edit Product**: Update all fields; selectively delete existing photos and add new ones; replace advantage/disadvantage property lists
- **Delete Product**: Confirmation and permanent delete
- **Update Offer**: Set or update a promotional discount on a product, providing the discounted price; the percentage is calculated automatically relative to the original price
- Paginated product table in the admin dashboard with search, category filter, brand filter, and price sorting (ascending/descending)

### Brand Management
- List all brands with their photos
- Add a brand (name + logo image upload)
- Edit a brand (update name; optionally replace the photo, which deletes the old file from disk)
- Delete a brand (confirmation page; deletes the photo from disk as well)

### Category Management
- List all categories
- Create a category (name only)
- Edit a category name
- Delete a category with a confirmation step
- Success and error feedback via `TempData`

### Search & Filtering
- **Home page**: filter by brand, category, and maximum price; sort by price ascending/descending; search by product name (case-insensitive, contains)
- **Admin product list**: filter by brand and category; sort by price; search by name
- Both use AJAX partial-view refresh (`X-Requested-With: XMLHttpRequest`) so the page does not fully reload when filters change
- Pagination with configurable page size (capped at 100 for admin, 100 for public)

### Image Upload
- Product images are uploaded to `wwwroot/ProductPhotoes/`
- Brand images are uploaded to `wwwroot/BrandPhotoes/`
- Each file is stored with a GUID prefix to avoid name collisions
- The `Files` helper class (in `Mobify.BLL`) handles saving and deleting files from disk
- Files are deleted from disk when a brand is deleted or when a product photo is removed during editing

### Validation
- `[DataType]` annotations on ViewModels (email, password)
- `ModelState.IsValid` checks in all POST actions before executing business logic
- Client-side validation via jQuery Unobtrusive Validation (bundled from `lib/jquery-validation` and `lib/jquery-validation-unobtrusive`)
- Error messages surfaced via `ModelState.AddModelError`

### Architecture & Design Patterns
- **Three-layer architecture**: DAL (Data Access), BLL (Business Logic), PL (Presentation)
- **Repository pattern**: `IBrandRepo`, `ICategoryRepo`, `IProductRepo` with concrete implementations; each repository receives `ApplicationDBContext` via constructor injection
- **Service layer**: Service interfaces in `BLL/Services/Abstraction/`, implementations in `BLL/Services/Implmentation/`; controllers depend only on service interfaces, never on repositories directly
- **ViewModel pattern**: Separate ViewModels per operation (Add, Edit, Show, Query, Details) kept in the BLL project
- **Generic response wrapper**: `Response<T>` (a C# record) wraps every service return value with the result, an optional error message, and an error flag
- **Generic pagination wrapper**: `PagedResult<T>` carries items, page, page size, total items, and a computed `TotalPages`
- **AutoMapper**: Registered with a `DomainProfile`; currently maps `CategoryVM ↔ Category`
- **Dependency Injection**: All repositories and services are registered with `AddScoped` in `Program.cs`
- **Partial views**: `_ProductsGrid.cshtml` (home page AJAX grid) and `_ProductsTablePartial.cshtml` (admin AJAX table)
- **Data seeding**: Roles (`Admin`, `User`) and a default admin user are seeded automatically at startup via `SeedingData`
- **EF Core Fluent API**: Each entity has a dedicated configuration class (e.g., `ProductConfiguration`, `BrandConfiguration`) inside `DAL/DataBase/Configuration/`

### UI
- Bootstrap 5 for layout and components
- Bootstrap Icons (`bi-*`) used throughout
- Custom CSS (`wwwroot/css/styles.css` and `wwwroot/css/site.css`)
- Responsive navigation with a mobile sidebar toggle and a mega-menu structure
- Sticky header with scroll behaviour handled by JavaScript
- Back-to-top button
- Footer with social links, contact info, and payment icons (all static/decorative)

---

## Technologies

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 8) |
| Language | C# 12 |
| ORM | Entity Framework Core 8 |
| Database | SQL Server (LocalDB or full instance) |
| Authentication | ASP.NET Core Identity |
| Object Mapping | AutoMapper 16 |
| Frontend | Bootstrap 5, Bootstrap Icons, jQuery |
| Validation | jQuery Validation + jQuery Unobtrusive Validation |
| IDE | Visual Studio |

### NuGet Packages

**Mobify.DAL**
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` 8.0.20
- `Microsoft.EntityFrameworkCore` 8.0.24
- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.24
- `Microsoft.EntityFrameworkCore.Tools` 8.0.24

**Mobify.BLL**
- `AutoMapper` 16.0.0
- `Microsoft.AspNetCore.Http.Features` 5.0.17

**Mobify.PL**
- `Microsoft.EntityFrameworkCore.Tools` 8.0.24

---

## Architecture

The solution is split into three C# projects that form a classic three-layer architecture:

```
Mobify.sln
├── Mobify.DAL    ← Data Access Layer
├── Mobify.BLL    ← Business Logic Layer (depends on DAL)
└── Mobify.PL     ← Presentation Layer / ASP.NET MVC app (depends on BLL)
```

**Dependency direction**: `PL → BLL → DAL`. The presentation layer never directly touches EF Core or the database.

**Request flow (example — load home page)**:

```
Browser → HomeController.Index()
        → IHomePageServices.GetProductsCard(vm)
        → IProductRepo.Query()   [IQueryable<Product>]
        → EF Core → SQL Server
        → ProductCardVM list returned
        → View rendered
```

The Repository pattern adds an abstraction over EF Core. Services contain the business logic (filtering, pagination, offer calculation, file management) and map between domain entities and ViewModels. Controllers only orchestrate requests; they do not contain business logic.

---

## Project Structure

```
Mobify.DAL/
├── Entities/                  # Domain models (Product, Brand, Category, etc.)
├── DataBase/
│   ├── DBContext/             # ApplicationDBContext (extends IdentityDbContext)
│   └── Configuration/        # EF Core Fluent API configs per entity
├── Repo/
│   ├── Abstraction/           # IProductRepo, IBrandRepo, ICategoryRepo
│   └── Implmentation/         # Concrete EF Core repository implementations
├── Enums/                     # (reserved, currently empty)
└── Global/global.cs           # Global usings for the DAL project

Mobify.BLL/
├── AutoMapper/
│   └── DomainProfile.cs       # AutoMapper profile (CategoryVM ↔ Category)
├── Helper/
│   └── Files.cs               # Static helper for disk file upload and delete
├── ModelVM/
│   ├── AccountVM/             # UserLogInVM, UserRegisterVM, RoleVM
│   ├── BrandVM/               # AddBrandVM, EditBrandVM, ShowBrandVM
│   ├── CategoryVM/            # GategoryVM (CategoryVM)
│   ├── HomePageVM/            # ProductCardVM, ProductCardQueryVM,
│   │                          #   BrandAndCountOfProduct, CategoryAndCountOfProduct,
│   │                          #   AllHomePageComponent
│   ├── ProductVM/             # AddProductVM, EditProductVM, ShowProductVM,
│   │                          #   ProductQueryVM, ProductOfferVM, ProductVM
│   ├── ProductDetailsVM/      # ProductDetailsVM
│   └── ResponseResult/        # Response<T> record, PagedResult<T>
├── SeedingData/
│   └── SeedingData.cs         # Seeds Admin/User roles and default admin account
├── Services/
│   ├── Abstraction/           # IAccountServices, IBrandServices, ICategoryService,
│   │                          #   IHomePageServices, IProductDetailsService, IProductServices
│   └── Implmentation/         # Concrete service classes
└── Global/global.cs           # Global usings for the BLL project

Mobify.PL/
├── Controllers/               # AccountController, BrandController, CategoryController,
│                              #   HomeController, ProductController, ProductDetailsController
├── Views/
│   ├── Account/               # Login.cshtml, Register.cshtml
│   ├── Brand/                 # Index, Add, Update, Delete
│   ├── Category/              # Index, Create, Edit, Delete
│   ├── Home/                  # Index.cshtml (storefront), _ProductsGrid.cshtml (partial)
│   ├── Product/               # Index, Add, Edit, UpdateOffer, _ProductsTablePartial (partial)
│   ├── ProductDetails/        # GetProductDetails.cshtml
│   └── Shared/                # _Layout.cshtml, Error.cshtml, _ValidationScriptsPartial
├── Models/                    # ErrorViewModel (scaffolded)
├── wwwroot/
│   ├── BrandPhotoes/          # Uploaded brand logo files
│   ├── ProductPhotoes/        # Uploaded product image files
│   ├── css/                   # site.css, styles.css
│   ├── js/                    # site.js
│   └── lib/                   # Bootstrap 5, jQuery, jQuery Validation
├── Program.cs                 # App bootstrap, DI registration, seeding, middleware
└── appsettings.json           # Connection string
```

---

## Screenshots

> Screenshots are not included in this version of the repository. Below are the paths where you can add them once captured.

```
Screenshots/home-catalog.png
Screenshots/product-detail.png
Screenshots/admin-product-list.png
Screenshots/admin-add-product.png
Screenshots/admin-update-offer.png
Screenshots/brand-management.png
Screenshots/login.png
Screenshots/register.png
```

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server LocalDB, which ships with Visual Studio)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or VS Code with the C# extension

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/Mobify.git
   cd Mobify
   ```

2. **Configure the connection string**

   Open `Mobify.PL/appsettings.json` and update the connection string if needed:
   ```json
   "ConnectionStrings": {
     "cs": "Server=.;Database=MobifyDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   Replace `Server=.` with your SQL Server instance name if it differs.

3. **Apply database migrations**

   In the Visual Studio Package Manager Console (or a terminal), set `Mobify.DAL` as the default project and `Mobify.PL` as the startup project, then run:
   ```bash
   Update-Database
   ```
   Or with the .NET CLI from the solution root:
   ```bash
   dotnet ef database update --project Mobify.DAL --startup-project Mobify.PL
   ```

4. **Run the application**
   ```bash
   dotnet run --project Mobify.PL
   ```
   Or press **F5** in Visual Studio.

5. **Default admin credentials** (seeded automatically on first run):
   - Email: `admin@gmail.com`
   - Password: `Admin@123`

---

## Database Setup

The project uses EF Core code-first migrations. Migrations are stored in `Mobify.DAL/Migrations/`.

To create a new migration after changing an entity:
```bash
dotnet ef migrations add <MigrationName> --project Mobify.DAL --startup-project Mobify.PL
```

To apply pending migrations:
```bash
dotnet ef database update --project Mobify.DAL --startup-project Mobify.PL
```

> **Note**: The `appsettings.json` file currently contains a plain-text connection string committed to the repository. Before making the repository public, move the connection string to `appsettings.Development.json` (which is already in `.gitignore`) or use user secrets (`dotnet user-secrets`).

---

## Future Improvements

The following features are not implemented and are noted here as possible next steps:

- **Shopping cart** — allow users to add products to a session or database-backed cart
- **Wishlist** — save favourite products per user account
- **Checkout flow** — collect shipping address and order summary
- **Order management** — store and track orders in the database
- **Payment integration** — connect a payment gateway (e.g. Stripe or PayPal)
- **Product reviews and ratings** — let authenticated users leave feedback
- **Email confirmation on registration** — verify user email addresses via ASP.NET Core Identity's email sender
- **User profile page** — allow users to update their name, address, and password
- **Stock tracking** — show "Out of Stock" on product cards when `StockQuantity` reaches zero
- **Admin authorization on Brand/Category** — `BrandController` and `CategoryController` currently have no `[Authorize]` attribute; restricting them to Admin would close a gap
- **Global error handling** — a structured error page or middleware instead of re-throwing raw exceptions in services
- **Logging** — add structured logging (e.g. Serilog) for service and repository operations
- **Unit tests** — test services and repositories in isolation using mocking

---

## Learning Objectives

By building this project I practised:

- Structuring a multi-project ASP.NET Core solution with clear layer separation
- Using Entity Framework Core code-first with Fluent API configuration for relationships
- Implementing the Repository pattern to decouple the data access logic from business logic
- Using ASP.NET Core Identity for authentication, role management, and cookie-based sign-in
- Designing a service layer that wraps results in a generic `Response<T>` type to carry error information without throwing exceptions for expected cases
- Handling file uploads (save with a GUID prefix) and file deletion on disk from a service class
- Building paginated, filtered, and sorted queries using `IQueryable<T>` without loading all data into memory
- Using AJAX partial-view refreshes to update a product grid/table without full page reloads
- Managing multiple photos per product during create and edit operations (add new, mark existing for deletion)
- Using AutoMapper for ViewModel-to-entity mapping
- Seeding database roles and an admin account on application startup

---

## Known Issues / Things to Note

- The `BrandController` and `CategoryController` are not protected with `[Authorize]`. Any user who knows the URL can access those management pages.
- Password requirements are relaxed in `Program.cs` (minimum length 4, uppercase/lowercase/non-alphanumeric not required). This is acceptable for a learning project but should not be used in a real application.
- The seeded admin password (`Admin@123`) is hard-coded in `SeedingData.cs`. It should be moved to a configuration value or environment variable before any public deployment.
- The `Files.UploadFile` method catches exceptions and returns the exception message as the file URL, which could silently fail. This is worth improving with proper exception propagation.
- The `AutoMapper` profile only maps `CategoryVM ↔ Category`; other entity-to-ViewModel mappings are done manually in the service classes.
- There are several typos in identifier names throughout the project (e.g., `Battary` instead of `Battery`, `Discription` instead of `Description`, `Precentage` instead of `Percentage`, `Implmentation` instead of `Implementation`). These are in the database schema, so fixing them requires a migration.

---

## License

MIT License. See [LICENSE](LICENSE) for details.

---

## Repository Suggestions

### Improve the GitHub Repository
- Add actual **screenshots** or a short **screen recording (GIF)** to the README — it significantly increases engagement.
- Add a `LICENSE` file (MIT or Apache 2.0).
- Add a `.editorconfig` file to enforce consistent code style.
- Consider adding a GitHub Actions workflow to build the project on every push, confirming it compiles cleanly.
- Move the connection string out of `appsettings.json` before making the repository public.

### Improve the Project Before Adding it to Your CV
1. **Fix the missing `[Authorize]` on Brand and Category controllers** — this is an obvious security gap a reviewer will spot immediately.
2. **Rename typo identifiers** (`Battary`, `Discription`, `Precentage`) — these suggest inattention to detail.
3. **Add at least a few unit tests** — even 3–5 tests on a service method demonstrate you understand testing fundamentals.
4. **Add proper exception handling** — instead of `throw new Exception(ex.Message)` (which loses the stack trace), either re-throw the original exception or use a global exception handler middleware.
5. **Move the seeded admin password to configuration** — shows awareness of security basics.
6. **Implement one more meaningful feature**, such as a product reviews section or a working cart page — it gives you more to talk about.

### Make the Repository More Attractive to Recruiters
- **Write a concise "What I learned" section** in the README (already included above — keep it honest and specific).
- **Add a live demo link** if you host it on Azure App Service, Railway, or similar — even a free-tier deployment makes a big difference.
- **Add a database diagram** (an ERD image) so a reviewer can understand the data model at a glance without reading code.
- **Tag a release** (v1.0.0) on GitHub so the repository looks maintained.
- **Pin the repository** on your GitHub profile.
- **Clean up commit history** — meaningful commit messages (e.g., `"Add paginated product filtering with AJAX partial refresh"`) are more impressive than `"fix"` or `"update"`.