using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowAsyncController : ControllerBase
    {
        public AppDbContext Context { get; set; }

        public BorrowAsyncController(AppDbContext context)
        {
            Context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Borrow>> Borrow(int userid, int bookid, DateTime date)
        {


            await Context.Database.EnsureCreatedAsync();
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
            await Context.borrow.AddAsync(borrow);
            await Context.SaveChangesAsync();
            return borrow;
        }
        [HttpGet("SearchBorrow")]
        public async Task<List<Borrow>> searchBorrow(int UserId)
        {

            return await Context.borrow.Where(i => i.UserId == UserId).ToListAsync();

        }

        [HttpDelete]
        public async Task<ActionResult<string>> ReturnBook(int userId, int bookId)
        {

            var borrow = await Context.borrow.FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);
            if (borrow == null)
            {
                return BadRequest(" هذا المستخدم لم يستأجر هذا الكتاب");
            }
            await Task.Run(() => Context.borrow.Remove(borrow));
            Context.SaveChangesAsync();
            return "تم الحذف بنجاح";
        }
        [HttpGet("EndedTime")]
        public async Task<List<Borrowedbook>> GetAll(int userId)
        {
            var user = await Context.users.Include(i => i.BorrowBooks).ThenInclude(i => i.Book).FirstOrDefaultAsync(x => x.Id == userId);
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

