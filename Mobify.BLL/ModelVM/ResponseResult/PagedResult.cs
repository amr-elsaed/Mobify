namespace Mobify.BLL.ModelVM.ResponseResult
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
