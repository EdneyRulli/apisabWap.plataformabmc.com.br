using ApiSabWap.Model;
using ApiSabWap.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiSabWap.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SubAtividadesController : BaseController
    {
        private readonly SubAtividadesServices _subactivityService;

        public SubAtividadesController(SubAtividadesServices subactivityService)
        {
            _subactivityService = subactivityService;
        }


        /// <summary>
        /// O retorno serão todos os dados da atividade princiapal com todas as subatividades e as perguntas de cada subatividade. Se passar o parametro id, vai retornar apenas essa SUB.
        /// </summary>
        /// <param name="id">Código da SubAtividade.</param>
        /// <param name="idAtiv">Código da atividade principal.</param>
        /// <param name="prChave">Chave de acesso.</param>
        /// <returns>O retorno serão todos os dados da atividade princiapal com todas as subatividades e as perguntas de cada subatividade. Se passar o parametro id, vai retornar apenas essa SUB.</returns>

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult Detalhes(string id = "", string idAtiv = "", string prChave = "")
        {
            if (!VerificaToken(prChave))
                return Unauthorized();

            if (string.IsNullOrEmpty(idAtiv) && string.IsNullOrEmpty(id))
                return NoContent();


            List<SubAtividades> Resultado = _subactivityService.ConsultarSubAtividade(id, idAtiv);


            if (Resultado == null)
                return NoContent();

            if (Resultado == null || !Resultado.Any())
                return StatusCode(500, "[Erro: Erro na consulta]");


            return Ok(Resultado);
        }


    }
}
