namespace BusinessManagement.Services.Model
{
    public class ItemResult<T> 
    {
        public bool IsFailed { get; set; }
        public int StatusCode { get; set; }
        public string Massage { get; set; }
        public T Result { get; set; }
    }
}