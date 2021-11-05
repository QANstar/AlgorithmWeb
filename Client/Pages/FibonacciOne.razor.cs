using AlgorithmWeb.Shared;
using BootstrapBlazor.Components;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class FibonacciOne
    {
        bool isLoading = false;
        private string PlaceHolderText = "请输入...";//input提示
        private int numInput = 0;//输入的第几个数
        List<FibonacciModel> fibonacciModelList = new List<FibonacciModel>();
        protected override async Task OnInitializedAsync()
        {
            fibonacciModelList = null;

        }

        //input键盘enter事件
        private async Task OnEnterAsync(int val)
        {
            isLoading = true;
            fibonacciModelList = await Http.GetFromJsonAsync<List<FibonacciModel>>("api/Fibonacci/returnFibonacciNum?n=" + val);
            if (fibonacciModelList != null)
            {
                isLoading = false;
            }
            StateHasChanged();
        }

        //input键盘esc事件
        private Task OnEscAsync(int val)
        {
            return Task.CompletedTask;
        }

        private async Task ClickAsyncButton()
        {
            isLoading = true;
            fibonacciModelList = await Http.GetFromJsonAsync<List<FibonacciModel>>("api/Fibonacci/returnFibonacciNum?n=" + numInput);
            if (fibonacciModelList != null)
            {
                isLoading = false;
            }
            StateHasChanged();
        }
        //图表
        private Task<ChartDataSource> OnInit1(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "操作次数柱状图";
            ds.Options.X.Title = "方法名称";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = fibonacciModelList.Select(x => x.name).ToList();
            ds.Data.Add(new ChartDataset()
            {
                Label = "操作次数",
                Data = fibonacciModelList.Select(x => x.opeNum).ToList().Cast<object>()
            });
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnInit2(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "时间柱状图";
            ds.Options.X.Title = "方法名称";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = fibonacciModelList.Select(x => x.name).ToList();
            ds.Data.Add(new ChartDataset()
            {
                Label = "时间",
                Data = fibonacciModelList.Select(x => x.time).ToList().Cast<object>()
            });
            return Task.FromResult(ds);
        }

    }
}
