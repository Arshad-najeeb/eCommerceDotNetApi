using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceNet.Models;

namespace ECommerceNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Comments1Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Comments1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments1
        [HttpGet]
        public IEnumerable<Comments> GetComments()
        {
            return _context.Comments;
        }

        // GET: api/Comments1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _context.Comments.FindAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        // PUT: api/Comments1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments([FromRoute] int id, [FromBody] Comments comments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comments.UserId)
            {
                return BadRequest();
            }

            _context.Entry(comments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments1
        [HttpPost]
        public async Task<ActionResult<CommentResponse>> PostComments([FromBody] Comments comments)
        {
            var resp = new CommentResponse { Success = "false" };
            try
            {
                var purchase=_context.PurchaseHistory.FirstOrDefault(oh => oh.UserId == comments.UserId);
                if (purchase== null)
                {
                    resp.Success = "false";
                    resp.Response = "User don't have access to comment";
                }
                else
                {
                    _context.Comments.Add(comments);
                    resp.Success = "true";
                    resp.Response = "Comment successfully added";
                    
                }
            }
            
            catch
            {
                resp.Success = "false";
                resp.Response = "User don't had access to add Comment";
            }
            return resp;
        }

        // DELETE: api/Comments1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();

            return Ok(comments);
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.UserId == id);
        }
        public class CommentResponse
        {
            public string Response { get; set; }
            public string Success { get; set; }
        }
    }
}