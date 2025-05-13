using Dapper;
using ApiSabWap.Data;
using ApiSabWap.Model;
using System.Data;

namespace ApiSabWap.Services
{
    public class AtividadesServices
    {
        private readonly Connections conn = new Connections();
        private readonly SubAtividadesServices _subactivityService;

        public AtividadesServices(SubAtividadesServices subactivityService)
        {
            _subactivityService = subactivityService;
        }

        public AtividadeModel ConsultarAtividade(string prId, string prIdSub = "")
        {
            AtividadeModel resultado = new AtividadeModel();
            try
            {
                var parammeters = new { 
                        PrId = prId
                        ,PrId_Sub = prIdSub
                };

                resultado = conn.sqlConnection.Query<AtividadeModel>("Sp_ConsultaAtividadesPorSub", parammeters, commandType: CommandType.StoredProcedure).FirstOrDefault();


                // PREENCHE A LISTA DAS SUB
                List<SubAtividades> resultado2 = _subactivityService.ConsultarSubAtividade(prIdSub, prId);

                if (resultado2 != null && resultado2.Any())
                {
                    if (resultado.SubAtividades == null)
                        resultado.SubAtividades = new List<SubAtividades>();


                    foreach (var sub in resultado2)
                    {
                        SubAtividades Subs = new SubAtividades
                        {
                            ID = sub.ID,
                            TecLocalizacao = sub.TecLocalizacao,
                            TecEndereco = sub.TecEndereco,
                            TecInicioTarefa = sub.TecInicioTarefa,
                            TecFimTarefa = sub.TecFimTarefa,
                            Descricao = sub.Descricao,
                            Comentarios = sub.Comentarios,
                            ValorMaoObra = sub.ValorMaoObra,
                            QtdHoras = sub.QtdHoras,
                            ValorKM = sub.ValorKM,
                            DistanciaKmServico = sub.DistanciaKmServico,
                            IdFormulario = sub.IdFormulario,
                            Formulario = sub.Formulario,
                            DataPrevistaAtendimento = sub.DataPrevistaAtendimento,
                            DataSubAtividade = sub.DataSubAtividade,
                            Cliente_Nome = sub.Cliente_Nome,
                            Cliente_Cnpj = sub.Cliente_Cnpj,
                            SubAtividade_Status = sub.SubAtividade_Status,
                            SubAtividade_Etapa = sub.SubAtividade_Etapa,
                            Atividade_Id = sub.Atividade_Id,
                            Atividade_Status = sub.Atividade_Status,
                            Atividade_Etapa = sub.Atividade_Etapa,
                            Id_Tecnico = sub.Id_Tecnico,
                            Id_UsuarioEquipe = sub.Id_UsuarioEquipe,
                            Equipamento = sub.Equipamento,
                            Equipamento_Modelo = sub.Equipamento_Modelo,
                            Equipamento_Marca = sub.Equipamento_Marca,
                            Equipamento_Status = sub.Equipamento_Status,
                            Equipamento_PlanoMensal = sub.Equipamento_PlanoMensal,
                            Equipamento_Necessidade = sub.Equipamento_Necessidade,
                            PerguntasRelatorio = sub.PerguntasRelatorio
                        };

                        resultado.SubAtividades.Add(Subs);
                    }
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }


    }
}
