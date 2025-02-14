using ConsoleApp1;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main()
    {
        var scraper = new Crawling();
        var news_data = await scraper.ScrapeProductsAsync("https://vnexpress.net/the-gioi/quan-su");
        string json = JsonConvert.SerializeObject(news_data);

        // Print the JSON string
        Console.WriteLine(json);
    }
}
