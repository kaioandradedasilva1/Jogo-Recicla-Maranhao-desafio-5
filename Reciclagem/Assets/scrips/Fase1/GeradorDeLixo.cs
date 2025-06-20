using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeLixo : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabLixo;
    public int indiceLixo; 

    public void GerarLixo()
    {
        GameObject novoLixo = Instantiate(prefabLixo, transform.position, Quaternion.identity);
        int totalFilhos = novoLixo.transform.childCount;
        int indiceAleatorio = Random.Range(0, totalFilhos);
        indiceLixo = indiceAleatorio; 
        Transform lixoEscolhido = novoLixo.transform.GetChild(indiceAleatorio);
        lixoEscolhido.gameObject.SetActive(true);
        novoLixo.tag = lixoEscolhido.tag;
        Debug.Log(novoLixo.tag);
    }
}
