using Coffee_Utilidades.Core.Entity.Request;
using Coffee_Utilidades.Core.Service;
using Coffee_Utilidades.DataModel.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Coffee_Utilidades.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProdutosCarrinho : Controller
    {
        private readonly ProdutoService _produtoService;
        public ProdutosCarrinho(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("SelecionaProduto")]
        public async Task<IActionResult> SelecionaProduto([FromBody] ProdutosSelecionados selecionado)
        {
            try
            {
                var prodSelecionado = await _produtoService.SelecionaProduto(selecionado);

                if (prodSelecionado == null) return BadRequest("Erro ao inserir no banco de dados");

                return Ok(prodSelecionado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("DeteleProduto")]
        public async Task<IActionResult> DeteleProduto(int Cod_Produto, string usuario)
        {
            try
            {
                var prodDeletado = await _produtoService.DeteleProduto(Cod_Produto, usuario);

                if (prodDeletado == null) return BadRequest("Erro ao deletar no banco de dados");

                return Ok(prodDeletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ObterProdSelecionadoQuente")]

        public async Task<IActionResult> ObterProdSelecionadoQuente(string user)
        {
            try
            {
                var produtos = await _produtoService.ObterProdSelecionado(user, "quente");

                if (produtos == null) return NotFound("Nenhum produto Selecionado!");

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ObterProdSelecionadoFrio")]

        public async Task<IActionResult> ObterProdSelecionadoFrio(string user)
        {
            try
            {
                var produtos = await _produtoService.ObterProdSelecionado(user, "frio");

                if (produtos == null) return NotFound("Nenhum produto Selecionado!");

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ObterProdSelecionado")]

        public async Task<IActionResult> ObterProdSelecionado(string user)
        {
            try
            {
                var produtos = await _produtoService.GetAll(user);

                if (produtos == null) return NotFound("Nenhum produto Selecionado!");

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("CalculoCarrinho")]

        public IActionResult CalculoCarrinho(string user)
        {
            try
            {
                int resultado = _produtoService.CalculoCarrinho(user);

                if(resultado == 0)
                {
                    return NotFound(0);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("FormadePagamento")]
        public async Task<IActionResult> formaDePagamento([FromBody] FormaPagamento formaPagamento)
        {
            try
            {
                bool inserido = await _produtoService.InsereFormaDePagamento(formaPagamento);

                if (!inserido)
                    return BadRequest("Cartão já existe!");

                return Ok("Inserido com sucesso!");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("DeleteprodutosComprados")]
        public async Task<IActionResult> DeleteprodutosComprados([FromBody] ProdutosComprados[] comprados)
        {
            try
            {
                var produtos = await _produtoService.HistoricoPedidos(comprados);

                if(produtos == null)
                {
                    return NotFound("Você ainda não possui pedidos");
                }

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("produtosComprados")]
        public IActionResult produtosComprados(string usuario)
        {
            try
            {
                var produtos =  _produtoService.MostrarHistorico(usuario);

                if (produtos == null)
                {
                    return NotFound("Você ainda não possui pedidos");
                }

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
