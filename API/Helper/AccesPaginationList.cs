namespace API.Helper
{
    public class AccesPaginationList
    {
        public AccesPaginationList()
        {

        }
        public AccesPaginationList(int pageIndex, int pageSize, int count, IReadOnlyList<object> data)
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
        public AccessFilterOptions AccessFilterOptions { get; set; }

    }
}

