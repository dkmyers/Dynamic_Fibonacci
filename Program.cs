internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Which edition would you like to run?");
        Console.WriteLine("1 Recursive Fibonacci\n2 Dynamic Fibonnaci\n3 Experimental Memoized Fibonacci");
        var choice = Console.ReadLine();
        var input = "";
        long result = 0;
        switch (choice)
        {
            case "1":
                Console.WriteLine("Running standard fibonacci sequence. What is your input?");
                try
                {
                    input = Console.ReadLine();
                    if(input != null)
                    {
                        result = standardFibonacci(Convert.ToInt64(input));
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number.");
                    }

                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Issue discovered when reading your input. Input read was: {input}");
                    throw;
                }
                
                break;
            case "2":
                Console.WriteLine("Running Dynamic fibonacci sequence. What is your input?");
                try
                {
                    List<long> memoList = new List<long>();
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        //Allocate memory for memoList, initializing to -1 for logic loop to easily determine uninitialized data
                        for(long x = 0; x <= Convert.ToInt64(input); x++)
                        {
                            memoList.Add(-1);
                        }
                        result = dynamicFibonacci(Convert.ToInt32(input), memoList);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number.");
                    }

                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Issue discovered when reading your input. Input read was: {input}");
                    throw;
                }
                break;
            case "3":
                Console.WriteLine("Running constant-memory dynamic fibonacci sequence. What is your input?");
                try
                {
                    List<long> memoList = new List<long>();
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        //Allocate memory for memoList
                        memoList.Add(1);
                        memoList.Add(1);
                        result = constantDynamicFibonacci(Convert.ToInt32(input), memoList);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number.");
                    }

                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Issue discovered when reading your input. Input read was: {input}");
                    throw;
                }
                break;
            default: 
                Console.WriteLine("Please just enter a number such as '1' or '2'");
                break;
        }


    }


    public static long standardFibonacci(long n)
    {
        
        try
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            else
            {
                return standardFibonacci(n - 1) + standardFibonacci(n - 2);
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Standard Fibonacci sequence ran into an error. Given input is {n}");
            throw;
        }

    }

    //Keeps a list of previously-calculated values in memoList
        //This allows future calculations to reuse that value
        //However, memoList must be defined with a length of the original input
    public static long dynamicFibonacci(int n, List<long> memoList)
    {
        
        if (memoList[n] != -1)
        {
            return memoList[n];
        }

        if (n == 1 || n == 2)
        {
            return 1;
        }

        memoList[n] = dynamicFibonacci(n - 1, memoList) + dynamicFibonacci(n - 2, memoList);
        return memoList[n];
    }

    //An attempt at keeping memoized Fibonacci results within constant memory constraints
    //memoList size is 2, both values initialized to 1
    //index ranges from 0 to n
    //n is threshold where function returns memoList[0] + memoList[1]
    public static long constantDynamicFibonacci(int n, List<long> memoList)
    {
        if(n == 1 || n == 2)
        {
            return 1;
        }
        //Index begins at 3 because memoList is initialized with results for n = 1 and n = 2.
        //Next iteration is therefore iteration 3
        int index = 3;
        return constantDynamicFibonacci(n, memoList, index);
    }
    public static long constantDynamicFibonacci(int n, List<long> memoList, int index)
    {

        if (index == n)
        {
            return memoList[0] + memoList[1];
        }
        //Set up memoList for next iteration
        long median = memoList[1];
        memoList[1] = memoList[0] + memoList[1];
        memoList[0] = median;
        //Console.WriteLine($"0: {memoList[0]} 1: {memoList[1]}");
        return constantDynamicFibonacci(n, memoList, index + 1);
    }
}