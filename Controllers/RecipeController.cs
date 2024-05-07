using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeDB.Data;
using RecipeDB.Models;

namespace RecipeDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RecipesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RecipesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }


        // GET: api/recipes/1
        [HttpGet("recipeId/{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeByRecipeId(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }


        // GET: api/recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.RecipeId }, recipe);
        }

        // PUT: api/recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // DELETE: api/recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }
    }
}
