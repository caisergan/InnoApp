using InnoApi.Dtos.Users;
using InnoApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InnoApi.Controllers
{
    [Route("InnoApi/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList()
                .Select(u => u.ToUserDto());
            return Ok(users);
        }
        [HttpGet("{ID}")]
        public IActionResult GetById([FromRoute] int ID)
        {
            var user = _context.Users.Find(ID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromCreateDTO();
            _context.Users.Add(userModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = userModel.ID}, userModel.ToUserDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = _context.Users.FirstOrDefault(x => x.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }
            userModel.ADI = updateDto.ADI;
            userModel.SOYADI = updateDto.SOYADI;
            userModel.KULLANICI_ADI = updateDto.KULLANICI_ADI;

            _context.SaveChanges();
            return Ok(userModel);

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var userModel = _context.Users.FirstOrDefault(x =>x.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            _context.SaveChanges();
            return NoContent(); 
        }
    }
}
