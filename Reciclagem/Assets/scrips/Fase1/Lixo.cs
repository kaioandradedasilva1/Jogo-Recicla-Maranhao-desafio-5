using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo : MonoBehaviour
{
    private Vector3 posicaoInicial;
    public float Velocidade = 10f;
    public bool Arrastando = false; 
    private GamePlay gamePlay; 

    private void Start()
    {
        posicaoInicial = transform.position; 
        gamePlay = FindObjectOfType<GamePlay>();
    }

    private void OnMouseDown()
    {    
        Arrastando = true;
        gamePlay.jogoExecutando = true;

    }

    private void Update()
    {
        if(gamePlay.jogoParado)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseUp()
    {   
        Arrastando = false;
    }
    
    private void FixedUpdate()
    {
        if (transform.position == posicaoInicial)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        
        if (Arrastando)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, posicaoInicial, Velocidade * Time.deltaTime);
        }
    }
}
