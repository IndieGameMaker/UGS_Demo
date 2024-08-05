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
    [SerializeField] private TMP_Text messageText;

    private async void Awake()
    {
        // UGS 초기화
        await UnityServices.InitializeAsync();

        // 버튼 이벤트 연결
        loginButton.onClick.AddListener(async () =>
        {
            // 익명 로그인 처리
            await LoginAsync();
        });
    }

    private async Task LoginAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("익명 로그인 성공");
        }
        catch (AuthenticationException e)
        {
            Debug.Log($"로그인 실패 : {e.Message}");
        }
    }
}
