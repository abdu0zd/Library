namespace Library.Entities
{
    public class Book
    {
        public Book() { }
        public Book(int id,string name,string author,string type) {

            Id = id;
            Name = name;
            Author = author;

        }

        public int Id {get; set;}
        public string Name { get; set;}
        public string Author { get; set;}
        public List<Borrow> BorrowUser { get; set; }
    }
}
