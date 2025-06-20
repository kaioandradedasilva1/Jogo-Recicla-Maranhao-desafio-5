using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo2 : MonoBehaviour
{
    private Pontuacao pontuacao;
    private GeradorDeLixo geradorLixo;
    private Rigidbody2D rb; 
    public float velocidadeQueda = 3f;
  

    private void Awake()
    {
        pontuacao = FindObjectOfType<Pontuacao>(); 
        geradorLixo = FindObjectOfType<GeradorDeLixo>();
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Desativa a gravidade
        rb.velocity = new Vector2(0, -velocidadeQueda);
    }

    private void OnTriggerEnter2D(Collider2D ColisaoLixeira)
    {
        int indice = geradorLixo.indiceLixo;
        string tipoLixo = transform.GetChild(indice).tag;
        string tipoLixeira = ColisaoLixeira.tag;
        if(tipoLixeira == tipoLixo) 
        {
           Reciclar(true); 
           Debug.Log("destruir 1");
           Destroy(gameObject);
        }  else if (tipoLixeira != "Barreira") 
        {
            Reciclar(false);
            Debug.Log("destruir 2");
            Destroy(gameObject);
            
        } else 
        {
            Debug.Log("destruir 3");
            Destroy(gameObject);
        }
        
    }

    private void Reciclar(bool valor)
    {
        if(valor)
        {
            pontuacao.Pontuar();
            pontuacao.AudioPontuacao(true); 
        } else 
        {
            pontuacao.AudioPontuacao(false);
        }
    }

}

