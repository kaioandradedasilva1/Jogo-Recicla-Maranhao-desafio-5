using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleDeInterfaceUI : MonoBehaviour
{
    [SerializeField]
    private GameObject telaLogin; 
    [SerializeField]
    private GameObject telaJogo; 
    [SerializeField]
    private Text mensagemLogin; 

    void Start()
    {
        MostrarTelaLogin();
    }

    public void MostrarTelaLogin()
    {
        telaLogin.SetActive(true);
        telaJogo.SetActive(false);
    }

    public void MostrarTelaJogo()
    {
        telaLogin.SetActive(false);
        telaJogo.SetActive(true);
    }

    public void ExibirMensagemLogin(string mensagem) 
    {
        mensagemLogin.text = mensagem;
    }




}
