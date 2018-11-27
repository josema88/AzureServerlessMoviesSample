#r "Newtonsoft.Json"
using Newtonsoft.Json;

public class Movies
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Actors { get; set; }
    public string ReleaseDate { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}