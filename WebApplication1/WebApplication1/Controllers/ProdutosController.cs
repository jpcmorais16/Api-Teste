using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using WebApplication1.Interfaces;
using AutoMapper;
using WebApplication1.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProdutosController : Controller
    {
        
        IUnitOfWork _uow;
        IMapper _mapper;

        public ProdutosController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            var procuraCategoria = await _uow.CategoriaRepository.Get()
                                 .FirstOrDefaultAsync(c => c.Nome == produto.Categoria.Nome);
            
            if(procuraCategoria == null)
            {
                return NotFound("Esta categoria não existe.");
            }

            procuraCategoria.Produtos.Add(produto);
            _uow.ProdutoRepository.add(produto);
            await _uow.Commit();
            return Ok(produto);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _uow.ProdutoRepository.Get()
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.ProdutoId == id) ;
                

            if (produto == null)
                return NotFound("Produto não encontrado");

            return Ok(produto);
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await _uow.ProdutoRepository.Get().Include(p => p.Categoria)
                .AsNoTracking().ToListAsync();

        }

        [HttpGet("dto")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetDTO()
        {
            var produtos = await _uow.ProdutoRepository.Get().Include(p => p.Categoria)
                .AsNoTracking().ToListAsync();//o que acontece deixando esse tolist()?

            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return Ok(produtosDTO);

        }



        [HttpDelete]
        public async Task<ActionResult<Produto>> Delete([FromBody] Produto produto)
        {
            var id = produto.ProdutoId;
            var nome = produto.Nome == null ? produto.Nome : "";

            var prod = _uow.ProdutoRepository.Get()
                .FirstOrDefault(p => p.ProdutoId == id || p.Nome == nome);

            if (prod == null)
                return NotFound("Produto não encontrado");

            _uow.ProdutoRepository.delete(prod);
            await _uow.Commit();
            return Ok(prod);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Produto produto)
        {
            
            if (id != produto.ProdutoId)
            {
                return BadRequest("Os ids não batem");
            }

            _uow.ProdutoRepository.update(produto);
            await _uow.Commit();
            return Ok(produto);
        }



    }
}
