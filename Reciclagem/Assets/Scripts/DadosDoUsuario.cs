using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DadosDoUsuario : MonoBehaviour
{
    public string tipoUsuario; 
    public string nomeUsuario;
    public int recordeAtual;

    private string urlSalvar = "";
    private string urlConsultar = "";


    public void DefinirUsuarioLocal(string nome)
    {
        recordeAtual = PlayerPrefs.GetInt("Recorde-" + nome, 0);
        nomeUsuario = nome;
        tipoUsuario = "local";
    }

    public void DefinirUsuarioOnline(string nome)
    {
        ConsultarRecordeOnline();
        nomeUsuario = nome; 
        tipoUsuario = "online";
    }

    public void SalvarRecorde(int NovoRecorde)
    {
        recordeAtual = NovoRecorde;

        if (tipoUsuario == "local")
        {
            PlayerPrefs.SetInt("Recorde-" + nomeUsuario, NovoRecorde);
            PlayerPrefs.Save();
        } else 
        {
            SalvarRecordeOnline();
        }
    }

    [System.Serializable]
    public class DadosRecorde
    {
        public string username;
        public int record;
    }

    // Envia dados do usuário para o servidor
    public void SalvarRecordeOnline()
    {
        DadosRecorde dados = new DadosRecorde
        {
            username = nomeUsuario,
            record = recordeAtual
        };

        string json = JsonUtility.ToJson(dados);
        StartCoroutine(EnviarParaServidor(json));
    }

    private IEnumerator EnviarParaServidor(string json)
    {
        UnityWebRequest request = new UnityWebRequest(urlSalvar, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Erro ao salvar: " + request.error);
        else
            Debug.Log("Recorde salvo com sucesso!");
    }

    // Consulta o recorde do usuário no servidor
    public void ConsultarRecordeOnline()
    {
        StartCoroutine(ConsultarServidor(nomeUsuario));
    }

    private IEnumerator ConsultarServidor(string username)
    {
        UnityWebRequest request = UnityWebRequest.Get(urlConsultar + "?username=" + username);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao consultar: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            DadosRecorde recordeRecebido = JsonUtility.FromJson<DadosRecorde>(jsonResponse);
            recordeAtual = recordeRecebido.record;
            Debug.Log("Recorde do usuário: " + recordeAtual);
        }
    }
}
