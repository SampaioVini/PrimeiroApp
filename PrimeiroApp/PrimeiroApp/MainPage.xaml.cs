using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrimeiroApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        int count = 0;


        private void btnCliqueAqui_Clicked(object sender, EventArgs e)
        {
            count++;
            btnCliqueAqui.Text = "Você clicou " + count.ToString() + " Vezes";
        }

        private void btnVerificar_Clicked(object sender, EventArgs e)
        {
            string texto = $"O nome tem {txtNome.Text.Length} caracteres";
            DisplayAlert("Mensagem", texto, "Ok");
        }

        private async void btnLimpar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Pergunta", "Deseja realmente limpar a tela?", "Yes", "No"))
            {
                txtNome.Text = string.Empty;
                btnCliqueAqui.Text = "Clique aqui";
            }
        }

        private async void btnInformarDataNasscimento_Clicked(object sender, EventArgs e)
        {
            try
            {
                string dataDigitada = await DisplayPromptAsync("Info", "Digite a data de nascimento", "Ok");

                DateTime dataConvertida;
                bool converteu = DateTime.TryParse(dataDigitada, new CultureInfo("pt-BR"), DateTimeStyles.None, out dataConvertida);

                //if(!converteu)
                if (converteu == false)
                {
                    throw new Exception("Esta data não é válida");
                }
                else
                {
                    lblDataNascimento.Text = string.Format("{0:dd/MM/yyyy}", dataConvertida);
                    int diasVividos = (int)DateTime.Now.Subtract(dataConvertida).TotalDays;
                    await DisplayAlert("info", $"Você ja viveu {diasVividos}.", "ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message + ex.InnerException, "ok");
            }
        }

        private async void btnOpcoes_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblDataNascimento.Text))
                    throw new Exception("Informe a data de nascimento");
                else
                {
                    DateTime dtNascimento = Convert.ToDateTime(lblDataNascimento.Text, new CultureInfo("pt-BR"));

                    string resposta = await
                        DisplayActionSheet("Pergunta",
                        "Selecionar uma opção",
                        "Cancelar",
                        "Saber o dia da semana",
                        "Saber o dia do mês",
                        "Saber o dia do ano");

                    if (resposta == "Saber o dia da semana")

                    {
                        string diaSemana = String.Empty;
                        switch (dtNascimento.DayOfWeek)
                        {
                            case DayOfWeek.Friday:
                                diaSemana = "Sexta";
                                break;
                            case DayOfWeek.Monday:
                                diaSemana = "Segunda";
                                break;
                            case DayOfWeek.Saturday:
                                diaSemana = "Sabado";
                                break;
                            case DayOfWeek.Sunday:
                                diaSemana = "Domingo";
                                break;
                            case DayOfWeek.Thursday:
                                diaSemana = "Quinta";
                                break;
                            case DayOfWeek.Tuesday:
                                diaSemana = "Terça";
                                break;
                            case DayOfWeek.Wednesday:
                                diaSemana = "Quarta";
                                break;
                            default:
                                break;
                        }



                        string msg = $"Você nasceu no(a) {diaSemana}";
                        await DisplayAlert("info", msg, "Ok");

                    }

                    else if (resposta == "Saber o dia do mês")
                    {
                        string msg = $"Você nasceu no(a) {dtNascimento.Day} dia do mês";
                        await DisplayAlert("info", msg, "Ok");

                    }
                    else if (resposta == "Saber o dia do ano")
                    {
                        string msg = $"Você nasceu no {dtNascimento.DayOfYear} dia do ano";
                        await DisplayAlert("info", msg, "Ok");

                    }

                }
            }

            catch (Exception ex)
            {

                await DisplayAlert("Erro", ex.Message + ex.InnerException, "Ok");
            }

        }
    }
}