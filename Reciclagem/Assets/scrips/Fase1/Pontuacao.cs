using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pontuacao : MonoBehaviour
{
    [SerializeField]
    private Interface inter; 
    private int pontos;

    public void Pontuar()
    {
        pontos++; 
        inter.AtualizarPontos(pontos);
    }
}
