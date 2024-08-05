using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AuthUserNamePassword
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userNameText, passwordText;
        [SerializeField] private Button signUpButton, loginButton;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
        }

        void OnEnable()
        {
            // 회원가입 버튼 연결
            signUpButton.onClick.AddListener(async () =>
            {
                await
            });
        }
    }

    private async Task SignUpUserNamePassword(string userName, string passwd)
    {
        await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(userName, passwd);
    }
}
