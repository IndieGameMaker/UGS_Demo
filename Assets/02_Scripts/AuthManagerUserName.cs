using System;
using System.Threading.Tasks;
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
            // 회원명 : 대소문자 구별없음, 20자 [. @]
            // 비밀번호 : 대소문자 구별, 8 ~ 30자 , 대문자1, 소문자1, 특수문자1, 숫자1
            signUpButton.onClick.AddListener(async () =>
            {
                await SignUpUserNamePassword(userNameText.text, passwordText.text);
            });
        }

        private async Task SignUpUserNamePassword(string userName, string passwd)
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(userName, passwd);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }


}
