using Microsoft.AspNetCore.Mvc;
using AlgorithmWeb.Server.Class;
using AlgorithmWeb.Shared;

namespace AlgorithmWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FibonacciController : Controller
    {
        //计算指定位置斐波拉契数列的值、所用时间和基本操作数
        [HttpGet]
        public List<FibonacciModel> returnFibonacciNum(long n)
        {
            List<FibonacciModel> fibonacciModelList = new List<FibonacciModel>();
            FibonacciNun fibonacci = new FibonacciNun();
            fibonacciModelList.Add(fibonacci.iteration(n));
            fibonacciModelList.Add(fibonacci.iterationPro(n));
            fibonacciModelList.Add(fibonacci.recursion(n));
            fibonacciModelList.Add(fibonacci.formulas(n));
            fibonacciModelList.Add(fibonacci.matrix(n));


            return fibonacciModelList;
        }
        //计算递归
        [HttpGet]
        public FibonacciModel returnRecursionFibonacciNum(long n)
        {
            FibonacciNun fibonacci = new FibonacciNun();


            return fibonacci.recursion(n);
        }

        //计算迭代计算最大int时的斐波那契数
        [HttpGet]
        public FibonacciNumModel returnFibonacciMaxInt()
        {
            FibonacciNun fibonacci = new FibonacciNun();
            return fibonacci.iterationMaxInt();


        }

        //计算递归计算最大int时的斐波那契数
        [HttpGet]
        public FibonacciNumModel returnRecursionMaxInt()
        {
            FibonacciNun fibonacci = new FibonacciNun();
            return fibonacci.recursionMaxInt();


        }
        //计算30内用递归能算的最大斐波那契数
        [HttpGet]
        public FibonacciMax30Model returnMax30()
        {
            FibonacciNun fibonacci = new FibonacciNun();
            return fibonacci.recursionTime30();


        }
        //计算30内用迭代能算的最大斐波那契数
        [HttpGet]
        public FibonacciMax30Model iterationMax30()
        {
            FibonacciNun fibonacci = new FibonacciNun();
            return fibonacci.iterationTime30();


        }
        //找出显示公式出现误差的最小值
        [HttpGet]
        public FibonacciErrModel findFormulasErr()
        {
            FibonacciNun fibonacci = new FibonacciNun();
            return fibonacci.findFormulasErr();
        }
    }
}
