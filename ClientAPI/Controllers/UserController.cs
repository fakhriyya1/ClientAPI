using ClientAPI.DTOs;
using ClientAPI.Mappers;
using ClientAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClientAPI.Controllers
{
    [Route("api/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users= _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _context.Users.Find(id);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserDto userDto)
        {
            var user=userDto.ToUser();

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id=user.Id}, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody]UserDto userDto)
        {
            var user=_context.Users.FirstOrDefault(x=>x.Id==id);

            if(user==null)
                return NotFound();

            _context.Entry(user).CurrentValues.SetValues(userDto);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user=_context.Users.FirstOrDefault(x=>x.Id == id);

            if(user==null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
