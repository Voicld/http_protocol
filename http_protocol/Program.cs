using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

class Program
{
    static async Task Main()
    {
        Console.Write("Enter the width of an image: ");
        string widthInput = Console.ReadLine();
        Console.Write("Enter the height of an image: ");
        string heightInput = Console.ReadLine();

        if (!int.TryParse(widthInput, out int width) || !int.TryParse(heightInput, out int height))
        {
            Console.WriteLine("Invalid size.");
            return;
        }

        string url = GenerateUrl(width, height);

        string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string fileName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
        string fullPath = Path.Combine(picturesPath, fileName);

        Console.WriteLine("Loading an image...");

        try
        {
            await DownloadImageAsync(url, fullPath);
            Console.WriteLine($"An image was saved in: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has been occurred during downloading: {ex.Message}");
        }
    }

    static string GenerateUrl(int width, int height)
    {
        return $"https://loremflickr.com/{width}/{height}";


    }
    static async Task DownloadImageAsync(string url, string destinationPath)
    {
        using HttpClient client = new HttpClient();
        byte[] imageBytes = await client.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(destinationPath, imageBytes);
    }

}
