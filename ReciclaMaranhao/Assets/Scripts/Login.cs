using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	[SerializeField] private ControlaUI controlaUI;
	[SerializeField] private ControlaAudio controlaAudio;
	[SerializeField] private DadosDoUsuario dadosUsuario; 
    private bool carregando = false; 
    private string email; 
    private string idUsuario;
    private string nomeUsuario;
    private int recorde;

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
        public string mensagem;
        public string usuarioId;
        public string nome;
        public string email;
        public int record;
    }

	public void FazerLogin()
    {
        controlaAudio.TocarClique();
        StartCoroutine(RequisicaoLogin());
        StartCoroutine(MensagemLogin(true, "Carregando"));
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
            StartCoroutine(MensagemLogin(false, erro));
        }
        else
        {
            string resposta = request.downloadHandler.text;
            UsuarioResponse usuario = JsonUtility.FromJson<UsuarioResponse>(resposta);
            idUsuario = usuario.usuarioId;
            email = usuario.email;
            nomeUsuario = usuario.nome;
            recorde = usuario.record; 
            StartCoroutine(MensagemLogin(false, usuario.mensagem));
            StartCoroutine(AcessarUsuarioOnLine());
        }
    }

	public void LoginConvidado() 
	{
		controlaAudio.TocarClique();
		string nomeConvidado = campoNomeConvidado.text;
		if(nomeConvidado == "")
		{
            nomeConvidado = "Player";
		}
        int record = PlayerPrefs.GetInt("Recorde-" + nomeConvidado, 0);
		controlaAudio.TocarAudioMenu(true);
		controlaUI.MostrarTelaPlay();
		dadosUsuario.DefinirUsuarioLocal(nomeConvidado, record);
		controlaUI.AtualizarTelaInicial(nomeConvidado, record);
	}

	public void FazerLogout()
	{
		controlaAudio.TocarAudioMenu(false);
		controlaUI.MostrarTelaLogin();
        controlaUI.ExibirMensagemLogin("");
	}

    IEnumerator AcessarUsuarioOnLine()
    {
        yield return new WaitForSeconds(1.5f);
        dadosUsuario.DefinirUsuarioOnline(idUsuario, nomeUsuario, recorde);
        controlaUI.MostrarTelaPlay();
		controlaUI.AtualizarTelaInicial(nomeUsuario, recorde); 
        controlaAudio.TocarAudioMenu(true);
    }

    IEnumerator MensagemLogin(bool valor, string mensagem)
    {
        carregando = valor;
        string baseTexto = mensagem;
        if (!carregando)
        {
            controlaUI.ExibirMensagemLogin(baseTexto);
            yield break;
        }

        int pontos = 0;
        while (carregando)
        {
           pontos = (pontos + 1) % 4;
           string textoCarregando = baseTexto + new string('.', pontos);
           controlaUI.ExibirMensagemLogin(textoCarregando);
           yield return new WaitForSeconds(0.5f);
        }
    }

}