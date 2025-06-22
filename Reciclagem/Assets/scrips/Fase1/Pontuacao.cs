using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pontuacao : MonoBehaviour
{
    [SerializeField] private GeradorDeLixo geradorLixo;
    [SerializeField] private Interface inter; 
    [SerializeField] private GamePlay gamePlay;
    public int acertos = 0;
    public int acertosMax = 20;
    private int erros = 0; 
    private int pontos = 0;

    public void Pontuar(bool valor)
    {
        if(valor)
        {
            acertos++;
            inter.AtualizarTextoAcerto("Acertou!");
            gamePlay.GerarNovoLixo(acertos < acertosMax);
        } else 
        {
            erros++;
            inter.AtualizarTextoAcerto("Lixeira Errada!");
        }

        pontos = acertos*100 - erros*20 - Mathf.RoundToInt(gamePlay.tempoDecorrido) * 2;
        
        if (pontos < 0) 
        {
            pontos = 0; 
        }

        inter.AtualizarPontuacao(acertos, pontos);
        inter.AudioPontuacao(valor);
        gamePlay.VerficarCertos(acertos == acertosMax, pontos);
    }

    public void ZerarPontos()
    {
        acertos = 0;
        erros = 0;
        pontos = 0; 
        inter.AtualizarPontuacao(acertos, pontos);
    }
}
