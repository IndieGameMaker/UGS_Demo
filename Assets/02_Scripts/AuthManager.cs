using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button saveNameButton;

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField playerNameIf;

    private async void Awake()
    {
        // UGS 초기화
        await UnityServices.InitializeAsync();

        // 인증 이벤트 연결
        EventConfig();

        // 버튼 이벤트 연결
        loginButton.onClick.AddListener(async () =>
        {
            // 익명 로그인 처리
            await LoginAsync();
        });

        logoutButton.onClick.AddListener(() =>
        {
            // 로그 아웃
            AuthenticationService.Instance.SignOut();
        });
    }

    // 인증 이벤트 설정
    private void EventConfig()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("익명 로그인 성공");
            string id = AuthenticationService.Instance.PlayerId;
            messageText.text += $"Player Id : {id}\n";
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("로그 아웃\n");
        };
        /*
            SingInFailed
            Expired
        */
    }

    private async Task LoginAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.Log($"로그인 실패 : {e.Message}");
        }
    }
}
