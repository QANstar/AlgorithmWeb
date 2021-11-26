using AlgorithmWeb.Shared;
namespace AlgorithmWeb.Server.Class
{
    public class Search
    {
        //判断排序类型
        public int orderType(int[] data)
        {
            bool isUp = true;
            bool isDown = true;
            int isUpAndDown = 0;
            int isDownAndUp = 0;
            for(int i = 1; i < data.Length; i++)
            {
                if (data[i - 1] < data[i])
                {
                    isDown = false;
                    if(isDownAndUp == 0)
                    {
                        isDownAndUp = 1;
                    }
                    if(isUpAndDown == 1)
                    {
                        isUpAndDown = 2;
                    }
                }
                else
                {
                    isUp = false;
                    if (isDownAndUp == 1)
                    {
                        isDownAndUp = 2;
                    }
                    if (isUpAndDown == 0)
                    {
                        isUpAndDown = 1;
                    }
                }
            }
            if(isUp)
            {
                return 1;
            }
            else if (isDown)
            {
                return 2;
            }
            else if(isUpAndDown == 1)
            {
                return 3;
            }
            else if(isDownAndUp == 1)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }

        //顺序搜索
        public SearchResultModel sequenceSearch(int[] data,int n)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.opeNum = 0;
            searchResultModel.index = 1;
            searchResultModel.type = "顺序搜索";
            int midNum = Math.Abs(n - data[0]);
            for (int i = 0; i < data.Length; i++)
            {
                searchResultModel.opeNum++;
                if(Math.Abs(n - data[i]) < midNum)
                {
                    midNum = Math.Abs(n - data[i]);
                    searchResultModel.index = i + 1;
                }
                if (data[i] == n)
                {
                    searchResultModel.index = i + 1;
                    return searchResultModel;
                }
            }
            return searchResultModel;
        }

