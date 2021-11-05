using Microsoft.AspNetCore.Mvc;
using AlgorithmWeb.Server.Class;
using AlgorithmWeb.Shared;
namespace AlgorithmWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SearchController : Controller
    {
        //判断排序类型api
        [HttpGet]
        public int showOrderApi(string inputString)
        {
            Search search= new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            return search.orderType(resultData);
        }

        //顺序查找
        [HttpGet]
        public SearchResultModel sequenceSearchApi(string inputString, long n)
        {
            Search search = new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            return search.sequenceSearch(resultData, n);
        }
        


    }
}
