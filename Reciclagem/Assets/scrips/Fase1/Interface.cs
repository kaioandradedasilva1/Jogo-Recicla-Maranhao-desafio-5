using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Text textoPontuacao;
    [SerializeField]
    private Text textoTipoLixo;
    [SerializeField]
    private Text textoAcerto;
    [SerializeField]
    private AudioSource audioAcerto;
    [SerializeField]
    private AudioSource audioErro;


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
    
    public void AtualizarPontos(int pontos)
    {
        textoPontuacao.text = "Pontuação: " + pontos.ToString();
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
}
