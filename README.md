# Mobify — Mobile Phone Store

> An ASP.NET Core MVC application for browsing and managing a mobile phone catalog, built with a three-layer architecture, Entity Framework Core, and ASP.NET Core Identity.

---

## Overview

Mobify is currently in **Phase One**, focusing on authentication, product catalog management, administration, and the browsing experience. Future phases will introduce shopping cart, order management, checkout, and payment integration.

**Regular users** can register, log in, browse a product catalog, filter by brand/category/price, sort by price, search by name, and view a full product detail page.

**Admins** have a dedicated management panel to create, edit, and delete products, brands, and categories, and to set promotional discounts on individual products.

---

## Current Features

### Authentication & Authorization
- Registration (name, email, address, password) and login with a "Remember Me" option
- Cookie-based authentication via ASP.NET Core Identity
- Two roles seeded on startup: **Admin** and **User**
- Role-based redirect on login (Admin → product dashboard, User → home catalog)
- `ProductController` restricted to Admin role; `HomeController` requires any authenticated user
- Anti-forgery token protection on all POST forms

### Product Catalog (All Users)
- Product card grid showing name, brand, CPU, RAM, price, and offer badge when applicable
- Full product detail page with all specifications, multiple photos, advantages, and disadvantages

### Product Management (Admin Only)
- **Add** — name, description, CPU, screen, camera, battery, stock, price, color, storage, RAM, category, brand, multiple photo uploads, and dynamic advantage/disadvantage property lists
- **Edit** — update all fields; selectively delete existing photos and upload new ones; replace property lists
- **Delete** — confirmation step before permanent removal
- **Update Offer** — set a discounted price per product; the discount percentage is calculated automatically

### Brand & Category Management (Admin)
- Full CRUD for brands (with logo image upload and deletion)
- Full CRUD for categories
- Success/error feedback via TempData

### Search, Filtering & Pagination
- Filter by brand, category, and maximum price (home page only)
- Sort by price ascending or descending
- Case-insensitive name search
- AJAX partial-view refresh — the grid/table updates without a full page reload
- Paginated results with configurable page size (capped at 100)

### Image Upload
- Product images stored in `wwwroot/ProductPhotoes/`, brand logos in `wwwroot/BrandPhotoes/`
- GUID-prefixed filenames to prevent collisions
- Old files deleted from disk when a brand or product photo is removed

---

## Screenshots

> *Screenshots coming soon — run the project locally to preview the UI.*

| Page | Path |
|---|---|
| Home Catalog | `Screenshots/home-catalog.png` |
| Product Detail | `Screenshots/product-detail.png` |
| Admin Product List | `Screenshots/admin-product-list.png` |
| Add Product Form | `Screenshots/admin-add-product.png` |
| Update Offer | `Screenshots/admin-update-offer.png` |
| Login | `Screenshots/login.png` |

---

## Tech Stack

- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core 8 (Code-First, Fluent API)
- SQL Server
- ASP.NET Core Identity
- AutoMapper 16
- Bootstrap 5 + Bootstrap Icons
- jQuery + jQuery Unobtrusive Validation
- AJAX (partial-view refresh)

---

## Architecture

The solution uses a three-layer architecture across three separate C# projects:

```
Mobify.sln
├── Mobify.DAL   ← Data Access Layer (EF Core, Repositories)
├── Mobify.BLL   ← Business Logic Layer (Services, ViewModels, Helpers)
└── Mobify.PL    ← Presentation Layer (Controllers, Views, wwwroot)
```

**Dependency direction:** `PL → BLL → DAL`

Controllers depend only on service interfaces. Services depend only on repository interfaces. Neither the presentation layer nor the service layer directly references EF Core `DbContext`.

Key patterns used:
- **Repository Pattern** — `IBrandRepo`, `ICategoryRepo`, `IProductRepo` with concrete EF Core implementations
- **Service Layer** — business logic, filtering, pagination, and file management isolated from controllers
- **ViewModel Pattern** — dedicated ViewModels per operation (Add, Edit, Show, Query, Details)
- **Generic Response Wrapper** — `Response<T>` record carries result, error message, and error flag
- **Generic Pagination Wrapper** — `PagedResult<T>` with computed `TotalPages`
- **Data Seeding** — roles and admin account seeded at startup via `SeedingData`

---

## Project Structure

```
Mobify.DAL/
├── Entities/              # Product, Brand, Category, ProductOffer, ProductPhoto,
│                          #   ProductProperties, BrandPhoto, ApplicationUser
├── DataBase/
│   ├── DBContext/         # ApplicationDBContext (extends IdentityDbContext)
│   └── Configuration/    # Fluent API configs per entity
└── Repo/
    ├── Abstraction/       # IProductRepo, IBrandRepo, ICategoryRepo
    └── Implmentation/     # Concrete EF Core implementations

Mobify.BLL/
├── AutoMapper/            # DomainProfile (CategoryVM ↔ Category)
├── Helper/                # Files.cs — disk upload and delete
├── ModelVM/               # ViewModels grouped by feature area
├── SeedingData/           # Role and admin user seeding
└── Services/
    ├── Abstraction/       # IAccountServices, IBrandServices, ICategoryService,
    │                      #   IHomePageServices, IProductDetailsService, IProductServices
    └── Implmentation/     # Concrete service classes

Mobify.PL/
├── Controllers/           # Account, Brand, Category, Home, Product, ProductDetails
├── Views/                 # Razor views and partial views per controller
├── wwwroot/               # Static assets, uploaded images, Bootstrap, jQuery
└── Program.cs             # DI registration, Identity config, seeding, middleware
```

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server or SQL Server LocalDB (included with Visual Studio)
- Visual Studio 2022 or VS Code with the C# extension

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/Mobify.git
   cd Mobify
   ```

2. **Update the connection string** in `Mobify.PL/appsettings.json`
   ```json
   "ConnectionStrings": {
     "cs": "Server=.;Database=MobifyDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update --project Mobify.DAL --startup-project Mobify.PL
   ```

4. **Run the application**
   ```bash
   dotnet run --project Mobify.PL
   ```

5. **Default admin credentials** (seeded automatically):
   - Email: `admin@gmail.com`
   - Password: `Admin@123`

### Creating a new migration
```bash
dotnet ef migrations add <MigrationName> --project Mobify.DAL --startup-project Mobify.PL
```

---

## What I Learned

- ASP.NET Core MVC
- Entity Framework Core (Code-First, Fluent API, relationships)
- Repository Pattern
- Three-Layer Architecture
- ASP.NET Core Identity & Role-Based Authorization
- Service Layer design with generic response wrappers
- File upload and deletion from disk
- AJAX partial-view rendering
- Server-side pagination and filtering with `IQueryable<T>`
- AutoMapper
- Data seeding on application startup

---

## Future Roadmap

The following features are planned for upcoming phases:

- Shopping cart
- Wishlist
- Checkout flow
- Order management
- Payment integration
- Product reviews and ratings
- Email confirmation on registration
- User profile management
- Improved authorization coverage across all admin modules
- Stock status display on product cards
- Structured logging

---

## License

MIT License. See [LICENSE](LICENSE) for details.