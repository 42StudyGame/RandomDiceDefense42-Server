using Microsoft.AspNetCore.StaticFiles;

namespace DotNet7_WebAPI.Service
{
    public class JsonContentTypeProvider : FileExtensionContentTypeProvider
    {
        public JsonContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}
