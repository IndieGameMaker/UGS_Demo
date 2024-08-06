using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class RCManager : MonoBehaviour
{
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
    }
}
