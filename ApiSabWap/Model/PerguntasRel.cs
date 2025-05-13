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

    public class GravarFoto
    {
        public int idFormulario { get; set; }
        public int IdSubatividade { get; set; }
        public int id_Tecnico { get; set; }
        public string ImagemBase64 { get; set; }
        public string Chave { get; set; }
    }

    

}
