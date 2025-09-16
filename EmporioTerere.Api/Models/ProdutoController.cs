using Microsoft.AspNetCore.Mvc;

namespace EmporioTerere.Api.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> produtos = [];

        public static List<Produto> Produtos { get => produtos; set => produtos = value; }

        [HttpGet]
        public IActionResult GetAll() => Ok(produtos);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            return produto == null ? NotFound() : Ok(produto);
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            produto.Id = produtos.Count + 1;
            produtos.Add(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Produto produtoAtualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            produtos.Remove(produto);
            return NoContent();
        }
    }
}
