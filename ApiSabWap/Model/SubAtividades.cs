namespace ApiSabWap.Model
{
    public class SubAtividades
    {
        public int ID { get; set; }
        public string TecLocalizacao { get; set; }
        public string TecEndereco { get; set; }
        public DateTime? TecInicioTarefa { get; set; }
        public DateTime? TecFimTarefa { get; set; }
        public string Descricao { get; set; }
        public string Comentarios { get; set; }
        public decimal? ValorMaoObra { get; set; }
        public decimal? QtdHoras { get; set; }
        public decimal? ValorKM { get; set; }
        public decimal? DistanciaKmServico { get; set; }
        public int IdFormulario { get; set; }
        public string Formulario { get; set; }
        public DateTime? DataPrevistaAtendimento { get; set; }
        public DateTime? DataSubAtividade { get; set; }

        public string Cliente_Nome { get; set; }
        public string Cliente_Cnpj { get; set; }

        public string SubAtividade_Status { get; set; }
        public string SubAtividade_Etapa { get; set; }

        public int Atividade_Id { get; set; }
        public string Atividade_Status { get; set; }
        public string Atividade_Etapa { get; set; }

        public int Id_Tecnico { get; set; }
        public int Id_UsuarioEquipe { get; set; }

        public string Equipamento { get; set; }
        public string Equipamento_Modelo { get; set; }
        public string Equipamento_Marca { get; set; }
        public string Equipamento_Status { get; set; }
        public string Equipamento_PlanoMensal { get; set; }
        public string Equipamento_Necessidade { get; set; }

        public List<SubAtividadesPerguntas> PerguntasRelatorio { get; set; }
    }

    public class SubAtividadesPerguntas {
        public int ID { get; set; }
        public int Id_Formulario { get; set; }
        public string NomeRelatorio { get; set; }
        public string Descricao { get; set; }
        public string Ordem { get; set; }
    }

}
