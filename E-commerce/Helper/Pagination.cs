using E_commerce.Dtos;

namespace E_commerce.Helper
{
    public class Pagination<T>
    {
       

       

        public  int PageIndex { get; set; }
        public  int PageSize { get; set; }
        public  int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageZize,int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageZize;
            Data = data;
            Count = count;
        }
    }
}
