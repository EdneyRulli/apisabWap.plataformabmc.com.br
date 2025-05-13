using ApiSabWap.Model;
using ApiSabWap.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiSabWap.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AtividadesController : BaseController
    {
        private readonly AtividadesServices _activityService;

        public AtividadesController(AtividadesServices activityService)
        {
            _activityService = activityService;
        }


        /// <summary>
        /// O retorno serão todos os dados da atividade princiapal com todas as subatividades e as perguntas de cada subatividade.
        /// </summary>
        /// <param name="id">Código da atividade principal.</param>
        /// <param name="idSub">Código da SubAtividade.</param>
        /// <param name="prChave">Chave de acesso.</param>
        /// <returns>O retorno serão todos os dados da atividade princiapal com todas as subatividades e as perguntas de cada subatividade.</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult Detalhes(string id = "", string idSub = "", string prChave = "")
        {
            if (!VerificaToken(prChave))
                return Unauthorized();

            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(idSub))
                return NoContent();

            AtividadeModel Resultado = _activityService.ConsultarAtividade(id, idSub);



            if (Resultado == null)
                return NoContent();

            if (Resultado.Id == 0)
                return StatusCode(500, "[Erro: Erro na consulta]");


            return Ok(Resultado);
        }

    }
}
