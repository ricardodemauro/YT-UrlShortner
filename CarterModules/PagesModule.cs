using Carter;
using Carter.Request;
using LiteDB;
using UrlShortnerVideo.Models;

namespace UrlShortnerVideo.CarterModules
{
    public class PagesModule : CarterModule
    {
        public PagesModule(ILiteDatabase db)
        {
            Get("/", async (req, res) =>
            {
                res.ContentType = "text/html";
                res.StatusCode = 200;
                await res.SendFileAsync("wwwroot/index.html");
            });

            //Expected Route: http://localhost:5000/{chunck}
            Get("/{chunck}", (req, res) =>
            {
                var chunck = req.RouteValues.As<string>("chunck");

                var shortUrl = db.GetCollection<ShortUrl>().FindOne(x => x.Chunck == chunck);

                if (shortUrl == null) res.Redirect("/");
                else res.Redirect(shortUrl.Url);

                return Task.CompletedTask;
            });
        }
    }
}
