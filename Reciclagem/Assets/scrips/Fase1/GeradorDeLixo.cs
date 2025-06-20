using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeLixo : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabLixo;
    [SerializeField]
    private Interface inter;
    public int indiceLixo; 

    private void Start()
    {
        GerarLixo();
        InvokeRepeating("MudarPosicao", 0f, 2f);
    }

    void MudarPosicao()
    {
        float novoX = Random.Range(-6.5f, 6.5f);
        float novoY = Random.Range(-1.5f, 0.5f);
        transform.position = new Vector3(novoX, novoY, 0f);
    }

    public void GerarLixo()
    {
        GameObject novoLixo = Instantiate(prefabLixo, transform.position, Quaternion.identity);
        int totalFilhos = novoLixo.transform.childCount;
        int indiceAleatorio = Random.Range(0, totalFilhos);
        indiceLixo = indiceAleatorio; 
        Transform lixoEscolhido = novoLixo.transform.GetChild(indiceAleatorio);
        lixoEscolhido.gameObject.SetActive(true);
        inter.AtualizarDescricaoLixo(lixoEscolhido.tag);
    }
}
