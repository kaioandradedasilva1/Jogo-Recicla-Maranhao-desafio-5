using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioMenu;
    [SerializeField] private AudioSource audioGamePlay;
    [SerializeField] private AudioSource audioAcerto;
    [SerializeField] private AudioSource audioErro;
    [SerializeField] private AudioSource audioConcluido;
    [SerializeField] private AudioSource audioCliqueButao;

    public void TocarAudioMenu(bool valor)
    {
        if(valor)
        {
            audioMenu.Play();
        } else
        {
            audioMenu.Stop();
        }
    }

    public void TocarAudioGamePlay(bool valor)
    {
        if(valor)
        {
            audioGamePlay.Play();
        } else 
        {
            audioGamePlay.Stop();
        }
    }

    public void PausarAudioGamePlay(bool valor)
    {
        if (valor)
        {
            audioGamePlay.volume = 0.2f;
        } else 
        {
            audioGamePlay.volume = 0.5f;
        }
        
    }

    public void TocarAudioPontuacao(bool acerto)
    {
        if (acerto)
        {
            audioAcerto.Play();
        } else
        {
            audioErro.Play();
        }
    }

    public void TocarAudioConcluido()
    {
        audioConcluido.Play();
    }
    
    public void TocarClique()
    {
        audioCliqueButao.Play();
    }
}
