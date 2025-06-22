using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixeira : MonoBehaviour
{
    [SerializeField]
    private Pontuacao pontuacao; 
    [SerializeField]
    private Interface inter; 
    [SerializeField]
    private GeradorDeLixo geradorLixo;

    private void OnTriggerStay2D(Collider2D ColisaoLixo)
    {
        int indice = geradorLixo.indiceLixo;
        string tipoLixo = ColisaoLixo.transform.GetChild(indice).tag;
        string tipoLixeira = transform.tag;
        bool arrastando = ColisaoLixo.GetComponent<Lixo>().Arrastando;
        if (tipoLixeira == tipoLixo && !arrastando)
        {            
            Reciclar(ColisaoLixo.gameObject);
        } else if (!arrastando)
        {
            NaoReciclar();
            ColisaoLixo.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    private void Reciclar(GameObject lixo)
    {
        Destroy(lixo);
        pontuacao.Pontuar(true);
        inter.AtualizarDescricaoLixo("");
    }

    private void NaoReciclar() 
    {
        pontuacao.Pontuar(false);
    }
}

    
