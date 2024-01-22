# Library
Web API Project using .NET web Core api

## Technologies
- .NET Core 8.0.100
- Entity Framework 8.1
- Migration
- Swagger
- SQL Server

## Getting Started

You can [browse this repository] or you can [clone this repository](https://help.github.com/articles/cloning-a-repository/) and browse the files:

```bash
git clone https://github.com/abdu0zd/Library.git
```

Then open the Library.sln so you can test or try the project.

## Controllers
You will find three Controllers.
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/BooksController.cs) : BooksController.
  it contain multi action method Like add book, delete book , edit book etc..
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/BorrowController.cs) : BorrowController.
  it contain multi action method Like add date of borrow, search the day of borrow , return a book etc..
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/UsersController.cs) : UsersController.
  it contain multi action method Like add User, delete User , edit User etc..  
## DataBase Class
- [Library/DataAccess](https://github.com/abdu0zd/Library/blob/master/Library/DataAccess/AppDbContext.cs) : AppDbContext.
  it contain two action method OnConfiguring  it connect to the databse and OnModelCreating multi relations when it will create the database.

## Usage Migration
If you have a problem with the database you can easily
Open Visual studio --> View --> Other windows --> Package Manager Console
![Info](https://github.com/abdu0zd/Library/assets/13774950/e608bee8-769a-4168-94af-4956cbb49787)
![info2](https://github.com/abdu0zd/Library/assets/13774950/5a04f20e-4bd9-456c-9008-7c94d9256271)

 ```bash
Add-Migration Library
```
After we add the Library we need to update the database using this command
 ```bash
Update-Database
```
