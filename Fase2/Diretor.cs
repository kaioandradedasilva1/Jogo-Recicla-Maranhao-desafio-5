using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretor : MonoBehaviour
{
    [SerializeField]
    private GeradorDeLixo geradorLixo;
    public float intervalo = 2f;
    private float tempoPassado = 0f;

    void Update()
    {
        tempoPassado += Time.deltaTime;
        if (tempoPassado >= intervalo)
        {
            geradorLixo.GerarLixo();
            tempoPassado = 0f;
        }
    }
}
