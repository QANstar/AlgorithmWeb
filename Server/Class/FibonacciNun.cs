using System.Diagnostics;
using AlgorithmWeb.Shared;

namespace AlgorithmWeb.Server.Class
{
    public class FibonacciNun
    {
        Stopwatch stopWatch = new Stopwatch();

        //迭代算法
        public FibonacciModel iteration(long n)
        {
            long lastNum = 0, last2Num = 0;
            FibonacciModel fibonacciModel= new FibonacciModel();
            fibonacciModel.id = 1;
            fibonacciModel.name = "迭代算法";
            fibonacciModel.resultNum = 0;
            fibonacciModel.time = 0;
            fibonacciModel.opeNum = 0;
            stopWatch.Restart();

            if (n < 1)
            {
                stopWatch.Stop();
                fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
                fibonacciModel.resultNum = 0;
                fibonacciModel.opeNum++;
                return fibonacciModel;
            }
            if (n == 1 || n == 2)
            {
                stopWatch.Stop();
                fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
                fibonacciModel.resultNum = 1;
                fibonacciModel.opeNum++;
                return fibonacciModel;
            }
            lastNum = 1;
            last2Num = 1;
            for (int i = 3; i <= n; i++)
            {
                fibonacciModel.resultNum = lastNum + last2Num;
                lastNum = last2Num;
                last2Num = fibonacciModel.resultNum;

                fibonacciModel.opeNum++;
            }

            stopWatch.Stop();
            fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciModel;
        }

        //迭代改进算法
        public FibonacciModel iterationPro(long n)
        {
            FibonacciModel fibonacciModel = new FibonacciModel();
            fibonacciModel.id = 2;
            fibonacciModel.name = "迭代改进算法";
            fibonacciModel.resultNum = 0;
            fibonacciModel.time = 0;
            fibonacciModel.opeNum = 0;
            stopWatch.Restart();

            if (n > 1)
            {
                long a;
                long b = 1;
                n--;
                a = n & 1;
                n /= 2;
                while (n-- > 0)
                {
                    a += b;
                    b += a;

                    fibonacciModel.opeNum++;
                }

                stopWatch.Stop();
                fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;

                fibonacciModel.resultNum = b;
                return fibonacciModel;
            }

            stopWatch.Stop();
            fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;

            fibonacciModel.resultNum = n;
            return fibonacciModel;
        }

        //递归算法
        public FibonacciModel recursion(long n)
        {
            FibonacciModel fibonacciModel = new FibonacciModel();
            fibonacciModel.id = 3;
            fibonacciModel.name = "递归算法";
            fibonacciModel.resultNum = 0;
            fibonacciModel.time = 0;
            fibonacciModel.opeNum = 0;

            long opeN = 0;
            stopWatch.Restart();

            fibonacciModel.resultNum = recursionMain(n, ref opeN);
            fibonacciModel.opeNum = opeN;
            stopWatch.Stop();
            fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciModel;
        }

        private long recursionMain(long n,ref long opeNum)
        {
            opeNum++;
            if (n == 1 || n == 2)
            {
                return 1;
            }
            if (n > 2)
            {
                return recursionMain(n - 1, ref opeNum) + recursionMain(n - 2, ref opeNum);
            }
            return -1;
        }

        private long recursionMain(long n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            if (n > 2)
            {
                return recursionMain(n - 1) + recursionMain(n - 2);
            }
            return -1;
        }

        //显示公式
        public FibonacciModel formulas(long n)
        {
            FibonacciModel fibonacciModel = new FibonacciModel();
            fibonacciModel.id = 4;
            fibonacciModel.name = "显示公式";
            fibonacciModel.resultNum = 0;
            fibonacciModel.time = 0;
            fibonacciModel.opeNum = 0;
            stopWatch.Restart();
            fibonacciModel.resultNum = (long)(1 / Math.Sqrt(5) * (Math.Pow((1 - Math.Sqrt(5)) / 2, n) - Math.Pow((1 + Math.Sqrt(5)) / 2, n)));

            stopWatch.Stop();
            fibonacciModel.opeNum++;
            fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            fibonacciModel.resultNum = -fibonacciModel.resultNum;

            return fibonacciModel;
        }

        //矩阵
        public FibonacciModel matrix(long n)
        {
            FibonacciModel fibonacciModel = new FibonacciModel();
            fibonacciModel.id = 5;
            fibonacciModel.name = "矩阵";
            fibonacciModel.resultNum = 0;
            fibonacciModel.time = 0;
            fibonacciModel.opeNum = 0;
            int opNum = 0;

            stopWatch.Restart();
            fibonacciModel.resultNum = sumMatrix(n, ref opNum);
            fibonacciModel.opeNum = opNum;
            stopWatch.Stop();
            fibonacciModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciModel;
        }

        /// <summary>
        /// 矩阵法
        /// </summary>
        private long sumMatrix(long n, ref int opNum)
        {
            long[,] a = new long[2, 2] { { 1, 1 }, { 1, 0 } };
            long[,] b = MatirxPower(a, n, ref opNum);
            return b[1, 0];
        }

        private long[,] MatirxPower(long[,] a, long n, ref int opNum)
        {
            if (n == 1) { return a; }
            else if (n == 2) { return MatirxMultiplication(a, a, ref opNum); }
            else if (n % 2 == 0)
            {
                long[,] temp = MatirxPower(a, n / 2, ref opNum);
                return MatirxMultiplication(temp, temp, ref opNum);
            }
            else
            {
                long[,] temp = MatirxPower(a, n / 2, ref opNum);
                return MatirxMultiplication(MatirxMultiplication(temp, temp, ref opNum), a, ref opNum);
            }
        }

