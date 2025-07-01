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
    float intervaloTempo; 
    private int pontos = 0;
    private float tempoAnterior = 0;

    private void Start()
    {
        string textAcertoMax = "Colete " + AcertosMax.ToString() + " Lixos";
        controlaUI.AtualizarTextoAcerto(textAcertoMax , AcertosMax);
    }

    public void Pontuar(bool valor)
    { 
    
        if(valor)
        {
            Acertos++;
            intervaloTempo = gamePlay.tempoDecorrido - tempoAnterior;
            CalcularPontos(true); 
            
            controlaUI.AtualizarTextoAcerto("Acertou!", AcertosMax - Acertos);
            if (Acertos < AcertosMax)
            {
                gamePlay.GerarNovoLixo();
            }
            tempoAnterior = gamePlay.tempoDecorrido;
        } else 
        {
            CalcularPontos(false); 
            controlaUI.AtualizarTextoAcerto("Lixeira Errada!", AcertosMax - Acertos);
        }

        controlaUI.AtualizarPontuacao(Acertos, pontos);
        controlaAudio.TocarAudioPontuacao(valor);
        gamePlay.VerficarCertos(Acertos == AcertosMax, pontos);
    }

    private void CalcularPontos(bool acertou)
    {
        if (acertou) 
        {
            pontos += 100; 
        } else 
        {
            pontos = pontos - 20; 
        }

        if (intervaloTempo > TempoParaAcerto + 1 && acertou)
        {
            float tempoExedente = intervaloTempo - (TempoParaAcerto + 1); 
            pontos = pontos - Mathf.RoundToInt(tempoExedente*10);
        } else if (acertou)
        {
            float tempoNaoExedente = (TempoParaAcerto + 1) - intervaloTempo; 
            pontos = pontos + Mathf.RoundToInt(tempoNaoExedente*20); 
        }

        if (pontos < 0) 
        {
            pontos = 0; 
        }
    }

    public void ZerarPontos()
    {
        Acertos = 0;
        pontos = 0; 
        tempoAnterior = 0f;
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
