namespace LibraryGUI.ViewModels
{
    public class BorrowedBook
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string UserName { get; set; }
    }

}
