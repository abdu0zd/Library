using System.Runtime.CompilerServices;
using System.Threading;
using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksAsyncController : ControllerBase
    {
        //public List<Book> Books { get; set; }
        public AppDbContext Context { get; set; }

        public BooksAsyncController(AppDbContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(string name, string author)
        {


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
        [HttpPut]
        public async Task<ActionResult<Book>> UpdateBook(int id, string name, string author)
        {


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author))
            {
                return BadRequest("الرجاء ادخال اسم الكتاب او اسم الكاتب");
            }
            var book = await Context.books.FirstOrDefaultAsync(i => i.Id == id);
            if (book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            book.Name = name;
            book.Author = author;
            await Context.SaveChangesAsync();
            return book;

        }
        [HttpGet]
        public async Task<ActionResult<Book>> GetBook(int id)
        {

            var book = await Context.books.FirstOrDefaultAsync(i => i.Id == id);
            if (book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            return book;


        }
        [HttpGet("GetAllBooks")]
        public async Task<List<Book>> GetAllBook()
        {

            return await Context.books.ToListAsync();


        }

        [HttpGet("Search")]
        public async Task<List<Book>> searchBook(string value)
        {

            return await Context.books.Where(i => (i.Name + i.Author).Trim().ToLower().Contains(value.Trim().ToLower())).ToListAsync();


        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteBook(int id)
        {

            var book = await  Context.books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            await Task.Run(() => Context.books.Remove(book));
            await Context.SaveChangesAsync();
            return "تم الحذف بنجاح";

        }
    }
}
