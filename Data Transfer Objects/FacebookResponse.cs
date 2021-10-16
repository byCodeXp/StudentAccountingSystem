using System;
using Newtonsoft.Json;

namespace Data_Transfer_Objects
{
    public class FacebookResponse
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "birthday")]
        public DateTime BirthDay { get; set; }
    }
}