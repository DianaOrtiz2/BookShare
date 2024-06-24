using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShare.Models;
using Microsoft.EntityFrameworkCore;
using BookShare.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookShare.Controllers;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BooksController> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BooksController(ApplicationDbContext context, ILogger<BooksController> logger, IWebHostEnvironment hostEnvironment)
    {
        this._logger = logger;
        _context = context;
        _hostEnvironment = hostEnvironment;
        
    }

     
     public async Task<IActionResult> BooksList()
    {
        List<BookModel> books = await _context.Books.Select(book => new BookModel()
        {
            Id = book.Id,
            Titulo = book.Titulo,
            Autor = book.Autor,
            Descripcion = book.Descripcion,
            ImagePath = book.ImagePath,
            DocumentPath = book.DocumentPath
    
        }).ToListAsync();
            return View(books);

    } 

    [HttpGet]

    public IActionResult BookAdd()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookAdd(BookModel model)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/IMG/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            
            string documentFileName = Path.GetFileNameWithoutExtension(model.DocumentFile.FileName);
            string documentExtension = Path.GetExtension(model.DocumentFile.FileName);
            documentFileName = documentFileName + DateTime.Now.ToString("yymmssfff") + documentExtension;
            string documentPath = Path.Combine(wwwRootPath + "/Documentos/", documentFileName);
            using (var fileStream = new FileStream(documentPath, FileMode.Create))
            {
                await model.DocumentFile.CopyToAsync(fileStream);
            }

            var book = new Book();
            book.Id = new Guid();
            book.Titulo = model.Titulo;
            book.Autor = model.Autor;
            book.Descripcion = model.Descripcion;
            book.ImagePath = "/IMG/"+ fileName;
            book.DocumentPath = "/Documentos/" + documentFileName;

             _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BooksList));
           

        }  
         return View(model);

            /* string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/IMG/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            
            string documentFileName = Path.GetFileNameWithoutExtension(model.DocumentFile.FileName);
            string documentExtension = Path.GetExtension(model.DocumentFile.FileName);
            documentFileName = documentFileName + DateTime.Now.ToString("yymmssfff") + documentExtension;
            string documentPath = Path.Combine(wwwRootPath + "/Documentos/", documentFileName);
            using (var fileStream = new FileStream(documentPath, FileMode.Create))
            {
                await model.DocumentFile.CopyToAsync(fileStream);
            }

            var book = new Book();
            book.Id = new Guid();
            book.Titulo = model.Titulo;
            book.Autor = model.Autor;
            book.Descripcion = model.Descripcion;
            book.ImagePath = "/IMG/"+ fileName;
            book.DocumentPath = "/Documentos/" + documentFileName;

             _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BooksList)); */
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookDeleted(Guid Id)
    {
        var book = await _context.Books.FindAsync(Id);
        if (book != null)
        {
            
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath, book.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(BooksList));
    }

    
   

    
   
     /* public async Task<IActionResult> BookDeleted(BookModel bookDeleted)
     {
        bool exits = await this._context.Books.AnyAsync(b => b.Id == bookDeleted.Id);
        if(!exits){
            _logger.LogError("No existe");
            return View(bookDeleted);
        }
         Book book = await this._context.Books.Where(b => b.Id == bookDeleted.Id).FindAsync();
         this._context.Books.Remove(book);
         await this._context.SaveChangesAsync();
        return RedirectToAction(nameof(BooksList));
     } */


        
}

