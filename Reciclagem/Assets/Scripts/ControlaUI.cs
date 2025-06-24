using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaUI : MonoBehaviour
{
    // Menu 
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject telaLogin; 
    [SerializeField] private GameObject telaPlay; 
    [SerializeField] private Text mensagemLogin;
    [SerializeField] private Text textoSaudacao; 

    // GamePlay
    [SerializeField] private GameObject painelGamePlay;
    [SerializeField] private GameObject painelPause;
    [SerializeField] private Text textoAcertos;
    [SerializeField] private Text textoPontos;
    [SerializeField] private Text textoTempo;
    [SerializeField] private Text textoTipoLixo;
    [SerializeField] private Text textoAcerto;

    //GameOver 
    [SerializeField] private GameObject painelGameOver;
    [SerializeField] private Text textoPontuacaoFinal;
    [SerializeField] private Text textoRecorde; 
    [SerializeField] private Text textoParabens;

    //Cenarios
    [SerializeField] private GameObject painelEscolhaCenario;
    [SerializeField] private GameObject cenarioPraia;
    [SerializeField] private GameObject cenarioCentroHistorico;

    private void Start()
    {
        MostrarTelaLogin();
    }

    public void MostrarPainelMenu()
    {
        painelPause.SetActive(false);
        painelGamePlay.SetActive(false);
        painelGameOver.SetActive(false);
        painelMenu.SetActive(true);
        MostrarTelaPlay();
    }

    public void MostrarTelaLogin()
    {
        telaLogin.SetActive(true);
        telaPlay.SetActive(false);
    }

    public void MostrarTelaPlay()
    {
        telaLogin.SetActive(false);
        telaPlay.SetActive(true);
    }

    public void AtualizarSaldacao(string nome)
    {
        textoSaudacao.text = "Olá, " + nome; 
    }

    public void AtualizarTextoParabens(string nome)
    {
        textoParabens.text = "Parabéns " + nome + "!";
    }

    public void ExibirMensagemLogin(string mensagem) 
    {
        mensagemLogin.text = mensagem;
    }

    public void MostrarTelaEscolhaCenario(bool valor)
    {
        painelMenu.SetActive(!valor);
        telaPlay.SetActive(!valor);
        painelEscolhaCenario.SetActive(valor);
    }

    public void TrocarCenario(string nomeCenario)
    {
        bool praiaActive = false; 
        bool centroHistoricoActive = false; 
        if (nomeCenario == "Praia")
        {
            praiaActive = true; 
        }
        if (nomeCenario == "CentroHistorico")
        {
            centroHistoricoActive = true;
        }
        painelEscolhaCenario.SetActive(false);
        cenarioPraia.SetActive(praiaActive);
        cenarioCentroHistorico.SetActive(centroHistoricoActive);
        MostrarPainelGamePlay(true);
    }

    public void MostrarPainelGamePlay(bool valor)
    {
        painelGamePlay.SetActive(valor);
        painelGameOver.SetActive(!valor);
    }

    public void MostarPainelPause(bool valor)
    {
        painelPause.SetActive(valor);
        painelGamePlay.SetActive(!valor);
    }

    public void AtualizarPontuacao(int acertos, int pontos)
    {
        textoAcertos.text = "Acertos: " + acertos.ToString();
        textoPontos.text = "Pontos: " + pontos.ToString();
    }

    public void AtualizarTempoDecorrido(float tempo)
    {
        textoTempo.text = "Tempo: " + tempo.ToString("F1") + "s";
    }

    public void AtualizarDescricaoLixo(string tipoLixo) 
    {
        textoTipoLixo.text = tipoLixo; 
    }
    
    public void AtualizarTextoAcerto(string texto, int acertosRestantes)
    {
        textoAcerto.text = texto;
        StartCoroutine(LimparTextoDepoisDeSegundos(1f, acertosRestantes));
    }

    IEnumerator LimparTextoDepoisDeSegundos(float segundos, int acertosRestantes)
    {
        yield return new WaitForSeconds(segundos);
        textoAcerto.text = "Colete " + acertosRestantes.ToString() + " lixos";
    }

    public void AtualizarPainelGameOver(int pontos, int recorde)
    {
        //Resgatar o valor do recod()
        textoPontuacaoFinal.text = "Seus Pontos: " + pontos.ToString() + " pontos";
        textoRecorde.text = "Seu Record: " + recorde.ToString() + " pontos";
    }

}
