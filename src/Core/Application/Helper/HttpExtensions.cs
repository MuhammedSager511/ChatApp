using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Helper
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,int currentPage ,int itemPerPage,int  totalItems,int totalPages)
        {
            PaginationHeader paginationHeader=new PaginationHeader(currentPage,itemPerPage,totalItems,totalPages);
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination",JsonSerializer.Serialize(paginationHeader));  
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");  

        }
    }
}
