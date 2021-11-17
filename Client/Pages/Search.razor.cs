using AlgorithmWeb.Shared;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
namespace AlgorithmWeb.Client.Pages
{
    public partial class Search
    {
        private bool isLoading = false;//加载
        private string inputStr = string.Empty;//输入字符串
        private int[] numArr;//输入数组

        private string inputStr2 = string.Empty;//输入字符串2
        private int[] numArr2;//输入数组2

        private string searchNum1 = string.Empty;//查找数1
        private SearchResultModel searchResultModel1 = null;//搜索模型1

        private string PlaceHolderText = "请输入...";//input提示

        private string orderTypeStr = string.Empty;//排序类型
        [NotNull]
        private Message? MessageElement { get; set; }//提示
        [Inject]
        [NotNull]
        public MessageService? MessageService { get; set; }//提示

        //确认
        private async Task ClickAsyncSureButton()
        {
            isLoading = true;
            string[] resultSTr = inputStr.Split(",");
            numArr = Array.ConvertAll(resultSTr, int.Parse);
            orderTypeStr = await orderType(inputStr);
            isLoading = false;

            StateHasChanged();

        }

        //确认2
        private async Task ClickAsyncSureButton2()
        {
            
            string orderTypeRes = await orderType(inputStr2);
            if (orderTypeRes == "升序" || orderTypeRes == "降序")
            {
                string[] resultSTr = inputStr2.Split(",");
                numArr2 = Array.ConvertAll(resultSTr, int.Parse);
            }
            else
            {
                MessageElement.SetPlacement(Placement.Top);
                await MessageService.Show(new MessageOption()
                {
                    Host = MessageElement,
                    Content = "输入的数组不是升序或降序！！！",
                    Icon = "fa fa-info-circle",
                    Color = Color.Danger
                });
            }
                
            StateHasChanged();
        }

        //排序类型
        private async Task<string> orderType(string input)
        {
            string orderType = "";
            
            int type = -1;
            type = await Http.GetFromJsonAsync<int>("api/Search/showOrderApi?inputString=" + input);
            if (type != -1)
            {
                switch (type)
                {
                    case 0:
                        orderType = "未排序";
                        break;
                    case 1:
                        orderType = "升序";
                        break;
                    case 2:
                        orderType = "降序";
                        break;
                    case 3:
                        orderType = "先升后降";
                        break;
                    case 4:
                        orderType = "先降后升";
                        break;
                }
            }
            return orderType;
        }

        //顺序查找
        private async Task sequenceSearchClick()
        {
            searchResultModel1 = await Http.GetFromJsonAsync<SearchResultModel>("api/Search/sequenceSearchApi?inputString=" + inputStr + "&n=" + searchNum1);
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
