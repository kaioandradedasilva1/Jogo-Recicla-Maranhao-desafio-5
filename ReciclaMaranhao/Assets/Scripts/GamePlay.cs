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
    [SerializeField] private DadosDoUsuario dadosUsuario;

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

    public void IniciarGamePlay(bool valor)
    {
        controlaAudio.TocarClique();
        controlaUI.MostrarTelaEscolhaCenario(valor);
    }

    public void IniciarJogo()
    {
        controlaAudio.TocarClique();
        controlaAudio.TocarAudioMenu(false);
        controlaAudio.TocarAudioGamePlay(true);
        controlaAudio.VolumeAudioGamePlay(0.5f);
        geradorLixo.ResertarPosicaoGeradorLixo();
        geradorLixo.GerarLixo();
    }

    public void PausarJogo(bool valor)
    {
        float volume;
        if(valor)
        {
            volume = 0.2f; 
            controlaUI.AtualizarPainelPause(dadosUsuario.recordeAtual);
        } else
        {
            volume = 0.5f; 
        }
        controlaAudio.TocarClique();
        controlaAudio.VolumeAudioGamePlay(volume);
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
        controlaAudio.VolumeAudioGamePlay(0.5f);
        pontuacao.ZerarPontos();
        jogoExecutando = false;
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
        controlaUI.AtualizarTelaInicial(dadosUsuario.nomeUsuario, dadosUsuario.recordeAtual);
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
            pontuacao.VerificarRecorde();
            controlaUI.MostrarPainelGamePlay(false);
            controlaUI.AtualizarPainelGameOver(pontos, dadosUsuario.recordeAtual);
            controlaUI.AtualizarTextoParabens(dadosUsuario.nomeUsuario);
            controlaAudio.VolumeAudioGamePlay(0.2f);
            controlaAudio.TocarAudioConcluido();
        }
    }

}
