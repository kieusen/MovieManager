namespace Movie_01.Data.Helpers
{
    public class Pager
    {
        public Pager()
        {
        }

        public Pager(int totalItems, int page, int pageSize, string controller, string action)
        {
            TotalItems = totalItems;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(totalItems / (double) pageSize);

            StartPage = page - 5;
            EndPage = page + 4;

            if (StartPage <= 0)
            {
                EndPage = EndPage - (StartPage - 1);
                StartPage = 1;
            }

            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;

                if (EndPage > 10){
                    StartPage = EndPage - 9;
                }
            }

            Controller = controller;
            Action = action;
        }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }        

        public string Controller { get; set; }
        public string Action { get; set; }
    }
}