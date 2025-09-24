using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaList.Data;
using PizzaList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaList.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly PizzaStoreContext _db;

        public PizzaController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetPizzas()
        {
            return (await _db.SetPizza.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
        }
    }
}
