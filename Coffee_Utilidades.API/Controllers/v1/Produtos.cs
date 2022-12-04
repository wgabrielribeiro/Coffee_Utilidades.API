using Coffee_Utilidades.Core.Entity.Request;
using Coffee_Utilidades.Core.Interface;
using Coffee_Utilidades.Core.Service;
using Coffee_Utilidades.DataModel.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Coffee_Utilidades.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Produtos : Controller
    {
        private readonly ProdutoService _produtoService;

        public Produtos(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Verifica se a API 1.0 está funcionando
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        /// 
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return Ok("blz");
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("InserirProduto")]
        public async Task<IActionResult> InserirProduto([FromBody] ProdutosCadastrados produto)
        {
            try
            {
                var unid_Produto = await _produtoService.AddProduto(produto);

                if (unid_Produto == null)
                    return BadRequest("Erro ao tentar cadastrar produto.");

                return Ok(unid_Produto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ProdutoQuente")]
        public IActionResult ProdutoQuente()
        {
            try
            {
                var result = _produtoService.GetProdutosByCodigo("quente");

                if(result == null) return NotFound("O produto não foi localizado");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[EnableCors("_myAllowSpecificOrigins")]
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ProdutoFrio")]
        public IActionResult ProdutoFrio()
        {
            try
            {
                var result = _produtoService.GetProdutosByCodigo("frio");

                if (result.ToString().Contains("Nenhum registro encontrado.")) 
                    return NotFound("Não há produtos");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }        
    }
}
