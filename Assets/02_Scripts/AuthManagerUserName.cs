using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AuthUserNamePassword
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userNameText, passwordText;
        [SerializeField] private Button signUpButton, loginButton;
    }
}
