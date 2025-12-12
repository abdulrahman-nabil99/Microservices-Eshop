namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
    {
        public int PageIndex { get;} = pageIndex;
        public int Pagesize { get;} = pageSize;
        public long Count { get;} = count;
        public IEnumerable<TEntity> Data { get;} = data;
    }
}
