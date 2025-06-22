using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField] private GameObject painelGamePlay;
    [SerializeField] private Text textoAcertos;
    [SerializeField] private Text textoPontos;
    [SerializeField] private Text textoTempo;
    [SerializeField] private Text textoTipoLixo;
    [SerializeField] private Text textoAcerto;
    [SerializeField] private GameObject painelGameOver;
    [SerializeField] private Text textoPontuacaoFinal;
    [SerializeField] private GameObject painelEscolhaCenario;
    [SerializeField] private GameObject cenarioPraia;
    [SerializeField] private GameObject cenarioCentroHistorico;
    [SerializeField] private GameObject painelPause;
    [SerializeField] private AudioSource audioAcerto;
    [SerializeField] private AudioSource audioErro;

    private void Start()
    {
       painelEscolhaCenario.SetActive(true);
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
        textoTempo.text = "Tempo: " + tempo.ToString("F0") + " s";
    }

    public void AtualizarDescricaoLixo(string tipoLixo) 
    {
        textoTipoLixo.text = tipoLixo; 
    }
    
    public void AtualizarTextoAcerto(string texto)
    {
        textoAcerto.text = texto;
        StartCoroutine(LimparTextoDepoisDeSegundos(1f));
    }

    IEnumerator LimparTextoDepoisDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        textoAcerto.text = "";
    }

    public void AtualizarPontuacaoFinal(int pontos)
    {
        textoPontuacaoFinal.text = "Pontuacão Final: " + pontos.ToString() + " pontos";
    }

    public void AudioPontuacao(bool acerto)
    {
        if (acerto)
        {
            audioAcerto.Play();
        } else
        {
            audioErro.Play();
        }
    }
}
