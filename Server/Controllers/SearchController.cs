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
        public SearchResultModel sequenceSearchApi(string inputString, int n)
        {
            Search search = new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            return search.sequenceSearch(resultData, n);
        }

        //多种方法
        [HttpGet]
        public List<SearchResultModel> muchSearchApi(string inputString, int n)
        {
            Search search = new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            List<SearchResultModel> searchResultModelsList = new List<SearchResultModel>();
            searchResultModelsList.Add(search.sequenceSearch(resultData, n));
            searchResultModelsList.Add(search.binartSearch(resultData, n));
            searchResultModelsList.Add(search.threeSearch(resultData, n));
            searchResultModelsList.Add(search.interpolationSearch(resultData, n));
            return searchResultModelsList;
        }

        //二分检索和三分检索找最大
        [HttpGet]
        public List<SearchResultModel> searchMaxApi(string inputString)
        {
            Search search = new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            List<SearchResultModel> searchResultModelsList = new List<SearchResultModel>();
            searchResultModelsList.Add(search.binartSearchMax(resultData));
            searchResultModelsList.Add(search.threeSearchMax(resultData));
            return searchResultModelsList;
        }

        //查找第k个最小值
        [HttpGet]
        public List<SearchResultModel> searchMinKApi(string inputString, int k)
        {
            Search search = new Search();
            string[] resultSTr = inputString.Split(",");
            int[] resultData = Array.ConvertAll(resultSTr, int.Parse);
            List<SearchResultModel> searchResultModelsList = new List<SearchResultModel>();
            searchResultModelsList.Add(search.bruteSearchMinK(resultData, k));
            searchResultModelsList.Add(search.sortFindK(resultData, k));
            searchResultModelsList.Add(search.DACFindMinK(resultData, k));
            return searchResultModelsList;
        }
    }
}
