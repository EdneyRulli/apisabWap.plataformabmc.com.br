using ApiSabWap.Model;
using ApiSabWap.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSabWap.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PerguntasRelController : BaseController
    {
        private readonly SubAtividadesServices _subactivityService;

        public PerguntasRelController(SubAtividadesServices subactivityService)
        {
            _subactivityService = subactivityService;
        }


        /// <summary>
        /// Retorna somente as perguntas de um relatorio especifico. Sem os dados da SUB e da ATIV.
        /// </summary>
        /// <param name="id">Código do relatório.</param>
        /// <param name="prChave">Chave de acesso.</param>
        /// <returns>Retorna somente as perguntas de um relatorio especifico. Sem os dados da SUB e da ATIV.</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult Detalhes(string id = "", string prChave = "")
        {
            if (!VerificaToken(prChave))
                return Unauthorized();

            if (string.IsNullOrEmpty(id))
                return NoContent();


            List<SubAtividadesPerguntas> Resultado = _subactivityService.ConsultarPerguntasRelatorio(id);


            if (Resultado == null)
                return NoContent();

            if (Resultado == null || !Resultado.Any())
                return StatusCode(500, "[Erro: Erro na consulta]");


            return Ok(Resultado);
        }


        /// <summary>
        /// Utilizado para gravar respostas das perguntas. Deve retornar TRUE ou mensagem de erro.
        /// </summary>
        /// <returns>Utilizado para gravar respostas das perguntas. Deve retornar TRUE ou mensagem de erro.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("GravarResposta")]
        public ActionResult GravarResposta([FromBody] RespostaFormularioRequest request)
        {
            if (request == null)
                return BadRequest("Requisição vazia");


            if (!VerificaToken(request.Chave))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.IdPergunta.ToString()))
                return NoContent();


            var Resultado = _subactivityService.GravarResposta(request);


            if (!Resultado)
                return StatusCode(500, "[Erro: Erro no processo]");


            return Ok(Resultado);
        }


        /// <summary>
        /// Utilizado para gravar respostas das perguntas. Deve retornar TRUE ou mensagem de erro.
        /// </summary>
        /// <returns>Utilizado para gravar respostas das perguntas. Deve retornar TRUE ou mensagem de erro.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("GravarRespostaLista")]
        public ActionResult GravarRespostaLista([FromBody] RespostaLista request)
        {
            if (request == null)
                return BadRequest("Requisição vazia");


            if (!VerificaToken(request.Chave))
                return BadRequest("Chave inválida");

            if (request.ListaRespostas.Count == 0)
                return BadRequest("Requisição vazia");


            var Resultado = _subactivityService.GravarRespostaLista(request);


            if (!Resultado)
                return StatusCode(500, "[Erro: Erro no processo]");


            return Ok(Resultado);
        }

        /// <summary>
        /// Utilizado para gravar as fotos do atendimento Deve retornar TRUE ou mensagem de erro.
        /// </summary>
        /// <returns>Utilizado para gravar as fotos do atendimento Deve retornar TRUE ou mensagem de erro.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("GravarRespostaFoto")]
        public ActionResult GravarRespostaFoto([FromBody] GravarFoto request)
        {
            if (request == null)
                return BadRequest("Requisição vazia");


            if (!VerificaToken(request.Chave))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.ImagemBase64.ToString()))
                return NoContent();


            var Resultado = _subactivityService.GravarRespostaFotos(request);


            if (!Resultado)
                return StatusCode(500, "[Erro: Erro no processo]");


            return Ok(Resultado);
        }

    }
}
