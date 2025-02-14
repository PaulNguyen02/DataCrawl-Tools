using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
namespace ConsoleApp1
{
    public class Crawling
    {
        private readonly HttpClient _client;
        private List<News> news_data;
        public Crawling()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            
        }

        public async Task<List<News>> ScrapeProductsAsync(string url)
        {
            news_data = new List<News>();

            try
            {
                var html = await _client.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Example: Scraping products from a page
                var productNodes = doc.DocumentNode.SelectNodes("//article[@class='item-news thumb-left item-news-common']");

                if (productNodes != null)
                {
                    foreach (var node in productNodes)
                    {
                        var news = new News
                        {
                            Title = node.SelectSingleNode(".//h2[@class='title-news']/a")?.InnerText.Trim(),
                            Description = node.SelectSingleNode(".//p[@class='description']/a")?.InnerText.Trim(),
                        };

                        news_data.Add(news);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping URL {url}: {ex.Message}");
            }

            return news_data;
        }
    }
}
