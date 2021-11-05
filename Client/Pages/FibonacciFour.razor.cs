using AlgorithmWeb.Shared;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class FibonacciFour
    {
        bool isLoading = false;
        //找出显示公式出现误差的最小值
        FibonacciErrModel fibonacciErrModel = new FibonacciErrModel();
        bool isLoading1 = false;
        bool isSum1 = false;


        public async Task sumButton()
        {
            isLoading1 = true;
            fibonacciErrModel = await Http.GetFromJsonAsync<FibonacciErrModel>("api/Fibonacci/findFormulasErr");
            isLoading1 = false;
            isSum1 = true;
            StateHasChanged();
        }
    }
}
