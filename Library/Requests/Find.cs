namespace Library.Requests
{
    public class Find
    {
        public class GetAllBooksName
        {
            public int Id_Books { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int Year_public { get; set; }
            public string Description { get; set; }
            public int Copies { get; set; }
            public int Id_genre { get; set; }
        }
        public class GetAllBooksId
        {
            public int Id_Books { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int Year_public { get; set; }
            public string Description { get; set; }
            public int Copies { get; set; }
            public int Id_genre { get; set; }
        }

        public class FindCopies
        {
            public int Copies { get; set; }
        }
    }
}
