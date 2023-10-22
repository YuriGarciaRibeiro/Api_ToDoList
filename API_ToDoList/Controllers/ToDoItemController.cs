using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Context;

namespace ToDoListApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            var itens = _context.ToDoItens.ToList();
            return Ok(itens);
        }


        [HttpGet("{id:int}", Name = "obterToDoItem")]
        public ActionResult<ToDoItem> Get(int id)
        {
            return _context.ToDoItens.FirstOrDefault(Tdi => Tdi.ToDoItemId == id) is ToDoItem item ? Ok(item) : NotFound();
        }

        [HttpGet("usuario/{id:int}")]
        public ActionResult<IEnumerable<ToDoItem>> GetPerUser(int id)
        {

            var itens = _context.ToDoItens.Where(Tdi => Tdi.userId == id).ToList();
            return  Ok(itens);
        }
        

        [HttpPost]
        public ActionResult Post(ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

    

            // Verifique se o userId existe na tabela de usuários
            var userExists = _context.Users.Any(u => u.userId == item.userId);
            if (!userExists)
            {
                return BadRequest("UserId inválido.");
            }


            _context.ToDoItens.Add(item);
            _context.SaveChanges();

            return new CreatedAtRouteResult("obterToDoItem", new { id = item.ToDoItemId }, item);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,ToDoItem item)
        {
            if (id != item.ToDoItemId) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(item);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var item = _context.ToDoItens.FirstOrDefault(Tdi => Tdi.ToDoItemId == id);
            if (item == null)
            {
                return BadRequest();
            }

            _context.ToDoItens.Remove(item);
            _context.SaveChanges();
            return Ok(item);
        }



    }



    
}
