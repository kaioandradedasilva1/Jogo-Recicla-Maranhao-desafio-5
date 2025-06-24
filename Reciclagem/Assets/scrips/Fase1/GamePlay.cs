using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private Pontuacao pontuacao; 
    [SerializeField] private GeradorDeLixo geradorLixo; 
    [SerializeField] private ControlaUI controlaUI; 
    [SerializeField] private TrocaCenario trocaCenario;
    [SerializeField] private ControlaAudio controlaAudio;

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
        controlaAudio.TocarClique();
        controlaUI.MostrarTelaEscolhaCenario();
    }

    public void IniciarJogo()
    {
        controlaAudio.TocarClique();
        controlaAudio.TocarAudioMenu(false);
        controlaAudio.TocarAudioGamePlay(true);
        geradorLixo.ResertarPosicaoGeradorLixo();
        geradorLixo.GerarLixo();
    }

    public void PausarJogo(bool valor)
    {
        controlaAudio.TocarClique();
        controlaAudio.PausarAudioGamePlay(valor);
        jogoExecutando = !valor;
        controlaUI.MostarPainelPause(valor);

    }

    public void ReiniciarJogo(string origem)
    {
        if(origem == "pause")
        {
            PausarJogo(false);
        } else if (origem == "gameOver")
        {
            controlaUI.MostrarPainelGamePlay(true);
        }
        pontuacao.ZerarPontos();
        tempoDecorrido = 0; 
        DestrirLixo();
        IniciarJogo();
    }

    public void PararJogo()
    {
        controlaAudio.TocarClique();
        controlaAudio.TocarAudioGamePlay(false);
        controlaAudio.TocarAudioMenu(true);
        jogoExecutando = false;
        tempoDecorrido = 0f;
        controlaUI.MostrarPainelMenu();
        pontuacao.ZerarPontos();
        DestrirLixo();
    }

    private void DestrirLixo()
    {
        Lixo lixo = FindObjectOfType<Lixo>();

        if(lixo) 
        {
            Destroy(lixo.gameObject);
        }
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
            controlaAudio.TocarAudioConcluido();

        }
    }

}
