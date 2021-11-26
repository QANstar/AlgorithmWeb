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
        private List<SearchResultModel> searchResultModelList = null;//查找结果1

        private string inputStr3 = string.Empty;//输入字符串3
        private int[] numArr3;//输入数组3
        private List<SearchResultModel> searchResultModelList2 = null;//查找结果2

        private string inputStr4 = string.Empty;//输入字符串4
        private int[] numArr4;//输入数组4
        private List<SearchResultModel> searchResultModelList3 = null;//查找结果3

        private string searchNum1 = string.Empty;//查找数1
        private string searchNum2 = string.Empty;//查找数2
        private string searchNum3 = string.Empty;//查找数3

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

        //确认3
        private async Task ClickAsyncSureButton3()
        {

            string orderTypeRes = await orderType(inputStr3);
            if (orderTypeRes == "先升后降")
            {
                string[] resultSTr = inputStr3.Split(",");
                numArr3 = Array.ConvertAll(resultSTr, int.Parse);
                searchResultModelList2 = await Http.GetFromJsonAsync<List<SearchResultModel>>("api/Search/searchMaxApi?inputString=" + inputStr3);
            }
            else
            {
                MessageElement.SetPlacement(Placement.Top);
                await MessageService.Show(new MessageOption()
                {
                    Host = MessageElement,
                    Content = "输入的数组不是先升后降！！！",
                    Icon = "fa fa-info-circle",
                    Color = Color.Danger
                });
            }

            StateHasChanged();
        }

        //确认4
        private async Task ClickAsyncSureButton4()
        {

            string orderTypeRes = await orderType(inputStr4);
            if (orderTypeRes == "未排序")
            {
                string[] resultSTr = inputStr4.Split(",");
                numArr4 = Array.ConvertAll(resultSTr, int.Parse);
            }
            else
            {
                MessageElement.SetPlacement(Placement.Top);
                await MessageService.Show(new MessageOption()
                {
                    Host = MessageElement,
                    Content = "输入的数组不是无序数组！！！",
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

        //多种查找
        private async Task muchSearchClick()
        {
            searchResultModelList = await Http.GetFromJsonAsync<List<SearchResultModel>>("api/Search/muchSearchApi?inputString=" + inputStr2 + "&n=" + searchNum2);
        }

        //查找数组中第k个最小元素
        private async Task kSearchClick()
        {
            if(int.Parse(searchNum3)> numArr4.Length)
            {
                MessageElement.SetPlacement(Placement.Top);
                await MessageService.Show(new MessageOption()
                {
                    Host = MessageElement,
                    Content = "输入的k值大于数组长度",
                    Icon = "fa fa-info-circle",
                    Color = Color.Danger
                });
            }
            else
            {
                searchResultModelList3 = await Http.GetFromJsonAsync<List<SearchResultModel>>("api/Search/searchMinKApi?inputString=" + inputStr4 + "&k=" + searchNum3);
            }
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

        private async Task ClickAsyncRandButton2()
        {
            Random rand = new Random();
            int n = rand.Next(4, 12);
            int[] resultData = new int[n];
            for (int i = 0; i < n; i++)
            {
                resultData[i] = rand.Next(1, 10000);
            }
            inputStr4 = string.Join(",", resultData);
            StateHasChanged();
        }

        //清空
        private async Task ClickAsyncClearButton()
        {
            inputStr = string.Empty;
            StateHasChanged();
        }

        //清空
        private async Task ClickAsyncClearButton2()
        {
            inputStr2 = string.Empty;
            StateHasChanged();
        }

        //清空
        private async Task ClickAsyncClearButton3()
        {
            inputStr3 = string.Empty;
            StateHasChanged();
        }

        //清空
        private async Task ClickAsyncClearButton4()
        {
            inputStr4 = string.Empty;
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

        private Task<ChartDataSource> OnInit2(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "操作次数柱状图";
            ds.Options.X.Title = "方法名称";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = searchResultModelList.Select(x => x.type).ToList();
            ds.Data.Add(new ChartDataset()
            {
                Label = "操作次数",
                Data = searchResultModelList.Select(x => x.opeNum).ToList().Cast<object>()
            });
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnInit3(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "操作次数柱状图";
            ds.Options.X.Title = "方法名称";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = searchResultModelList2.Select(x => x.type).ToList();
            ds.Data.Add(new ChartDataset()
            {
                Label = "操作次数",
                Data = searchResultModelList2.Select(x => x.opeNum).ToList().Cast<object>()
            });
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnInit4(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "操作次数柱状图";
            ds.Options.X.Title = "方法名称";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = searchResultModelList3.Select(x => x.type).ToList();
            ds.Data.Add(new ChartDataset()
            {
                Label = "操作次数",
                Data = searchResultModelList3.Select(x => x.opeNum).ToList().Cast<object>()
            });
            return Task.FromResult(ds);
        }
    }
}
