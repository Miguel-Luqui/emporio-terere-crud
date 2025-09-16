using Xunit;
using EmporioTerere.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmporioTerere.Tests
{
    public class ProdutoControllerTests
    {
        private ProdutoController _controller;

        public ProdutoController Controller { get => _controller; set => _controller = value; }

        public ProdutoControllerTests()
        {
            _controller = new ProdutoController();

            // Limpar a lista estática antes de cada teste
            var produtosField = typeof(ProdutoController)
                .GetField("produtos", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            produtosField?.SetValue(null, new List<Produto>());
        }

        [Fact]
        public void Create_ProdutoValido_RetornaCreated()
        {
            var produto = new Produto { Nome = "Erva Mate", Preco = 15.50m, Estoque = 10 };

            var result = _controller.Create(produto) as CreatedAtActionResult;

            Assert.NotNull(result);
            var createdProduto = result.Value as Produto;
            Assert.Equal("Erva Mate", createdProduto.Nome);
            Assert.Equal(15.50m, createdProduto.Preco);
            Assert.Equal(10, createdProduto.Estoque);
        }

        [Fact]
        public void GetAll_ProdutosExistentes_RetornaLista()
        {
            _controller.Create(new Produto { Nome = "Erva", Preco = 10, Estoque = 5 });
            _controller.Create(new Produto { Nome = "Cuia", Preco = 20, Estoque = 3 });

            var result = _controller.GetAll() as OkObjectResult;
            var produtos = result.Value as List<Produto>;

            Assert.NotNull(produtos);
            Assert.Equal(2, produtos.Count);
        }

        [Fact]
        public void GetById_ProdutoExistente_RetornaProduto()
        {
            _controller.Create(new Produto { Nome = "Erva", Preco = 10, Estoque = 5 });

            var result = _controller.GetById(1) as OkObjectResult;
            var produto = result.Value as Produto;

            Assert.NotNull(produto);
            Assert.Equal(1, produto.Id);
            Assert.Equal("Erva", produto.Nome);
        }

        [Fact]
        public void Update_ProdutoExistente_AtualizaValores()
        {
            _controller.Create(new Produto { Nome = "Erva", Preco = 10, Estoque = 5 });

            var produtoAtualizado = new Produto { Nome = "Erva Premium", Preco = 20, Estoque = 15 };
            var result = _controller.Update(1, produtoAtualizado);

            Assert.IsType<NoContentResult>(result);

            var updated = (_controller.GetById(1) as OkObjectResult).Value as Produto;
            Assert.Equal("Erva Premium", updated.Nome);
            Assert.Equal(20, updated.Preco);
            Assert.Equal(15, updated.Estoque);
        }

        [Fact]
        public void Delete_ProdutoExistente_RemoveProduto()
        {
            _controller.Create(new Produto { Nome = "Erva", Preco = 10, Estoque = 5 });

            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);

            var getResult = _controller.GetById(1) as NotFoundResult;
            Assert.NotNull(getResult);
        }
    }
}
