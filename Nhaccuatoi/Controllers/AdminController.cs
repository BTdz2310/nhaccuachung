using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nhaccuatoi.Data;
using Nhaccuatoi.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nhaccuatoi.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ApplicationDbContext _context;

    public AdminController(ILogger<AdminController> logger, IWebHostEnvironment environment, ApplicationDbContext context)
    {
        _logger = logger;
        _environment = environment;
        _context = context;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Songs()
    {
        var songs = await _context.Songs
        .Include(s => s.Album)
        .Include(s => s.Genre)
        .ToListAsync();
        return View(songs);
    }

    [HttpPost]
    public async Task<IActionResult> UploadSong(UploadSongModel model)
    {
        if (!ModelState.IsValid)
            // return View(model);
            return BadRequest(ModelState);

        var uniqueId = Guid.NewGuid().ToString();

        // 1. Xử lý lưu file ảnh
        // string imageFileName = Path.GetFileName(model.ImageFile.FileName);
        // string imagePath = Path.Combine("images", imageFileName);
        // string imageSavePath = Path.Combine(_environment.WebRootPath, imagePath);
        string imageExtension = Path.GetExtension(model.ImageFile.FileName);
        string imageFileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
        string newImageFileName = $"{imageFileName}_{uniqueId}{imageExtension}";
        string imagePath = Path.Combine("images", newImageFileName);
        string imageSavePath = Path.Combine(_environment.WebRootPath, imagePath);

        using (var stream = new FileStream(imageSavePath, FileMode.Create))
        {
            await model.ImageFile.CopyToAsync(stream);
        }

        // 2. Xử lý lưu file mp3
        // string audioFileName = Path.GetFileName(model.AudioFile.FileName);
        // string audioPath = Path.Combine("mp3", audioFileName);
        // string audioSavePath = Path.Combine(_environment.WebRootPath, audioPath);
        string audioExtension = Path.GetExtension(model.AudioFile.FileName);
        string audioFileName = Path.GetFileNameWithoutExtension(model.AudioFile.FileName);
        string newAudioFileName = $"{audioFileName}_{uniqueId}{audioExtension}";
        string audioPath = Path.Combine("mp3", newAudioFileName);
        string audioSavePath = Path.Combine(_environment.WebRootPath, audioPath);

        using (var stream = new FileStream(audioSavePath, FileMode.Create))
        {
            await model.AudioFile.CopyToAsync(stream);
        }

        // 3. Tạo đối tượng SongModel và lưu vào DB
        var song = new SongModel
        {
            Name = model.Name,
            Singer = model.Singer,
            Description = model.Description ?? "",
            ReleasedYear = model.ReleasedYear,
            Image = "/" + imagePath.Replace("\\", "/"),
            Source = "/" + audioPath.Replace("\\", "/")
        };

        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        var allSongs = await _context.Songs.ToListAsync();

        TempData["Success"] = "🎉 Bài hát đã được tải lên thành công!";
        return Json(allSongs);
    }

    [HttpPut("/Admin/UploadSong/{id}")]
    public async Task<IActionResult> UploadSong(int id, [FromBody] UpdateSongModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Cập nhật bài hát trong database
        var song = _context.Songs.FirstOrDefault(s => s.Id == id);
        if (song == null) return NotFound();

        song.Name = model.Name;
        song.Singer = model.Singer;
        song.ReleasedYear = model.ReleasedYear;
        song.Description = model.Description;

        _context.SaveChanges();

        var allSongs = await _context.Songs.ToListAsync();

        return Json(allSongs);
    }

    [HttpDelete("/Admin/UploadSong/{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var song = _context.Songs.FirstOrDefault(s => s.Id == id);
        if (song == null)
        {
            return NotFound();
        }

        _context.Songs.Remove(song);
        _context.SaveChanges();

        var allSongs = await _context.Songs.ToListAsync();

        return Json(allSongs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
