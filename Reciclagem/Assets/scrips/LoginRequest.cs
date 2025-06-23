using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoginRequest : MonoBehaviour
{
	[SerializeField]
	private ControlaUI controlaUI;
	//Informações do usuário
	[Header("Campo de Entrada")]
	public InputField campoUsuario;
	public InputField campoSenha;
	
	//Endereço da API do back end
	private string apiUrl = "https://suaapi.com/auth/login";
	
	//Função que deve ser chamada na hora de fazer login
	public void FazerLogin()
	{
		string usuario = campoUsuario.text;
		string senha = campoSenha.text;
	    StartCoroutine(EnviarLogin(usuario, senha));
	}
	
	//Função que checa se o login do usuario está devidamente cadastrado no banco de dados
	private IEnumerator EnviarLogin(string usuario, string senha)
	{
	    LoginData loginData = new LoginData { username = usuario, password = senha };
	    string jsonData = JsonUtility.ToJson(loginData);
	
	    UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
	    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
	
	    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
	    request.downloadHandler = new DownloadHandlerBuffer();
	    request.SetRequestHeader("Content-Type", "application/json");
	
	    yield return request.SendWebRequest();
	
	    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
	    {
			string erro = "Erro na requisição: " + request.error;
	        //Debug.LogError(erro);
			controlaUI.ExibirMensagemLogin(erro);
	    }
	    else
	    {
	        string jsonResponse = request.downloadHandler.text;
	        LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);
	
	        if (response.success)
	        {
				string mensagemSucesso = "Login realizado com sucesso: " + response.message;
	            //Debug.Log(mensagemSucesso);
				controlaUI.ExibirMensagemLogin(mensagemSucesso); 
				controlaUI.MostrarTelaPlay();

	        }
	        else
	        {
				string mensagemFalha = "Falha no login: " + response.message;
	            //Debug.Log(mensagemFalha);
				controlaUI.ExibirMensagemLogin(mensagemFalha);
	        }
	    }
	}

	public void PularLogin() 
	{
		controlaUI.MostrarTelaPlay();
	}
	
	//Informações enviadas para o backend
	[System.Serializable]
	public class LoginData
	{
	    public string username;
	    public string password;
	}
	
	//Informações recebidas do backend
	[System.Serializable]
	public class LoginResponse
	{
	    public bool success;
	    public string message;
	}
}