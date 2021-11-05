using AlgorithmWeb.Shared;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class FibonacciTwo
    {
        bool isLoading = false;
        //迭代求的时间和第几个斐波那契数
        FibonacciNumModel iteraFibMode = new FibonacciNumModel();
        bool isLoading1 = false;
        bool isSum1 = false;
        //递归求的时间和第几个斐波那契数
        FibonacciNumModel recursionFibMode = new FibonacciNumModel();
        bool isLoading2 = false;
        bool isSum2 = false;
        //递归用迭代结果计算
        FibonacciModel recursionFibMode2 = new FibonacciModel();
        bool isLoading3 = false;
        bool isSum3 = false;
        bool inOneMin = false;

        public async Task iterationMaxIntClick()
        {
            isLoading1 = true;
            iteraFibMode = await Http.GetFromJsonAsync<FibonacciNumModel>("api/Fibonacci/returnFibonacciMaxInt");
            isLoading1 = false;
            isSum1 = true;
            StateHasChanged();
        }
        public async Task recursionMaxIntClick()
        {
            isLoading2 = true;
            recursionFibMode = await Http.GetFromJsonAsync<FibonacciNumModel>("api/Fibonacci/returnRecursionMaxInt");
            isLoading2 = false;
            isSum2 = true;
            StateHasChanged();
        }

        public async Task recursionClick()
        {
            isLoading3 = true;
            if(iteraFibMode.num != 0)
            {
                recursionFibMode2 = await Http.GetFromJsonAsync<FibonacciModel>("api/Fibonacci/returnRecursionFibonacciNum?n=" + iteraFibMode.num);
            }
            if(recursionFibMode2.time <= 60000)
            {
                inOneMin = true;
            }
            else
            {
                inOneMin = false;
            }
            isLoading3 = false;
            isSum3 = true;

            StateHasChanged();
        }
    }
}
