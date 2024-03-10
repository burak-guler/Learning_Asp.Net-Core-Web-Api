using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")] int id) 
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book is null) 
                    return NotFound(); //404
                return Ok(book);    
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)   
                    return BadRequest(); //400
                _context.Add(book);
                _context.SaveChanges();
                return StatusCode(201,book);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.Find(id);

                // check book
                if (entity is null)
                    return NotFound(); //404

                //check id
                if (id!=book.Id)
                    return BadRequest(); //400

                entity.Title= book.Title;
                entity.Price= book.Price; 
                _context.SaveChanges();

                return Ok(book);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")] int id)
        {
            try
            {               
                var book = _context.Books.Find(id); 
               

                if (book is null)
                    return NotFound(new {
                        StatusCode = 404,
                        messagge=$"Book with id:{id} could found."
                    }); //404

                _context.Books.Remove(book);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartialAllyUpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //check entity 
                var entity = _context.Books.Find(id);
                if (entity is null) return NotFound();

                bookPatch.ApplyTo(entity);  
                _context.SaveChanges();
                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
