using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private Button singleDataSaveButton;

    private async void Awake()
    {
        // 서비스 초기화
        await UnityServices.InitializeAsync();

        // 로그인 성공시 호출되는 이벤트
        AuthenticationService.Instance.SignedIn += () =>
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"익명 로그인 성공 : {playerId}");
        };

        // 익명로그인 처리
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

}
