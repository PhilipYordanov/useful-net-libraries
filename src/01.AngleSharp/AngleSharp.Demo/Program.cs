using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Text;

//Configurations
Console.OutputEncoding = Encoding.UTF8;
IConfiguration config = Configuration.Default.WithDefaultLoader();
IBrowsingContext context = BrowsingContext.New(config);

//Get first 2000 cars
for (int page = 1; page <= 100; page++)
{
    string pageUrl = $"https://www.mobile.bg/pcgi/mobile.cgi?act=3&slink=n1pf6i&f1={page}";
    IDocument carPageDocument = await context.OpenAsync(pageUrl);

    List<string> carsUrls = carPageDocument
        .QuerySelectorAll(".valgtop > a")
        .Cast<IHtmlAnchorElement>()
        .Where(x => x.Href != "javascript:;" && x.Href.Contains("adv"))
        .Select(x => x.Href)
        .ToList();

    foreach (var carUrl in carsUrls)
    {
        //Get car details
        IDocument carDocument = await context.OpenAsync(carUrl);

        IElement makeModelElement = carDocument.QuerySelector("h1");
        Console.WriteLine($"Марка/Модел: {makeModelElement.TextContent}");

        IElement carPriceElement = carDocument.QuerySelector("#details_price");
        Console.WriteLine($"Цена: {carPriceElement?.TextContent}");

        IHtmlCollection<IElement> carInfoElements = carDocument.QuerySelectorAll(".dilarData > li");

        for (int i = 0; i < carInfoElements.Length; i += 2)
        {
            string outputInfo = $"{carInfoElements[i].TextContent}: {carInfoElements[i + 1].TextContent}";
            Console.WriteLine(outputInfo);
        }

        Console.WriteLine(new string('-', 60));
    }
}
