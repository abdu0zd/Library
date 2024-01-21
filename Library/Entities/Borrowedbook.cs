namespace Library.Entities
{
    public class Borrowedbook
    {
        public string BookName { get; set; }
        public DateTime broughtDate { get; set; }
        public DateTime endedDate { get; set; }
        public bool isEnded { get; set; }

    }
}
