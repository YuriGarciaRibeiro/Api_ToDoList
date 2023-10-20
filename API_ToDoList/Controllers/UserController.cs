using DefaultNamespace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Context;

namespace ToDoListApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            return users.Any() ? Ok(users) : NotFound();
        }

        [HttpGet("{id:int}", Name = "obterUser")]
        public ActionResult<User> Get(int id)
        {
            return _context.Users.FirstOrDefault(user => user.userId == id) is User user ? Ok(user) : NotFound();
        }

        [HttpGet("itens")]
        public ActionResult<IEnumerable<User>> GetUserItens()
        {
            var users = _context.Users.Include(p=> p.todo_items).ToList();
            return users.Any() ? Ok(users) : NotFound();
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            return new CreatedAtRouteResult("obterUser", new { id = user.userId }, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, User user)
        {
            if (id != user.userId) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(user => user.userId == id);
            if (user == null)
            {
                return BadRequest();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);

        }


    }
}
