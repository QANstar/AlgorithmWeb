using AlgorithmWeb.Shared;
using BootstrapBlazor.Components;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class Search
    {
        private bool isLoading = false;//加载
        private string inputStr = string.Empty;//输入字符串
        private int[] numArr;//输入数组
        private int searchNum1;//查找数1
        private string PlaceHolderText = "请输入...";//input提示
        private SearchResultModel searchResultModel = new SearchResultModel();//搜索模型
        private string orderTypeStr = string.Empty;//排序类型


        //确认
        private async Task ClickAsyncSureButton()
        {
            string[] resultSTr = inputStr.Split(",");
            numArr = Array.ConvertAll(resultSTr, int.Parse);
            orderType();
            StateHasChanged();
        }

        //排序类型
        private async Task orderType()
        {
            isLoading = true;
            int type = -1;
            type = await Http.GetFromJsonAsync<int>("api/Search/showOrderApi?inputString=" + inputStr);
            if (type != -1)
            {
                switch (type)
                {
                    case 0:
                        orderTypeStr = "未排序";
                        break;
                    case 1:
                        orderTypeStr = "升序";
                        break;
                    case 2:
                        orderTypeStr = "降序";
                        break;
                    case 3:
                        orderTypeStr = "先升后降";
                        break;
                    case 4:
                        orderTypeStr = "先降后升";
                        break;
                }
                isLoading = false;
            }
            StateHasChanged();
        }
        //随机
        private async Task ClickAsyncRandButton()
        {
            Random rand = new Random();
            int n = rand.Next(4, 12);
            int[] resultData = new int[n];
            for (int i = 0; i < n; i++)
            {
                resultData[i] = rand.Next(1,10000);
            }
            inputStr = string.Join(",", resultData);
            StateHasChanged();
        }

        //清空
        private async Task ClickAsyncClearButton()
        {
            inputStr = string.Empty;
            StateHasChanged();
        }

        //输入数图表
        private Task<ChartDataSource> OnInit1(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "输入数据线性图";
            ds.Options.X.Title = "输入数";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = inputStr.Split(",");
            ds.Data.Add(new ChartDataset()
            {
                Label = "数值",
                Data = numArr.Cast<object>()
            });
            return Task.FromResult(ds);
        }
    }
}
