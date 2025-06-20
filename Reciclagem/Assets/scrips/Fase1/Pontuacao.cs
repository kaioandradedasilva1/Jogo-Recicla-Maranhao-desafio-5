using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pontuacao : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioAcerto;
    [SerializeField]
    private AudioSource audioErro;
    [SerializeField]
    private Text textoPontuacao;
    [SerializeField]
    private GeradorDeLixo geradorLixo;
    private int acertos;

    private void Start()
    {
        geradorLixo.GerarLixo();
    }

    public void Pontuar()
    {
        acertos++; 
        AtualizarPontos();
        //geradorLixo.GerarLixo();
    }

    private void AtualizarPontos()
    {
        textoPontuacao.text = "Pontuação: " + acertos.ToString();
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
