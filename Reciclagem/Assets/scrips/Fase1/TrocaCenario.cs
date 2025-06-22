using System.Collections.Generic;
using UnityEngine;

public class TrocaCenario : MonoBehaviour
{
    [SerializeField] private Interface inter; 
    [SerializeField] private GeradorDeLixo geradorLixo;
    [SerializeField] private Pontuacao pontuacao;
    [SerializeField] private GamePlay gamePlay;

    public List<Vector2> VerticesTrapesio;

    private void Start() 
    {
        VerticesTrapesio = new List<Vector2>
        {
            Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero
        };
    }

    public void SelecionaCenarioPraia()
    {
        inter.TrocarCenario("Praia");

        VerticesTrapesio = new List<Vector2>
        {
            new Vector2(0.5f, 0.5f),    // A
            new Vector2(7f, 0.5f),      // B
            new Vector2(7f, -1.5f),     // C
            new Vector2(-6.5f, -1.5f)   // D
        };
        geradorLixo.ConfigurarVertices(VerticesTrapesio);
        gamePlay.IniciarJogo();
    }

    public void SelecionaCenarioCentroHistorico()
    {
        inter.TrocarCenario("CentroHistorico");

        VerticesTrapesio = new List<Vector2>
        {
            new Vector2(-1.3f, -1.6f), // A
            new Vector2(1f, -1.6f),    // B
            new Vector2(5f, -4.7f),    // C
            new Vector2(-5f, -4.7f)    // D
        };

        geradorLixo.ConfigurarVertices(VerticesTrapesio);
        gamePlay.IniciarJogo();
    }
}
