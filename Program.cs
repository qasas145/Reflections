
using System.Diagnostics;
using System.Reflection;
using reflections;

public class Program
{
    public static void Main(string[] args)
    {
        var homeController = new HomeController();


        // we want to do same above code with reflections ;
        // let's try    


        var homeType = typeof(HomeController);
        var targetProp = homeType.GetProperties().FirstOrDefault(pr => pr.IsDefined(typeof(Data), true));
        var getMethod = targetProp.GetMethod;

        // part of traditional way 
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 0; i < 100000; i++)
        {
            var result = (IDictionary<string, string>)getMethod.Invoke(homeController, new object[] { });
        }
        stopWatch.Stop();
        Console.WriteLine(stopWatch.Elapsed.ToString());


        // seperation of codes 
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("-------");
        Console.WriteLine("-------");
        Console.ForegroundColor = ConsoleColor.White;

        // new part of delegates

        var getMethodDelegate = PropertyHelper
            .MakePropertyFast<IDictionary<string, string>>(targetProp);

        // this will get the delegate from the cache because it have been created from the above line already 
        var newGetMethodDelegate = PropertyHelper
                .MakePropertyFast<IDictionary<string, string>>(targetProp);

        //var newResult = getMethodDelegate(home);
        var stopWatchDelegate = new Stopwatch();
        stopWatchDelegate.Start();

        var aboutController = new AboutController();
        for (int i = 0; i < 100000; i++)
        {
            newGetMethodDelegate(homeController); // when getting to this , it will call the WrapProperty method to take the Object
        }
        stopWatchDelegate.Stop();
        Console.WriteLine($"time taken with stop watch for delegates is {stopWatchDelegate.Elapsed.ToString()}");



        // let's try with delegates

        //foreach (var item in newResult)
        //{
        //    Console.WriteLine(item.Key + " " + item.Value);
        //}
    }
}