namespace BusinessManagement.Services.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class OData<T>
    {
        [JsonProperty("odata.context")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public List<T> Value { get; set; }
        [JsonProperty("@odata.count")]
        public int Count { get; set; }
    }
}