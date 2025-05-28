namespace ApiSabWap.Model
{
    public class RespostaFormularioRequest
    {
        public int IdPergunta { get; set; }
        public int IdSub { get; set; }
        public string Resposta { get; set; }
        public string Comentario { get; set; }
        public string Chave { get; set; }
    }

    public class RespostaFormularioRequestLista
    {
        public int IdPergunta { get; set; }
        public int IdSub { get; set; }
        public string Resposta { get; set; }
        public string Comentario { get; set; }
    }

    public class RespostaLista
    {
        public string Chave { get; set; }
        public List<RespostaFormularioRequestLista> ListaRespostas { get; set; }
    }

    public class GravarFoto
    {
        public int idFormulario { get; set; }
        public int IdSubatividade { get; set; }
        public int id_Tecnico { get; set; }
        public string ImagemBase64 { get; set; }
        public string Chave { get; set; }
    }

    public class RelatorioResposta
    {
        public string Tecnico { get; set; }
        public string TipoRelatorio { get; set; }
        public string Pergunta { get; set; }
        public string Id_Formulario { get; set; }
        public string Resposta { get; set; }
        public string Imagem { get; set; }
        public string Imagem2 { get; set; }
        public string Imagem3 { get; set; }
        public string Imagem4 { get; set; }
        public string Video { get; set; }
        public string Obs { get; set; }
    }

    

}
