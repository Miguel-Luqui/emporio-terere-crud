using Xunit;
using EmporioTerere.Api.Controllers;
using EmporioTerere.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmporioTerere.Tests
{
    public class ProdutoControllerTests
    {
        private ProdutoController _controller;

        public ProdutoControllerTests()
        {
            // Criamos uma nova instância para cada teste
            _controller = new ProdutoController();
        }

        [Fact]
        public void GetAll_DeveRetornarListaVazia_QuandoNaoHaProdutos()
        {
            var resultado = _controller.GetAll() as OkObjectResult;

            Assert.NotNull(resultado);
            var produtos = resultado.Value as List<Produto>;
            Assert.Empty(produtos);
        }

        [Fact]
        public void Create_DeveAdicionarProdutoERetornarCreated()
        {
            var novoProduto = new Produto { Nome = "Erva Mate", Preco = 10.5m, Estoque = 20 };

            var resultado = _controller.Create(novoProduto) as CreatedAtActionResult;

            Assert.NotNull(resultado);
            var produtoCriado = resultado.Value as Produto;
            Assert.NotNull(produtoCriado);
            Assert.Equal("Erva Mate", produtoCriado.Nome);
            Assert.Equal(1, produtoCriado.Id);
        }

        [Fact]
        public void GetById_DeveRetornarProduto_SeExistir()
        {
            var novoProduto = new Produto { Nome = "Tereré Natural", Preco = 15.0m, Estoque = 50 };
            _controller.Create(novoProduto);

            var resultado = _controller.GetById(1) as OkObjectResult;

            Assert.NotNull(resultado);
            var produto = resultado.Value as Produto;
            Assert.Equal("Tereré Natural", produto.Nome);
        }

        [Fact]
        public void Update_DeveAtualizarProdutoExistente()
        {
            var produto = new Produto { Nome = "Tereré Menta", Preco = 12.0m, Estoque = 10 };
            _controller.Create(produto);

            var atualizado = new Produto { Nome = "Tereré Menta Gelada", Preco = 13.0m, Estoque = 15 };
            var resultado = _controller.Update(1, atualizado);

            Assert.IsType<NoContentResult>(resultado);

            var resultadoGet = _controller.GetById(1) as OkObjectResult;
            var produtoAtualizado = resultadoGet.Value as Produto;
            Assert.Equal("Tereré Menta Gelada", produtoAtualizado.Nome);
        }

        [Fact]
        public void Delete_DeveRemoverProdutoExistente()
        {
            var produto = new Produto { Nome = "Tereré Abacaxi", Preco = 20.0m, Estoque = 5 };
            _controller.Create(produto);

            var resultado = _controller.Delete(1);

            Assert.IsType<NoContentResult>(resultado);

            var resultadoGet = _controller.GetById(1);
            Assert.IsType<NotFoundResult>(resultadoGet);
        }
    }
}
