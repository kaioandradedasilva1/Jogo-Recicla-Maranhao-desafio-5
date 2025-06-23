using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pontuacao : MonoBehaviour
{
    [SerializeField] private GeradorDeLixo geradorLixo;
    [SerializeField] private ControlaUI controlaUI; 
    [SerializeField] private GamePlay gamePlay;
    public int acertos = 0;
    public int acertosMax = 20;
    private int erros = 0; 
    private int pontos = 0;

    private void Start()
    {
        string textAcertoMax = "Colete " + acertosMax.ToString() + " Lixos";
        controlaUI.AtualizarTextoAcerto(textAcertoMax , acertosMax);
    }

    public void Pontuar(bool valor)
    { 
        if(valor)
        {
            acertos++;
            controlaUI.AtualizarTextoAcerto("Acertou!", acertosMax - acertos);
            if (acertos < acertosMax)
            {
                gamePlay.GerarNovoLixo();
            }
        } else 
        {
            erros++;
            controlaUI.AtualizarTextoAcerto("Lixeira Errada!", acertosMax - acertos);
        }

        pontos = acertos*100 - erros*20 - Mathf.RoundToInt(gamePlay.tempoDecorrido*10);

        if (pontos < 0) 
        {
            pontos = 0; 
        }

        controlaUI.AtualizarPontuacao(acertos, pontos);
        controlaUI.AudioPontuacao(valor);
        gamePlay.VerficarCertos(acertos == acertosMax, pontos);
    }

    public void ZerarPontos()
    {
        acertos = 0;
        erros = 0;
        pontos = 0; 
        controlaUI.AtualizarPontuacao(acertos, pontos);
        string textAcertoMax = "Colete " + acertosMax.ToString() + " lixos";
        controlaUI.AtualizarTextoAcerto(textAcertoMax , acertosMax);
    }
    
    public void SalvarRecorde()
    {
        int recorde = PlayerPrefs.GetInt("Recorde", 0);

        if (pontos > recorde)
        {
            PlayerPrefs.SetInt("Recorde", pontos);
            PlayerPrefs.Save();
        }
    }

}
