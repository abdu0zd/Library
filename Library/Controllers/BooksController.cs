using System.Linq;
using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        //public List<Book> Books { get; set; }
        public AppDbContext Context { get; set; } =new AppDbContext();
        
        [HttpPost]
        public ActionResult<Book> AddBook(string name,string author)
        {
            

            Context.Database.EnsureCreated();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author))
            {
                return BadRequest("الرجاء ادخال اسم الكتاب او اسم الكاتب");
            }
            var book = new Book { Name = name, Author = author };
            Context.books.Add(book);
            Context.SaveChanges();
            return book;



        }
        [HttpPut]
        public ActionResult<Book> UpdateBook(int id,string name, string author)
        {
            

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author))
            {
                return BadRequest("الرجاء ادخال اسم الكتاب او اسم الكاتب");
            }
            var book = Context.books.FirstOrDefault(i => i.Id == id);
            if (book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            book.Name = name;
            book.Author = author;
            Context.SaveChanges();
            return book;

        }
        [HttpGet]
        public ActionResult<Book> GetBook(int id)
        {

          var book =  Context.books.FirstOrDefault(i => i.Id == id);
          if(book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }  
          return book;


        }
        [HttpGet("GetAllBooks")]
         public List<Book> GetAllBook()
        {

            return Context.books.ToList();
            
            
        }

        [HttpGet("Search")]
        public List<Book> searchBook(string value)
        {

         return  Context.books.Where(i=>(i.Name + i.Author).Trim().ToLower().Contains(value.Trim().ToLower())).ToList();
            

        }

        [HttpDelete]
        public ActionResult<string> DeleteBook(int id)
        {
            
            var book = Context.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            Context.books.Remove(book);
            Context.SaveChanges();
            return "تم الحذف بنجاح";

        }




    }
}
