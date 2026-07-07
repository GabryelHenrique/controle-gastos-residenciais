using ControleGastos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ControleGastosDbContext _context;

    public TransacoesController(ControleGastosDbContext context)
    {
        _context = context;
    
    }
}
