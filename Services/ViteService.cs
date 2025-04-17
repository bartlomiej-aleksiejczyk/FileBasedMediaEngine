using System.Text.Json; 
using Microsoft.AspNetCore.Html;

namespace SimpleMediaCenter.Services;
public class ViteService
{
    private readonly IConfiguration _configuration;

    public ViteService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IHtmlContent LoadViteAssets(bool isDebug, string staticUrl, string clientComponentsPath, string manifestPath)
    {
        var tags = new List<string>();

        // **If in Development Mode, inject Vite Dev Server links**
        if (isDebug)
        {
            tags.Add($"<script type=\"module\" src=\"http://localhost:5173/@vite/client\"></script>");
            tags.Add($"<script type=\"module\" src=\"http://localhost:5173/{clientComponentsPath}main.js\"></script>");
        }
        else
        {
            // **In Production Mode, load from manifest.json**
            if (!File.Exists(manifestPath))
            {
                throw new Exception($"Vite manifest.json not found at {manifestPath}");
            }

            var manifest = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(manifestPath));

            // **Extract JS & CSS files from the manifest**
            var jsFiles = manifest.Where(kvp => kvp.Key.EndsWith(".js")).ToList();
            var cssFiles = manifest.Where(kvp => kvp.Key.EndsWith(".css")).ToList();

            foreach (var file in jsFiles)
            {
                tags.Add($"<script type=\"module\" src=\"{staticUrl}{file.Value}\"></script>");
            }

            foreach (var file in cssFiles)
            {
                tags.Add($"<link rel=\"stylesheet\" href=\"{staticUrl}{file.Value}\">");
            }
        }

        return new HtmlString(string.Join("\n", tags));
    }
}
