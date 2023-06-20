namespace PustokMVC.Areas.Manage.ViewModels
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> query, int page, int totalpage)
        {
            Items = query;
            PageIndex = page;
            TotalPage = totalpage;
        }

        public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPage { get; set; }

        public bool HasPrevious => PageIndex > 1;

        public bool HasNext => PageIndex < TotalPage;

        public static PaginatedList<T> Create(IQueryable<T> query,int pageIndex,int pageSize)
        {
            var items = query.Skip((pageIndex-1) * 2 ).Take(pageSize).ToList();

            var totalPage = (int)Math.Ceiling(query.Count()/(decimal)pageSize);

            return new PaginatedList<T>(items, pageIndex, totalPage);

        }
    }
}
