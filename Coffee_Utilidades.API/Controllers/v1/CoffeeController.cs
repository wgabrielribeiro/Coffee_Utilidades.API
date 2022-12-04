using Microsoft.AspNetCore.Mvc;

namespace Coffee_Utilidades.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]    
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Coffee : Controller
    {
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
            return Ok("Teste realizado com sucesso!!!");
        }

        /// <summary>
        /// Verifica se a API 1.1 está funcionando
        /// </summary>
        /// <param name="Index_1.1"></param>
        /// <returns></returns>
        /// 
        [MapToApiVersion("1.1")]
        [HttpGet]
        [Route("Index_1.1")]
        public IActionResult Indexx()
        {
            return Ok("Teste realizado com sucesso!!!");
        }
    }
}
