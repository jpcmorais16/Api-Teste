using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Repositories;
using AutoMapper;
using WebApplication1.DTOs;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        IMapper _mapper;
        public CategoriasController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {

            _uow.CategoriaRepository.add(categoria);
            await _uow.Commit();

            return Ok(categoria);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var categoria = await _uow.CategoriaRepository.Get()
                .Include(c=>c.Produtos)
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        
            if(categoria != null)
            {
                return Ok(categoria);
            }
            return NotFound();

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var categorias = await _uow.CategoriaRepository.Get()
                 .Include(p => p.Produtos).ToListAsync();

          
            return Ok(categorias);


            /*return _context.Categorias.Include(c => c.Produtos)
                .AsNoTracking().ToList();*/

        }
        [HttpGet("dto")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetDTO()
        {
            var categorias = await _uow.CategoriaRepository.Get()
                 .Include(p => p.Produtos).ToListAsync();

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

            return Ok(categoriasDto);


            /*return _context.Categorias.Include(c => c.Produtos)
                .AsNoTracking().ToList();*/

        }

        [HttpDelete]
        public async Task<ActionResult<Categoria>> Delete([FromBody] Categoria categoria)
        {
            var id = categoria.CategoriaId;
            var nome = categoria.Nome == null ? categoria.Nome : "";

            var cat = await _uow.CategoriaRepository
                .GetById(c => c.CategoriaId == categoria.CategoriaId);

            if(cat == null)
            {
                cat = await _uow.CategoriaRepository
                    .GetByNome(c => c.Nome == categoria.Nome);
            }


            if(cat == null)
                return NotFound("Categoria não encontrada");

            _uow.CategoriaRepository.delete(cat);
            await _uow.Commit();
            return Ok(cat);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {

            if (id != categoria.CategoriaId)
            {
                return BadRequest("Os ids não batem");
            }

             _uow.CategoriaRepository.update(categoria);
            await _uow.Commit();
            return Ok(categoria);
        }




    }
}