        private long[,] MatirxMultiplication(long[,] a, long[,] b, ref int opNum)
        {
            long[,] c = new long[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                        opNum++;
                    }
                }
            }
            return c;
        }


        //迭代算法求int类型最大数是第几个
        public FibonacciNumModel iterationMaxInt()
        {
            int lastNum = 0, last2Num = 0;
            FibonacciNumModel fibonacciNumModel = new FibonacciNumModel();
            fibonacciNumModel.resultNum = 0;
            fibonacciNumModel.time = 0;
            fibonacciNumModel.num = 0;
            stopWatch.Restart();

            lastNum = 1;
            last2Num = 1;
            fibonacciNumModel.num = 2;
            while (fibonacciNumModel.num == 2 || lastNum < fibonacciNumModel.resultNum)
            {
                fibonacciNumModel.resultNum = lastNum + last2Num;
                lastNum = last2Num;
                last2Num = fibonacciNumModel.resultNum;
                fibonacciNumModel.num++;
            }
            fibonacciNumModel.resultNum = lastNum;
            fibonacciNumModel.num--;

            stopWatch.Stop();
            fibonacciNumModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciNumModel;
        }

        //递归算法求int类型最大数是第几个
        public FibonacciNumModel recursionMaxInt()
        {
            FibonacciNumModel fibonacciNumModel = new FibonacciNumModel();
            fibonacciNumModel.resultNum = 0;
            fibonacciNumModel.time = 0;
            fibonacciNumModel.num = 0;
            int lastNum = 1, nowNum = 1;
            int n = 2;
            stopWatch.Restart();
            while (nowNum == 1 || lastNum < nowNum)
            {
                n++;
                lastNum = nowNum;
                nowNum = recursionMaxIntMain(n);
            }

            fibonacciNumModel.resultNum = lastNum;
            fibonacciNumModel.num = n - 1;
            stopWatch.Stop();
            fibonacciNumModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciNumModel;
        }

        private int recursionMaxIntMain(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            if (n > 2)
            {
                return recursionMaxIntMain(n - 1) + recursionMaxIntMain(n - 2);
            }
            return -1;
        }

        //递归计算30s能求得的最大解
        public FibonacciMax30Model recursionTime30()
        {
            FibonacciMax30Model fibonacciNumModel = new FibonacciMax30Model();
            fibonacciNumModel.resultNum = 0;
            fibonacciNumModel.resultNum2 = 0;
            fibonacciNumModel.time1 = 0;
            fibonacciNumModel.time2 = 0;
            fibonacciNumModel.num = 0;
            ulong lastNum = 1, nowNum = 1;
            long n = 2;
            stopWatch.Restart();
            while (true)
            {

                n++;
                lastNum = nowNum;
                nowNum = (ulong)recursionMain(n);
                if (stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000 > 30000)
                {
                    break;
                }
                fibonacciNumModel.time1 = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            }

            fibonacciNumModel.resultNum = lastNum;
            fibonacciNumModel.resultNum2 = nowNum;
            fibonacciNumModel.num = n - 1;
            stopWatch.Stop();
            fibonacciNumModel.time2 = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;

            return fibonacciNumModel;
        }
        //迭代计算30s能求得的最大解
        public FibonacciMax30Model iterationTime30()
        {
            ulong lastNum = 0, last2Num = 0;
            FibonacciMax30Model fibonacciNumModel = new FibonacciMax30Model();
            fibonacciNumModel.resultNum = 0;
            fibonacciNumModel.resultNum2 = 0;
            fibonacciNumModel.time1 = 0;
            fibonacciNumModel.time2 = 0;
            fibonacciNumModel.num = 0;
            stopWatch.Restart();

            lastNum = 1;
            last2Num = 1;
            fibonacciNumModel.num = 2;
            while (true)
            {
                fibonacciNumModel.resultNum = lastNum + last2Num;
                lastNum = last2Num;
                last2Num = fibonacciNumModel.resultNum;
                if (stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000 > 30000)
                {
                    break;
                }
                fibonacciNumModel.time1 = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
                fibonacciNumModel.num++;
            }
            fibonacciNumModel.resultNum = lastNum;
            fibonacciNumModel.resultNum2 = last2Num;

            stopWatch.Stop();
            fibonacciNumModel.time2 = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciNumModel;
        }

        //找出显示公式出现误差的最小值
        public FibonacciErrModel findFormulasErr()
        {
            stopWatch.Restart();
            FibonacciErrModel fibonacciErrModel= new FibonacciErrModel();
            long n = 1;
            long formulasNum = 1;
            long iterationNum = 1;
            while (formulasNum == iterationNum)
            {
                formulasNum = formulas(n).resultNum;
                iterationNum = iterationPro(n).resultNum;
                n++;
            }
            fibonacciErrModel.resultNum = iterationNum;
            fibonacciErrModel.errorNum = formulasNum;
            fibonacciErrModel.num = n - 1;
            stopWatch.Stop();
            fibonacciErrModel.time = stopWatch.ElapsedTicks / (decimal)Stopwatch.Frequency * 1000;
            return fibonacciErrModel;
        }
    }
}
