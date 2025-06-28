using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	[SerializeField] private ControlaUI controlaUI;
	[SerializeField] private ControlaAudio controlaAudio;
	[SerializeField] private DadosDoUsuario dadosUsuario; 
    string idUsuario;
    string nomeUsuario;
    int recorde; 

	[Header("Campo de Entrada")]
	public InputField campoEmail;
	public InputField campoSenha;
	public InputField campoNomeConvidado;
	
	private string urlLogin = "https://dc-descarte-certo-backend.onrender.com/api/usuarios/login";
	
	[System.Serializable]
    private class LoginRequest
    {
        public string email;
        public string senha;
    }

    [System.Serializable]
    private class UsuarioResponse
    {
        public string usuarioId;
        public string nome;
        public int? record; // Pode vir nulo
    }

	public void FazerLogin()
    {
        controlaAudio.TocarClique();
        StartCoroutine(RequisicaoLogin());
    }

    private IEnumerator RequisicaoLogin()
    {
        LoginRequest dadosLogin = new LoginRequest { email = campoEmail.text, senha = campoSenha.text };
        string json = JsonUtility.ToJson(dadosLogin);

        UnityWebRequest request = new UnityWebRequest(urlLogin, "POST");
        byte[] corpo = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(corpo);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
			string erro = "Erro ao fazer login: " + request.error;
			controlaUI.ExibirMensagemLogin(erro);
        }
        else
        {
            string resposta = request.downloadHandler.text;
            UsuarioResponse usuario = JsonUtility.FromJson<UsuarioResponse>(resposta);

            idUsuario = usuario.usuarioId; 
            nomeUsuario = usuario.nome;
            recorde = usuario.record.HasValue ? usuario.record.Value : 0;
			dadosUsuario.DefinirUsuarioOnline(idUsuario, nomeUsuario, recorde);
			controlaUI.MostrarTelaPlay();
			controlaUI.AtualizarSaldacao(nomeUsuario); 
            controlaAudio.TocarAudioMenu(true);
            Debug.Log($"Login bem-sucedido. Id: {idUsuario}, Nome: {nomeUsuario}, Recorde: {recorde}");
            Debug.Log(idUsuario);
        }
    }

	public void LoginConvidado() 
	{
		controlaAudio.TocarClique();
		string nomeConvidado = campoNomeConvidado.text;
		if(nomeConvidado.Length > 2)
		{
			controlaAudio.TocarAudioMenu(true);
			controlaUI.MostrarTelaPlay();
			dadosUsuario.DefinirUsuarioLocal(nomeConvidado);
			controlaUI.AtualizarSaldacao(nomeConvidado);
		} else
		{
			controlaUI.ExibirMensagemLogin("Insira seu nome");
		}
	}

	public void FazerLogout()
	{
		controlaAudio.TocarAudioMenu(false);
		controlaUI.MostrarTelaLogin();
	}
}