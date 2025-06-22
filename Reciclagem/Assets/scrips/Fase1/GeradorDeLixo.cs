using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeLixo : MonoBehaviour
{
    [SerializeField] private GameObject prefabLixo;
    [SerializeField] private Interface inter;

    private Vector2 A, B, C, D;
    public int indiceLixo; 

    // Este método será chamado pela TrocaCenario
    public void ConfigurarVertices(List<Vector2> vertices)
    {
        A = vertices[0];
        B = vertices[1];
        C = vertices[2];
        D = vertices[3];

        InvokeRepeating(nameof(GerarPontoAleatorioNoTrapezio), 0f, 2f);
    }

    public void GerarPontoAleatorioNoTrapezio()
    {
        float t = Random.Range(0f, 1f); // posição vertical
        float s = Random.Range(0f, 1f); // posição horizontal entre as bases

        Vector2 pontoTopo = Vector2.Lerp(A, B, s);
        Vector2 pontoBase = Vector2.Lerp(D, C, s);
        Vector2 pontoFinal = Vector2.Lerp(pontoBase, pontoTopo, t);
        transform.position = pontoFinal;
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
