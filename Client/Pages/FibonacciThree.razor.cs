using AlgorithmWeb.Shared;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class FibonacciThree
    {
        bool isLoading = false;
        //迭代求30s内最大的斐波那契数和下一个
        FibonacciMax30Model iteraFibMode = new FibonacciMax30Model();
        bool isLoading1 = false;
        bool isSum1 = false;
        //递归求30s内最大的斐波那契数和下一个
        FibonacciMax30Model recursionFibMode = new FibonacciMax30Model();
        bool isLoading2 = false;
        bool isSum2 = false;


        public async Task iterationMax30()
        {
            isLoading1 = true;
            iteraFibMode = await Http.GetFromJsonAsync<FibonacciMax30Model>("api/Fibonacci/iterationMax30");
            isLoading1 = false;
            isSum1 = true;
            StateHasChanged();
        }
        public async Task recursionMax30()
        {
            isLoading2 = true;
            recursionFibMode = await Http.GetFromJsonAsync<FibonacciMax30Model>("api/Fibonacci/returnMax30");
            isLoading2 = false;
            isSum2 = true;
            StateHasChanged();
        }

    }
}
