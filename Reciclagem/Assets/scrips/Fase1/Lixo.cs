using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo : MonoBehaviour
{
    private Pontuacao pontuacao;
    private GeradorDeLixo geradorLixo;
    private Vector3 posicaoInicial;
    private bool arrastando = false;

    private void Awake()
    {
        pontuacao = FindObjectOfType<Pontuacao>(); 
        geradorLixo = FindObjectOfType<GeradorDeLixo>();
    }

    private void Start()
    {
        posicaoInicial = transform.position;
    }

    private void OnMouseDown()
    {    
        arrastando = true;
    }

    private void OnMouseUp()
    {   
        arrastando = false;
    }
    
    private void FixedUpdate()
    {
        MoverLixo(arrastando); 
    }

    private void OnTriggerExit2D(Collider2D ColisaoLixeira)
    {
        int indice = geradorLixo.indiceLixo;
        string tipoLixo = transform.GetChild(indice).tag;
        string tipoLixeira = ColisaoLixeira.tag;
        if (tipoLixeira == tipoLixo && !arrastando)
        {            
            Reciclar();
        } else if (!arrastando)
        {
            NaoReciclar();
        }
    }

    private void MoverLixo(bool valor)
    {
        if (valor)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        } else
        {
            transform.position = posicaoInicial;
        }
    }

    private void Reciclar()
    {
        Destroy(gameObject);
        pontuacao.Pontuar();
        pontuacao.AudioPontuacao(true); 
    }

    private void NaoReciclar()
    {
        pontuacao.AudioPontuacao(false);
    }

}
