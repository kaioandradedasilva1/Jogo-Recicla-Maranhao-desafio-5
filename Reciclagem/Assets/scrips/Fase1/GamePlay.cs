using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private Pontuacao pontuacao; 
    [SerializeField] private GeradorDeLixo geradorLixo; 
    [SerializeField] private ControlaUI controlaUI; 
    [SerializeField] private TrocaCenario trocaCenario;
    public bool jogoExecutando = false;
    public bool jogoParado;  
    public float tempoDecorrido = 0f;


    private void Update()
    {
        ContagemTempoDecorrido();
    }

    private void ContagemTempoDecorrido()
    {
        if(jogoExecutando)
        {
            tempoDecorrido +=Time.deltaTime;
        }
        controlaUI.AtualizarTempoDecorrido(tempoDecorrido);
    }

    public void IniciarGamePlay()
    {
        controlaUI.MostrarTelaEscolhaCenario();
    }
    

    public void IniciarJogo()
    {
        jogoParado = false;
        geradorLixo.ResertarPosicaoGeradorLixo();
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
        controlaUI.MostarPainelPause(valor);

    }

    public void ReiniciarJogo()
    {
        tempoDecorrido = 0f;
        controlaUI.MostrarPainelGamePlay(true);
        IniciarJogo();
        pontuacao.ZerarPontos();
    }

    public void PararJogo()
    {
        jogoExecutando = false;
        tempoDecorrido = 0f;
        controlaUI.MostrarPainelMenu();
        pontuacao.ZerarPontos();
        jogoParado = true; 
    }

    public void GerarNovoLixo()
    {
        StartCoroutine(GerarLixoDepoisDeSegundos(1f));
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
            pontuacao.SalvarRecorde();
            controlaUI.MostrarPainelGamePlay(false);
            controlaUI.AtualizarPainelGameOver(pontos);
        }
    }

}
