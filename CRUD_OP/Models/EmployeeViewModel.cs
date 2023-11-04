namespace CRUD_OP.Models

{
    public class RenderPages<T> where T : class
    {

        public List<T>? Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

    }
}