        /// <summary>
        /// 二分查找
        /// </summary>
        /// <param name="arr"> 数组 </param>
        /// <param name="low">开始索引 0</param>
        /// <param name="high">结束索引 </param>
        /// <param name="key">要查找的对象</param>
        /// <returns></returns>
        public SearchResultModel binartSearch(int[] data,int n)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index=0;
            searchResultModel.opeNum=0;
            searchResultModel.type = "二分查找";
            long opeNum = searchResultModel.opeNum;
            int index = 1;
            int midNum = Math.Abs(n - data[0]);
            BinarySearchMain(data, 0, data.Length - 1, n,ref index,ref opeNum,midNum);
            searchResultModel.opeNum = opeNum;
            searchResultModel.index = index;
            return searchResultModel;
        }

        private int BinarySearchMain(int[] arr, int low, int high, int key,ref int index,ref long opeNum,int midNum)
        {
            opeNum++;
            int mid = (low + high) / 2;
            if (low > high)
                return -1;
            else
            {
                if (Math.Abs(key - arr[mid]) < midNum)
                {
                    midNum = Math.Abs(key - arr[mid]);
                    index = mid + 1;
                }
                if (arr[mid] == key)
                    return mid + 1;
                else if (arr[mid] > key)
                    return BinarySearchMain(arr, low, mid - 1, key,ref index, ref opeNum,midNum);
                else
                    return BinarySearchMain(arr, mid + 1, high, key, ref index, ref opeNum,midNum);
            }
        }


        //三分查找

        public SearchResultModel threeSearch(int[] data, int n)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "三分查找";
            long opeNum = searchResultModel.opeNum;
            searchResultModel.index = ArrayTernarySearch1(data, n,ref opeNum);
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }

        /// <summary>
        /// 三分查找
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        private int ArrayTernarySearch1(int[] arr, int target,ref long opeNum)
        {
            //开始;
            int len = arr.Length;
            int low = 0;
            int high = len - 1;
            int midNum = Math.Abs(target - arr[0]);
            int midIndex = 1;
            while (low <= high)
            {
                opeNum++;
                //计算中间位置；
                int middle1Index = low + (high - low) / 3;
                int middle2Index = high - (high - low) / 3;
                //中间值；
                int middle1Vaule = arr[middle1Index];
                int middle2Vaule = arr[middle2Index];
                if (Math.Abs(target - arr[middle1Index]) < midNum)
                {
                    midNum = Math.Abs(target - arr[middle1Index]);
                    midIndex = middle1Index + 1;
                }
                if (Math.Abs(target - arr[middle2Index]) < midNum)
                {
                    midNum = Math.Abs(target - arr[middle2Index]);
                    midIndex = middle2Index + 1;
                }
                if (target <= middle1Vaule)  //做好区间分割；
                {
                    if (target == middle1Vaule)
                    {
                        return middle1Index + 1;
                    }
                    else
                    {
                        high = middle1Index - 1;
                    }
                }
                else if (middle1Vaule < target && target <= middle2Vaule) //做好区间分割；
                {
                    if (target == middle2Vaule)
                    {
                        return middle2Index + 1;
                    }
                    else
                    {
                        low = middle1Index + 1;
                        high = middle2Index - 1;
                    }

                }
                else if (target > middle2Vaule) //做好区间分割；
                {
                    low = middle2Index + 1;
                }

            }

            return midIndex;
        }

        //插值查找

        public SearchResultModel interpolationSearch(int[] data, int n)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "插值查找";
            long opeNum = searchResultModel.opeNum;
            int midNum = Math.Abs(n - data[0]);
            searchResultModel.index = InsertSearch(data, 0, data.Length-1, n, ref opeNum, midNum);
            searchResultModel.opeNum = opeNum;
            
            return searchResultModel;
        }


        public int InsertSearch(int[] arr, int low, int high, int value,ref long opeNum,int midNum)
        {
            if (arr == null || arr.Length == 0 || low >= high)
            {
                return -1;
            }
            int midIndex = 0;
            int mid;
            while (low <= high)
            {
                opeNum++;
                mid = low + ((value - arr[low]) / (arr[high] - arr[low])) * (high - low);// 插值查找的核心代码
                if (Math.Abs(value - arr[mid]) < midNum)
                {
                    midNum = Math.Abs(value - arr[mid]);
                    midIndex = mid + 1;
                }
                if (value > arr[mid])//值在arr[mid]的右边
                {
                    low = mid + 1;
                }
                if (value < arr[mid])//值在arr[mid]的左边
                {
                    high = mid - 1;
                }
                if (value == arr[mid])
                {
                    return mid + 1;
                }
            }
            return midIndex;
        }


        //二分查找最大值
        public SearchResultModel binartSearchMax(int[] data)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "二分查找";
            long opeNum = searchResultModel.opeNum;
            searchResultModel.index = BinarySearchMaxMain(data, 0, data.Length - 1, ref opeNum);
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }

        private int BinarySearchMaxMain(int[] arr, int low, int high, ref long opeNum)
        {
            opeNum++;
            int mid = (low + high) / 2;
            if (arr[mid] > arr[mid - 1] && arr[mid] > arr[mid + 1])
            {
                return mid;
            }
            else if(arr[mid]>arr[mid - 1] && arr[mid] < arr[mid + 1])
            {
                return BinarySearchMaxMain(arr,mid+1,high,ref opeNum);
            }
            else
            {
                return BinarySearchMaxMain(arr,low, mid - 1,ref opeNum);
            }
        }

        //三分查找最大值
        public SearchResultModel threeSearchMax(int[] data)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "三分查找";
            long opeNum = searchResultModel.opeNum;
            searchResultModel.index = ArrayTernarySearchMax(data, ref opeNum);
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }

        /// <summary>
        /// 三分查找
        /// </summary>
        private int ArrayTernarySearchMax(int[] arr, ref long opeNum)
        {
            //开始;
            int len = arr.Length;
            int low = 0;
            int high = len - 1;
            while (low <= high)
            {
                opeNum++;
                //计算中间位置；
                int middle1Index = low + (high - low) / 3;
                int middle2Index = high - (high - low) / 3;
                if(arr[middle1Index] > arr[middle1Index - 1] && arr[middle1Index] > arr[middle1Index + 1])
                {
                    return middle1Index;
                }
                else if(arr[middle2Index] > arr[middle2Index - 1] && arr[middle2Index] > arr[middle2Index + 1])
                {
                    low = middle2Index;
                }
                else if(arr[middle2Index] > arr[middle2Index - 1] && arr[middle2Index] < arr[middle2Index + 1])
                {
                    low = middle2Index + 1;
                }
                else if(arr[middle1Index] < arr[middle2Index - 1] && arr[middle1Index] > arr[middle1Index + 1])
                {
                    high = middle1Index - 1;
                }
                else
                {
                    low = middle1Index + 1;
                    high = middle2Index - 1;
                }
                

            }

            return -1;
        }

        //蛮力法查找第k个最小值
        public SearchResultModel bruteSearchMinK(int[] arr,int k)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "蛮力法";
            long opeNum = searchResultModel.opeNum;
            searchResultModel.index = bruteSearchMinKMain(arr, k,ref opeNum);
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }

        private int bruteSearchMinKMain(int[] arr,int k,ref long opeNum)
        {
            int minTemp = 0;
            for(int i = 0; i < k; i++)
            {
                minTemp = i;
                for (int j = i; j < arr.Length; j++)
                {
                    opeNum++;
                    if (arr[j] < arr[minTemp])
                    {
                        minTemp = j;
                    }
                }
                Swap(arr, minTemp, i);
            }
            return arr[k-1];
        }

        //预排序查找第k个最小值
        public SearchResultModel sortFindK(int[] arr,int k)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "预排序";
            long opeNum = searchResultModel.opeNum;
            QuickSort(arr, 0, arr.Length - 1,ref opeNum);
            searchResultModel.index = arr[k - 1];
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }


        private void QuickSort(int[] numbers, int start, int end, ref long opeNum)
        {
            if (numbers == null || numbers.Length <= 0)
            {
                return;
            }

            if (start < 0 || end < 0 || start >= end)
            {
                return;
            }
            int pivort = start;
            int mid = Partition(numbers, start, end, pivort, ref opeNum);// mid 已经定位
            QuickSort(numbers, start, mid - 1, ref opeNum);
            QuickSort(numbers, mid + 1, end, ref opeNum);
        }

        private int Partition(int[] numbers, int start, int end, int pivort, ref long opeNum)
        {
            int pivortValue = numbers[pivort];

            // 置换在数组中位置
            Swap(numbers, end, pivort);

            int storeIndex = start;
            // 执行分割操作
            for (int i = start; i <= end - 1; i++)
            {
                opeNum++;
                if (numbers[i] < pivortValue)
                {
                    Swap(numbers, i, storeIndex);
                    storeIndex++;
                }
            }

            Swap(numbers, storeIndex, end);

            return storeIndex;
        }

        private void Swap(int[] array, int indexX, int indexY)
        {
            int temp = array[indexX];
            array[indexX] = array[indexY];
            array[indexY] = temp;
        }

        //减可变规模查找第k个最小值
        public SearchResultModel DACFindMinK(int[] arr,int k)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            searchResultModel.index = 0;
            searchResultModel.opeNum = 0;
            searchResultModel.type = "减可变规模";
            long opeNum = searchResultModel.opeNum;
            searchResultModel.index = QuickSelect(arr,0,arr.Length-1, k, ref opeNum);
            searchResultModel.opeNum = opeNum;
            return searchResultModel;
        }

        private int QuickSelect(int[] a, int left, int right, int k,ref long opeNum)
        {
            if (left > right)
                return -1;
            int i, j, t, temp;
            temp = a[left];
            i = left;
            j = right;
            while (i != j)
            {
                opeNum++;
                while (a[j] >= temp && i < j)
                {
                    j--;
                }
                while (a[i] <= temp && i < j)
                {
                    i++;
                }
                if (i < j)
                {
                    t = a[i];
                    a[i] = a[j];
                    a[j] = t;
                }
            }
            a[left] = a[i];
            a[i] = temp;
            if (i == k - 1) return a[i];
            else if (i < k - 1) return QuickSelect(a, i + 1, right, k,ref opeNum);
            else return QuickSelect(a, left, i - 1, k,ref opeNum);
        }
    }
}
