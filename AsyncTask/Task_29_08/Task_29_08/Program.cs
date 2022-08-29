using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Task_29_08
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] urls = { 
                "https://kontakt.az/",
                "https://www.next.com.az/en",
                "https://irshad.az/",
                "https://www.bakuelectronics.az/",
                "https://www.soliton.az/"
            };
            HttpClient client=new HttpClient();
            Stopwatch watch=new Stopwatch();

            //1st Task:
            Console.WriteLine("1st Starts:");
            watch.Start();
            for (int i = 0; i < urls.Length; i++)
            {
                Console.WriteLine(client.GetStringAsync(urls[i]).Result.Length);
                Console.WriteLine(new String('-',50));
            }
            watch.Stop();
            Console.WriteLine($"1st Task Time: {watch.ElapsedMilliseconds}\n");
            watch.Reset();

            //2nd Task:
            Console.WriteLine("2nd Starts:");
            watch.Start();
            List<Task<string>> tasks=new List<Task<string>>();
            for(int i=0; i<urls.Length; i++)
            {
               tasks.Add(client.GetStringAsync(urls[i]));
            }
            while (tasks.Any())
            {
                Task<string> finished=await Task.WhenAny(tasks);
                Console.WriteLine(finished.Result.Length);
                Console.WriteLine(new String('-', 50));
                tasks.Remove(finished);
            }
            watch.Stop();
            Console.WriteLine($"2nd Task Time: {watch.ElapsedMilliseconds}");
        }
    }
}
