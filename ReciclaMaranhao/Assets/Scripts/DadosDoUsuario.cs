using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DadosDoUsuario : MonoBehaviour
{
    [Header("Credenciais do Usuário")]
    private string tipoUsuario; 
    public string idUsuario;
    public string nomeUsuario;
    public int recordeAtual;

    private string urlAtualizar = "https://dc-descarte-certo-backend.onrender.com/api/usuarios/";


    public void DefinirUsuarioLocal(string nome, int record)
    {
        recordeAtual = record;
        nomeUsuario = nome;
        tipoUsuario = "local";
    }

    public void DefinirUsuarioOnline(string id, string nome, int recorde)
    {
        recordeAtual = recorde; 
        nomeUsuario = nome;
        idUsuario = id; 
        tipoUsuario = "online";
    }

    [System.Serializable]
    private class AtualizarRecordRequest
    {
        public int record;
    }


    public void SalvarRecorde(int NovoRecorde)
    {
        recordeAtual = NovoRecorde;

        if (tipoUsuario == "local")
        {
            PlayerPrefs.SetInt("Recorde-" + nomeUsuario, recordeAtual);
            PlayerPrefs.Save();
        } else 
        {
            StartCoroutine(RequisicaoAtualizarRecorde());
        }
        
    }

    private IEnumerator RequisicaoAtualizarRecorde()
    {
        AtualizarRecordRequest dados = new AtualizarRecordRequest { record = recordeAtual };
        string json = JsonUtility.ToJson(dados);

        string urlFinal = urlAtualizar + idUsuario;

        UnityWebRequest request = UnityWebRequest.Put(urlFinal, json);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao atualizar recorde: " + request.error);
        }
        else
        {
            Debug.Log("Recorde atualizado com sucesso!");
        }
    }

}

