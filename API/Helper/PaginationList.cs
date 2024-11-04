namespace API.Helper
{
    public class PaginationList
    {
        public PaginationList()
        {

        }
        public PaginationList(int pageIndex, int pageSize, int count, IReadOnlyList<object> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;

        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<object> Data { get; set; }
        public FilterOptions FilterOptions { get; set; }

    }
}
