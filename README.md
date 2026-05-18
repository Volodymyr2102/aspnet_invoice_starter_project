# 📘 ASP.NET Invoice Starter Project  
*(English below / Čeština níže)*  

---

## 🇬🇧 English version

### 🧾 Project Overview
This is a **training project** created as part of the **C# .NET Developer** course on [ITnetwork.cz](https://www.itnetwork.cz).  
It demonstrates the fundamentals of **ASP.NET Core MVC**, **Entity Framework**, **MS SQL**, and **MongoDB** through an example of an **invoice management system**.

---

### ⚙️ Features
- Manage a list of clients (Persons)  
- Create, edit and delete invoices  
- Filter invoices by year, product or price  
- View statistics: total sums, VAT, and turnover by supplier  
- Uses **MS SQL** for relational data  
- Uses **MongoDB** for logs and analytics  
- Responsive **Bootstrap 5** interface with Razor Pages  

---

### 🧱 Technologies Used

| Component | Technology |
|------------|-------------|
| Backend | ASP.NET Core MVC (.NET 8) |
| Frontend | HTML, CSS, Bootstrap, JavaScript |
| ORM | Entity Framework Core |
| Databases | MS SQL, MongoDB |
| IDE | Visual Studio 2022 |
| OS | Windows 10 / 11 |
---

### 🚀 How to Run

1. Clone the repository or download ZIP:
   ```bash
   git clone https://github.com/your_username/aspnet_invoice_starter_project.git
   ```

2. Open the project in **Visual Studio**

3. Make sure you have:
   - **.NET SDK 8.0**
   - **MS SQL LocalDB**
   - **MongoDB** (local or cloud – e.g. MongoDB Atlas)

4. Configure connection strings in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoiceDB;Trusted_Connection=True;",
       "MongoDB": "mongodb://localhost:27017"
     }
   }
   ```

5. Apply migrations and run:
   ```bash
   dotnet ef database update
   dotnet run
   ```

6. Open in browser:
   ```
   https://localhost:5001
   ```

---

### 👤 Author
**Volodymyr Dukhno**  
Student of *C# .NET Developer* course at [ITnetwork.cz](https://www.itnetwork.cz)

---

## 🇨🇿 Česká verze

### 🧾 Přehled projektu
Tento **výukový projekt** vznikl v rámci kurzu **C# .NET Developer** na [ITnetwork.cz](https://www.itnetwork.cz).  
Ukazuje principy práce s **ASP.NET Core MVC**, **Entity Frameworkem**, **MS SQL** a **MongoDB** na příkladu systému pro správu faktur.

---

### ⚙️ Funkce
- Správa seznamu osob (klientů)  
- Vytváření, úprava a mazání faktur  
- Filtrování podle roku, produktu nebo ceny  
- Statistiky: součet částek, DPH, obraty podle dodavatelů  
- Použití **MS SQL** pro relační data  
- Použití **MongoDB** pro logování a analytiku  
- Responzivní rozhraní pomocí **Bootstrap 5** a Razor Pages  

---

### 🧱 Použité technologie

| Komponenta | Technologie |
|-------------|-------------|
| Backend | ASP.NET Core MVC (.NET 8) |
| Frontend | HTML, CSS, Bootstrap, JavaScript |
| ORM | Entity Framework Core |
| Databáze | MS SQL, MongoDB |
| IDE | Visual Studio 2022 |
| OS | Windows 10 / 11 |

---

### 🚀 Jak projekt spustit

1. Naklonujte repozitář nebo stáhněte ZIP:
   ```bash
   git clone https://github.com/your_username/aspnet_invoice_starter_project.git
   ```

2. Otevřete projekt ve **Visual Studio**

3. Zkontrolujte, že máte:
   - **.NET SDK 8.0**
   - **MS SQL LocalDB**
   - **MongoDB** (lokálně nebo přes MongoDB Atlas)

4. Nastavte připojovací řetězce v `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoiceDB;Trusted_Connection=True;",
       "MongoDB": "mongodb://localhost:27017"
     }
   }
   ```

5. Proveďte migrace a spusťte:
   ```bash
   dotnet ef database update
   dotnet run
   ```

6. Otevřete v prohlížeči:
   ```
   https://localhost:5001
   ```

---

### 👤 Autor
**Volodymyr Dukhno**  
Student kurzu *C# .NET Developer* na [ITnetwork.cz](https://www.itnetwork.cz)
