using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoodBooksRecommender._Objects
{
    public class SimilarBook
    {
        [JsonProperty(PropertyName = "BookID")]
        public int BookID { get; set; }
        [JsonProperty(PropertyName = "BookName")]
        public string BookName { get; set;}
        [JsonProperty(PropertyName = "BookDistance")]
        public float BookDistance { get; set; }
    }
}
