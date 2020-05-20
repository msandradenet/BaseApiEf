using BaseApiEF.Data;
using BaseApiEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return categories;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return category;
        }
    }
}
