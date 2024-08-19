using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class BaseCommonResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }=string.Empty;
        public List<string> Erorrs { get; set; }=new List<string>();
    }
}
