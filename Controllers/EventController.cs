using BookShare.Entities;
using BookShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShare.Controllers;

public class EventController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EventController> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventController(ApplicationDbContext context, ILogger<EventController> logger, IWebHostEnvironment hostEnvironment)
    {
        this._logger = logger;
        _context = context;
        _hostEnvironment = hostEnvironment;
        
    }

    public async Task<IActionResult> EventList()
    {
        List<EventModel> events = await _context.Events.Select(evento => new EventModel()
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descripcion = evento.Descripcion,
            Date = evento.Date,
            Location = evento.Location,
            ImagePath = evento.ImagePath,
            
    
        }).ToListAsync();
            return View(events);

    } 

    [HttpGet]
    public IActionResult EventAdd()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EventAdd(EventModel model)
    {
        if (ModelState.IsValid)
        {
            return View(model);
        }
        string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/IMG/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            var evento = new Event
            {
                Id = Guid.NewGuid(),
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                Location = model.Location,
                Date = model.Date,
                ImagePath = "/IMG/" + fileName
            };

            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EventList));
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EventDeleted(Guid Id)
    {
        var events = await _context.Events.FindAsync(Id);
        if (events != null)
        {
            
            if (!string.IsNullOrEmpty(events.ImagePath))
            {
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath,events.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(EventList));
    }



}