namespace MultiTracksWebApi.Pagination
{
    public class PageDetails
    {
        public PageDetails(int inputtedPageSize)
        {
            ExpectedPageSize = inputtedPageSize > MaxPageSize ? MaxPageSize : inputtedPageSize;
        }
        public PageDetails(decimal totalRows, int pageNumber, int inputtedPageSize, int itemsReturned)
        {
            ActualPageSize = itemsReturned;
            InputtedPageSize = inputtedPageSize;
            ExpectedPageSize = InputtedPageSize > MaxPageSize ? MaxPageSize : InputtedPageSize;
            TotalNumberOfItems = totalRows;
            CurrentPage = pageNumber;
            TotalPages = Math.Ceiling(TotalNumberOfItems / ExpectedPageSize);
            HasNextPage = TotalPages > CurrentPage;
            NextPage = HasNextPage ? CurrentPage + 1 : 0;
            RemainingItems = (ActualPageSize < ExpectedPageSize) || CurrentPage == TotalPages 
                ? 0 : (int)TotalNumberOfItems - (((CurrentPage - 1) * ExpectedPageSize) + ExpectedPageSize);
        }
        public decimal TotalNumberOfItems { get; set; }
        public int RemainingItems { get; set; }

        const int MaxPageSize = 50;
        public static int InputtedPageSize { get; set; }
        public int ExpectedPageSize { get; set; }   // this is what the user inputted as page size
        public decimal TotalPages { get; set; } 
        public bool HasNextPage { get; set;}
        public int CurrentPage { get; set;}
        public int NextPage { get; set;}
        public int ActualPageSize { get; set;}  // this is the count of items returned in a page. It could be smaller than ExpectedPageSize if that's what is left to be returned
    }
}
