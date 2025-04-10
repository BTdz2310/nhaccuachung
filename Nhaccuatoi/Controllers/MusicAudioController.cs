using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nhaccuatoi.Data;
using Nhaccuatoi.Models;

namespace Nhaccuatoi.Controllers;
[Route("MusicAudio")]

public class MusicAudioController : Controller
{
    private readonly ILogger<MusicAudioController> _logger;
    private ApplicationDbContext DbContext { get; }

    public MusicAudioController(ApplicationDbContext context, ILogger<MusicAudioController> logger)
    {
        DbContext = context;
        _logger = logger;
    }

    [Route("song/{id}")]
    public IActionResult MusicAudio(int id)
    {
        Console.WriteLine("id: " + id);
        var song = DbContext.Find<SongModel>(id);
        return Ok(song);
        // return PartialView("_MusicAudio", );
    }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public interface IActionResult<T>
{
}