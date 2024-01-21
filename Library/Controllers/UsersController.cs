using Library.DataAccess;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public AppDbContext Context { get; set; } = new AppDbContext();

    [HttpPost]
        public ActionResult<User> AddUser(string name)
        {


            Context.Database.EnsureCreated();
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("الرجاء ادخال اسم المستخدم");
            }
            var user = new User { Name = name };
            Context.users.Add(user);
            Context.SaveChanges();
            return user;



        }
        [HttpPut]
        public ActionResult<User> UpdateUser(int id, string name)
        {


            if (id == null || string.IsNullOrEmpty(name))
            {
                return BadRequest("الرجاء ادخال اسم  او الايدي");
            }
            var user = Context.users.FirstOrDefault(i => i.Id == id);
            if (user == null)
            {
                return BadRequest("المستخدم غير موجود");
            }
            user.Name = name;
            Context.SaveChanges();
            return user;

        }
        [HttpGet]
        public ActionResult<User> GetUser(int id)
        {

            var user = Context.users.FirstOrDefault(i => i.Id == id);
            if (user == null)
            {
                return BadRequest("الكتاب غير موجود");
            }
            return user;


        }
        [HttpGet("GetAllUsers")]
        public List<User> GetAllUsers()
        {

            return Context.users.ToList();


        }

        [HttpGet("Search")]
        public List<User> searchUser(string value)
        {

            return Context.users.Where(i => (i.Name + i.Id).Trim().ToLower().Contains(value.Trim().ToLower())).ToList();


        }

        [HttpDelete]
        public ActionResult<string> DeleteUser(int id)
        {

            var book = Context.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest("المستخدم غير موجود");
            }
            Context.books.Remove(book);
            Context.SaveChanges();
            return "تم الحذف بنجاح";

        }
    }

}
