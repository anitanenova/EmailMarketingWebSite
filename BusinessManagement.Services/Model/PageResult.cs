namespace BusinessManagement.Services.Model
{
    using System.Collections.Generic;

    public class PageResult<T> where T: class 
    {
        public bool IsFailed { get; set; }
        public int StatusCode { get; set; }
        public string Massage { get; set; }
        public List<T> Result { get; set; }
    }
}