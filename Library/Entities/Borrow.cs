using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Borrow
    {

        public Borrow() { }
        public Borrow(int userId ,int bookId , DateTime date)
        {
            UserId = userId;
            BookId =bookId;
            RentalDate = DateTime.Now;
            EndDate = date;
        }
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }   
        public DateTime RentalDate { get; set; }     
        public DateTime EndDate { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }

    }
}
