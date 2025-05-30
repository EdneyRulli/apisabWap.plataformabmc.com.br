﻿using ApiSabWap.Data;
using ApiSabWap.Model;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Drawing;
using System.IO;


namespace ApiSabWap.Services
{
    public class SubAtividadesServices
    {
        private readonly Connections conn = new Connections();


        public List<SubAtividades> ConsultarSubAtividade(string prIdSubAtividade, string prIdAtividade = "0")
        {
            List<SubAtividades> resultado = new List<SubAtividades>();

            try
            {
                var parammeters = new {
                    PrId_Usuario        = 0,
                    PrId_Atividade      = prIdAtividade,
                    PrId_SubAtividade   = prIdSubAtividade
                };

                resultado = conn.sqlConnection.Query<SubAtividades>("Sp_ConsultaSubAtividades", parammeters, commandType: CommandType.StoredProcedure).ToList();

                if (resultado.Any())
                {

                    // LISTA SUSBS
                    foreach (var sub in resultado)
                    {
                        var IdSub       = sub.ID;
                        var IdRelatorio = sub.IdFormulario.ToString();
                        var RespostaForm= sub.StatusRelatorio;

                        bool RelAprovado = false;


                        if (!string.IsNullOrEmpty(RespostaForm))
                        {
                            if (RespostaForm.ToUpper().Contains("APROVADO")) RelAprovado = true;
                        }

                        if (!RelAprovado)
                        {
                            // PREENCHE A LISTA DAS PERGUNTAS
                            List<SubAtividadesPerguntas> resultado2 = ConsultarPerguntasRelatorio(IdRelatorio);

                            if (resultado2 != null && resultado2.Any())
                            {
                                if (sub.PerguntasRelatorio == null)
                                    sub.PerguntasRelatorio = new List<SubAtividadesPerguntas>();


                                // LISTA PERGUNTAS
                                foreach (var pergunta in resultado2)
                                {
                                    var varResposta = "";

                                    var ConsultaResposta = ConsultaRelatorioResposta(prIdSubAtividade, IdRelatorio, pergunta.ID.ToString());

                                    if (ConsultaResposta != null)
                                        varResposta = ConsultaResposta.Resposta;



                                    SubAtividadesPerguntas perguntaADD = new SubAtividadesPerguntas
                                    {
                                        ID              = pergunta.ID,
                                        Id_Formulario   = sub.IdFormulario,
                                        NomeRelatorio   = pergunta.NomeRelatorio,
                                        Descricao       = pergunta.Descricao,
                                        Ordem           = pergunta.Ordem,
                                        Resposta        = varResposta    
                                    };

                                    sub.PerguntasRelatorio.Add(perguntaADD);
                                }
                            }
                        }
                    }
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public RelatorioResposta ConsultaRelatorioResposta(string PprIdSubAtividade, string IdRelatorio, string perguntaID)
        {
            try
            {
                var parammeters = new
                {
                    prIdSubAtividade = PprIdSubAtividade
                    ,prId_Formulario = IdRelatorio
                    ,prIdPergunta = perguntaID
                };

                var resultado = conn.sqlConnection.Query<RelatorioResposta>("Sp_ListarTiposFormularios_Resposta", parammeters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SubAtividadesPerguntas> ConsultarPerguntasRelatorio(string IdRelatorio) 
        {
            try
            {
                var parammeters = new
                {
                    prId_Formulario = IdRelatorio
                };

                List<SubAtividadesPerguntas> resultado = conn.sqlConnection.Query<SubAtividadesPerguntas>("Sp_ListarTiposFormularios_Perguntas", parammeters, commandType: CommandType.StoredProcedure).ToList();

                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GravarResposta(RespostaFormularioRequest request)
        {
            try
            {
                var parameters = new
                {
                    PrId_Atividade = 0,
                    PrId_SubAtividade = request.IdSub,
                    PrId_Usuario = 0,
                    PrId_Pergunta = request.IdPergunta,
                    PrResposta = request.Resposta,
                    PrRespostaOBS = request.Comentario,
                    PrOrigemRespostas = "EVA"
                };

                var RetProc = "";

                try
                {
                    RetProc = conn.sqlConnection.Execute("Sp_GravarTiposFormularios_Respostas", parameters, commandType: CommandType.StoredProcedure).ToString();

                    RetProc = "";
                }
                catch (Exception ErrSQL)
                {
                    RetProc = ErrSQL.Message;
                }               

                return RetProc;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool GravarRespostaLista(RespostaLista request)
        {
            try
            {
                foreach (var itemResp in request.ListaRespostas)
                {
                    var parameters = new
                    {
                        PrId_Atividade = 0,
                        PrId_SubAtividade = itemResp.IdSub,
                        PrId_Usuario = 0,
                        PrId_Pergunta = itemResp.IdPergunta,
                        PrResposta = itemResp.Resposta,
                        PrRespostaOBS = itemResp.Comentario,
                        PrOrigemRespostas = "EVA"
                    };

                    conn.sqlConnection.Execute("Sp_GravarTiposFormularios_Respostas", parameters, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GravarRespostaFotos(GravarFoto request)
        {
            string basePath         = AppDomain.CurrentDomain.BaseDirectory;
            string caminhoAbsoluto  = Path.Combine(basePath, "UploadApp");

            try
            {
                string pathGravar = caminhoAbsoluto + "\\RespostaFormularioFotos_" + request.IdSubatividade + "_" + request.idFormulario;

                if (!Directory.Exists(pathGravar))
                    Directory.CreateDirectory(pathGravar).ToString();


                Random random = new Random();
                long numeroAleatorio = (long)(random.NextDouble() * 9000000000L) + 1000000000L;

                var Nomeimg = string.Empty;
                if (!string.IsNullOrEmpty(request.ImagemBase64))
                    Nomeimg = (numeroAleatorio.ToString() + ".png");



                if (!string.IsNullOrEmpty(request.ImagemBase64))
                {
                    Image imagem;

                    imagem = Base64ToImage(request.ImagemBase64);
                    imagem.Save(pathGravar + "\\" + Nomeimg);
                }


                var parameters = new
                {
                    PrId_SubAtividade = request.IdSubatividade,
                    PrId_Usuario      = request.id_Tecnico,
                    PrId_TipoRel      = request.idFormulario,
                    PrImagem          = Nomeimg
                };


                var RetGravar = conn.sqlConnection.Execute("Sp_GravarRespostaFotos", parameters, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}
