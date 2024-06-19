using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShare.Models;
using Microsoft.EntityFrameworkCore;
using BookShare.Entities;

namespace BookShare.Controllers;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
     public async Task<IActionResult> BooksList()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

    [HttpGet]

    public IActionResult BookAdd()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Books/BookSave")]
    public async Task<IActionResult> BookSave(BookModel bookModel, IFormFile Image)
    {
        if (ModelState.IsValid)
        {
            var book = new Book
        {
            Titulo = bookModel.Titulo,
            Autor = bookModel.Autor,
            Descripcion = bookModel.Descripcion
        };
            try{
            if (Image != null && Image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Image.CopyToAsync(ms);
                    book.Image = ms.ToArray();
                }
            }

            _context.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BooksList));

            }catch (Exception ex){
                 Debug.WriteLine("Error al guardar el libro: " + ex.Message);
            ModelState.AddModelError("", "Error al guardar el libro.");
            return View("BookAdd", bookModel);
            }
        }
        return View("BookAdd", bookModel);
    }

}