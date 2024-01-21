namespace Library.Entities
{
    public class User
    {
        public User() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Borrow> BorrowBooks { get; set; }


    }
}
