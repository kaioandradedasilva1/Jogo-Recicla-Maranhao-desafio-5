using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixeira : MonoBehaviour
{
    private bool arrastando = false;
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
        MovimentoLixeira(arrastando); 
    }

    private void MovimentoLixeira(bool valor)
    {
        if (valor)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, transform.position.y, 0);
        }
    }
}
