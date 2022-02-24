using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestApi1
{

    public class User
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("owes")]
        public List<PersonOwed> Owes { get; set; }
        [JsonPropertyName("owed_by")]
        public List<PersonOwedBy> OwedBy { get; set; }
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }

    public class PersonOwed
    {
        public string Name { get; set; }
        public float Dan { get; set; }
    }

    public class PersonOwedBy
    {
        public float Bob { get; set; }
        public float Dan { get; set; }
    }

}
