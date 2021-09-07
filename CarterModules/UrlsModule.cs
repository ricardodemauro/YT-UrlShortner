using Carter;
using Carter.ModelBinding;
using Carter.Request;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerVideo.Models;

namespace UrlShortnerVideo.CarterModules
{
    public class UrlsModule : CarterModule
    {
        public UrlsModule(ILiteDatabase db) : base("/urls")
        {
            //JSON para criar a url curta
            /*
             * { url: "https://google.com" }
             * */
            Post("/", async (req, res) =>
            {
                var shortUrl = await req.Bind<ShortUrl>();

                if (Uri.TryCreate(shortUrl.Url, UriKind.Absolute, out var uriParsed))
                {
                    shortUrl.Chunck = Nanoid.Nanoid.Generate(size: 9);

                    db.GetCollection<ShortUrl>(BsonAutoId.Guid).Insert(shortUrl);

                    res.StatusCode = (int)HttpStatusCode.OK;
                    var rawShortUrl = $"{req.Scheme}://{req.Host}/{shortUrl.Chunck}";
                    await res.WriteAsJsonAsync(new { ShortUrl = rawShortUrl });
                }
                else
                {
                    res.StatusCode = (int)HttpStatusCode.BadRequest;
                    await res.WriteAsJsonAsync(new { ErrorMessage = "Invalid Url" });
                }
            });
        }
    }
}
