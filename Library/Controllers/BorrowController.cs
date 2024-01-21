using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        public AppDbContext Context { get; set; } = new AppDbContext();
        [HttpPost]
        public ActionResult<Borrow> Borrow(int userid, int bookid, DateTime date)
        {


            Context.Database.EnsureCreated();
            if (userid == null || bookid == null || date == null)
            {
                return BadRequest("الرجاء ادخال اسم الكتاب او اسم الكاتب");
            }
            if (date <= DateTime.Now)
            {
                return BadRequest("خطأ في تاريخ الادخال");
            }
            if (Context.books.Any(i => i.Id == bookid) == false)
            {
                return BadRequest("هذا الكتاب غير موجود");
            }
            if (Context.users.Any(i => i.Id == userid) == false)
            {
                return BadRequest("هذا المستخدم غير موجود");
            }
            var borrow = new Borrow(userid, bookid, date);
            Context.borrow.Add(borrow);
            Context.SaveChanges();
            return borrow;
        }
        [HttpGet("SearchBorrow")]
        public List<Borrow> searchBorrow(int UserId)
        {

            return Context.borrow.Where(i => i.UserId == UserId).ToList();

        }

        [HttpDelete]
        public ActionResult<string> ReturnBook(int userId, int bookId)
        {

            var borrow = Context.borrow.FirstOrDefault(x => x.UserId == userId && x.BookId == bookId);
            if (borrow == null)
            {
                return BadRequest(" هذا المستخدم لم يستأجر هذا الكتاب");
            }
            Context.borrow.Remove(borrow);
            Context.SaveChanges();
            return "تم الحذف بنجاح";
        }
        [HttpGet("EndedTime")]
        public List<Borrowedbook> GetAll(int userId)
        {
            var user = Context.users.Include(i=>i.BorrowBooks).ThenInclude(i=>i.Book).FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                BadRequest("لا يوجد مستخدم");
            }
            return user.BorrowBooks.Select(x => new Borrowedbook
            {
                BookName = x.Book.Name,
                broughtDate = x.RentalDate,
                endedDate = x.EndDate,
                isEnded = DateTime.Now > x.EndDate ? true : false
            }).ToList();
        }
    }
}
