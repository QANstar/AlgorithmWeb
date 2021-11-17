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
        public static int BinarySearch(int[] arr, int low, int high, int key)
        {
            int mid = (low + high) / 2;
            if (low > high)
                return -1;
            else
            {
                if (arr[mid] == key)
                    return mid;
                else if (arr[mid] > key)
                    return BinarySearch(arr, low, mid - 1, key);
                else
                    return BinarySearch(arr, mid + 1, high, key);
            }
        }

        //三分查找
        static bool threeSearch(int[] sortedArray, int number)
        {
            if (sortedArray.Length == 0)
                return false;

            int start = 0;
            int end = sortedArray.Length - 1;

            while (end >= start)
            {
                int firstMiddle = (end - start) / 3 + start;
                int secondMiddle = end - (end - start) / 3;
                if (sortedArray[firstMiddle] > number)
                    end = firstMiddle - 1;
                else if (sortedArray[secondMiddle] < number)
                    start = secondMiddle + 1;
                else if (sortedArray[firstMiddle] != number && sortedArray[secondMiddle] != number)
                {
                    end = secondMiddle - 1;
                    start = firstMiddle + 1;
                }
                else
                    return true;
            }
            return false;
        }

        //插值查找
        private static int InterpolationSearch(int[] array, int key, int low, int high)
        {
            if (low > high) return -1;
            var mid = (int)(low + ((double)key - array[low]) /
                (array[high] - array[low]) * (high - low));
            if (array[mid] == key)
                return mid;
            else if (array[mid] > key)
                return InterpolationSearch(array, key, low, mid - 1);
            else
                return InterpolationSearch(array, key, mid + 1, high);
        }
    }
}
