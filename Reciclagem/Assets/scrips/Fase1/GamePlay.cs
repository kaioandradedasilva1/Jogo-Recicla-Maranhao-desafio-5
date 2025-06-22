using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private Pontuacao pontuacao; 
    [SerializeField] private GeradorDeLixo geradorLixo; 
    [SerializeField] private Interface inter; 
    [SerializeField] private TrocaCenario trocaCenario;
    [SerializeField] private TrocarCena trocarCena; 
    public bool jogoExecutando = false; 
    public float tempoDecorrido = 0f;

    void Update()
    {
        ContagemTempoDecorrido();
    }

    private void ContagemTempoDecorrido()
    {
        if(jogoExecutando)
        {
            tempoDecorrido +=Time.deltaTime;
            inter.AtualizarTempoDecorrido(tempoDecorrido);
        }
    }

    public void IniciarJogo()
    {
        jogoExecutando = true;
        geradorLixo.GerarLixo();
        
    }

    public void PausarJogo(bool valor)
    {
        if (valor)
        {
            jogoExecutando = false;
        } else 
        {
            jogoExecutando = true;
        }
        inter.MostarPainelPause(valor);

    }

    public void ReiniciarJogo()
    {
        tempoDecorrido = 0f;
        inter.MostrarPainelGamePlay(true);
        IniciarJogo();
        pontuacao.ZerarPontos();
    }

    public void PararJogo()
    {
        jogoExecutando = false;
        tempoDecorrido = 0f;
        trocarCena.CarregarCena("MenuInicial");
    }

    public void GerarNovoLixo(bool valor)
    {
        if (valor) 
        {
        StartCoroutine(GerarLixoDepoisDeSegundos(1f));
        }
    }

    IEnumerator GerarLixoDepoisDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        geradorLixo.GerarLixo();
    }

    public void VerficarCertos(bool valor, int pontos)
    {
        if (valor) 
        {
            inter.MostrarPainelGamePlay(false);
            inter.AtualizarPontuacaoFinal(pontos);
        }
    }

}
