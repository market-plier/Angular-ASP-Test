using Newtonsoft.Json;

namespace Angular_ASP_Test.Dto.Error
{
    public class ErrorDto
    {
        public int Code {get;set;}
        public string Message { get; set; }

        // other fields

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}