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

## Async Method 
Use the async modifier to specify that a method, lambda expression, or anonymous method is asynchronous. If you use this modifier on a method or expression, it's referred to as an async method. 
The following example defines an async method named BooksAsyncController:
```bash
[HttpPost]
public async Task<ActionResult<Book>> AddBook(string name, string author){

}
```
If you're new to asynchronous programming or do not understand how an async method uses the [await operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await) to do potentially long-running work without blocking the caller's thread,
[read the introduction in Asynchronous programming with async and await](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/).
```bash
[HttpPost]
public async Task<ActionResult<Book>> AddBook(string name, string author){

 await Context.Database.EnsureCreatedAsync();
 if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author))
 {
     return BadRequest("الرجاء ادخال اسم الكتاب او اسم الكاتب");
 }
 var book = new Book { Name = name, Author = author };
 await Context.books.AddAsync(book);
 await Context.SaveChangesAsync();
 return book;
}
```
An async method runs synchronously until it reaches its first await expression, at which point the method is suspended until the awaited task is complete.
In the meantime, control returns to the caller of the method, as the example in the next section shows.

## Async Controllers 
You will find three Async Controllers.
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/BooksAsyncController.cs) : BooksAsyncController.
- Using Async Method 
  it contain multi action method Like add book, delete book , edit book etc..
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/BorrowAsyncController.cs) : BorrowAsyncController.
- Using Async Method
  it contain multi action method Like add date of borrow, search the day of borrow , return a book etc..
- [Library/Controllers](https://github.com/abdu0zd/Library/blob/master/Library/Controllers/UsersAsyncController.cs) : UsersAsyncController.
- Using Async Method
  it contain multi action method Like add User, delete User , edit User etc..

## DataBase Class
- [Library/DataAccess](https://github.com/abdu0zd/Library/blob/master/Library/DataAccess/AppDbContext.cs) : AppDbContext.
  it contain two action method OnConfiguring  it connect to the databse and OnModelCreating multi relations when it will create the database.
- DataBase Diagram
  ![DatabaseLibrary](https://github.com/abdu0zd/Library/assets/13774950/103b702d-6516-4b7b-b6c4-3742320d81da) 

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
