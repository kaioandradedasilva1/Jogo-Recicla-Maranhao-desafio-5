using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pontuacao : MonoBehaviour
{
    [SerializeField] private GeradorDeLixo geradorLixo;
    [SerializeField] private ControlaUI controlaUI; 
    [SerializeField] private ControlaAudio controlaAudio;
    [SerializeField] private GamePlay gamePlay;
    [SerializeField] private DadosDoUsuario dadosUsuario; 
    
    public int Acertos = 0;
    public int AcertosMax = 20;
    public float TempoParaAcerto = 2f; 
    private int erros = 0; 
    private int pontos = 0;
    private float tempoAnterior = 0;

    private void Start()
    {
        string textAcertoMax = "Colete " + AcertosMax.ToString() + " Lixos";
        controlaUI.AtualizarTextoAcerto(textAcertoMax , AcertosMax);
    }

    public void Pontuar(bool valor)
    { 
        float intervaloTempo = gamePlay.tempoDecorrido - tempoAnterior;
    
        if(valor)
        {
            Acertos++;
            controlaUI.AtualizarTextoAcerto("Acertou!", AcertosMax - Acertos);
            if (Acertos < AcertosMax)
            {
                gamePlay.GerarNovoLixo();
            }
        } else 
        {
            erros++;
            controlaUI.AtualizarTextoAcerto("Lixeira Errada!", AcertosMax - Acertos);
        }

        CalcularPontos(intervaloTempo);
        
        controlaUI.AtualizarPontuacao(Acertos, pontos);
        controlaAudio.TocarAudioPontuacao(valor);
        gamePlay.VerficarCertos(Acertos == AcertosMax, pontos);
        tempoAnterior = gamePlay.tempoDecorrido;
    }

    private void CalcularPontos(float tempo)
    {
        float tempoExedente = 0; 
        if (tempo > TempoParaAcerto)
        {
            tempoExedente = tempo - TempoParaAcerto; 
        }
        Debug.Log(tempoExedente);
        pontos = Acertos*100 - erros*20 - Mathf.RoundToInt(tempoExedente)*5;

        if (pontos < 0) 
        {
            pontos = 0; 
        }
    }

    public void ZerarPontos()
    {
        Acertos = 0;
        erros = 0;
        pontos = 0; 
        controlaUI.AtualizarPontuacao(Acertos, pontos);
        string textAcertoMax = "Colete " + AcertosMax.ToString() + " lixos";
        controlaUI.AtualizarTextoAcerto(textAcertoMax , AcertosMax);
    }
    
    public void VerificarRecorde()
    {
        int record = dadosUsuario.recordeAtual;
        if (pontos > record)
        {
            dadosUsuario.SalvarRecorde(pontos);
        }
    }

}
