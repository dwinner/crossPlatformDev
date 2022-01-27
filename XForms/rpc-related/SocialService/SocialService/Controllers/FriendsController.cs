using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialService.Models;

namespace SocialService.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class FriendsController : ControllerBase
   {
      private readonly ApplicationContext _context = new ApplicationContext();

      [HttpGet]
      public IEnumerable<Friend> Get() => _context.Friends.ToList();

      [HttpGet("{friendId}")]
      public Friend Get(int friendId) => _context.Friends.Find(friendId);

      [HttpPost]
      public async Task<IActionResult> Post([FromBody] Friend friend)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         _context.Friends.Add(friend);
         await _context.SaveChangesAsync().ConfigureAwait(false);

         return Ok(friend);
      }

      [HttpPut]
      public async Task<IActionResult> Put([FromBody] Friend friend)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         _context.Entry(friend).State = EntityState.Modified;
         await _context.SaveChangesAsync().ConfigureAwait(false);

         return Ok(friend);
      }

      [HttpDelete("{friendId}")]
      public async Task<IActionResult> Delete(int friendId)
      {
         var friend = await _context.Friends.FindAsync(friendId).ConfigureAwait(false);
         if (friend != null)
         {
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(friend);
         }

         return NotFound();
      }
   }
}