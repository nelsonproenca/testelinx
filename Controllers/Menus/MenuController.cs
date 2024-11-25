using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Menus.Application.Menu.Models;
using RVC.Intranet4.Menus.Application.Menu.Queries;

namespace RVC.Intranet4.Web.Controllers.Menus
{
    /// <summary>
    /// Menu Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuQueries menuQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuController"/> class.
        /// </summary>
        /// <param name="menuQueries">menuQueries interface.</param>
        public MenuController(IMenuQueries menuQueries)
        {
            this.menuQueries = menuQueries;
        }

        /// <summary>
        /// GetAsync método BuscarTodos.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<MenuController>
        [HttpGet]
        
        public async Task<IActionResult> GetAsync()
        {
            var menus = await menuQueries.BuscarTodosAsync();

            if (menus == null)
            {
                return NotFound();
            }

            return Ok(menus);
        }

        /// <summary>
        /// GetAsync método BuscarTodos com paginação.
        /// </summary>
        /// <param name="codigoMenuPai">codigoMenuPai.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<MenuController>
        [HttpGet("menuitens/{codigoMenuPai}")]
        
        public async Task<IActionResult> GetTodosMenuItensAsync(int codigoMenuPai = 0)
        {
            var menus = await menuQueries.BuscarTodosMenuItensAsync(codigoMenuPai);

            if (menus == null)
            {
                return NotFound();
            }

            return Ok(menus);
        }

        /// <summary>
        /// GetAsync método BuscarUm por id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetAsync(int id)
        {
            var menu = await menuQueries.BuscarUmAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }
    }
}
