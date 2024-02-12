using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAsyncController : ControllerBase
    {
        public AppDbContext Context { get; set; }
        public UsersAsyncController(AppDbContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(string name)
        {


            await Context.Database.EnsureCreatedAsync();
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("الرجاء ادخال اسم المستخدم");
            }
            var user = new User { Name = name };
            await Context.users.AddAsync(user);
            await Context.SaveChangesAsync();
            return user;



        }
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(int id, string name)
        {


            if (id == null || string.IsNullOrEmpty(name))
            {
                return BadRequest("الرجاء ادخال اسم  او الايدي");
            }
            var user = await Context.users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return BadRequest("المستخدم غير موجود");
            }
            user.Name = name;
            await Context.SaveChangesAsync();
            return user;

        }
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            var user = await Context.users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            return user;


        }
        [HttpGet("GetAllUsers")]
        public async Task<List<User>> GetAllUsers()
        {

            return await Context.users.ToListAsync();


        }

        [HttpGet("Search")]
        public async Task<List<User>> searchUser(string value)
        {

            return await Context.users.Where(i => (i.Name + i.Id).Trim().ToLower().Contains(value.Trim().ToLower())).ToListAsync();


        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {

            var book = await Context.books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return BadRequest("المستخدم غير موجود");
            }
            await Task.Run(() => Context.books.Remove(book));
            await Context.SaveChangesAsync();
            return "تم الحذف بنجاح";

        }
    }
}

