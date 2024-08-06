using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Board = Unity.Services.Leaderboards;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_InputField scoreIf;

    private async Task Awake()
    {
        // 유니티 서비스 초기화
        await UnityServices.InitializeAsync();
        // 익명 로그인 이벤트
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"로그인 완료 : {AuthenticationService.Instance.PlayerId}");
        };
        // 익명 로그인 처리
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        saveButton.onClick.AddListener(() =>
        {
            int score = int.Parse(scoreIf.text);

        });
    }
}
