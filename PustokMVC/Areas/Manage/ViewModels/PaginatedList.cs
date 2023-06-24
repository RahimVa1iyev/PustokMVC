namespace PustokMVC.Areas.Manage.ViewModels
{
    public class PaginatedList<T>
    {

        public PaginatedList(List<T> items,int page, int totalPage)
        {
            Items = items;
            PageIndex = page;
            TotalPages = totalPage;
        }

        public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrev  =>  PageIndex > 1;

        public bool HasNext => PageIndex < TotalPages;

      

        public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex,int pagesize)
        {
            var Items = query.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
            var TotalPages = (int)Math.Ceiling((query.Count() / (double)pagesize));

            return new PaginatedList<T>(Items,pageIndex, TotalPages);

        }
    }
}
