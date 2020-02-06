Filter on items in a list
=========================


Manu times when you have 

Most modern applications look more or less like this:


 public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
        {
            new ApiResource("api1", "My API")
        };
    }
