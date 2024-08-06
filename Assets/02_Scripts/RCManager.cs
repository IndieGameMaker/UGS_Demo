#pragma warning disable IDE0051, UNT0006

using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.RemoteConfig;

public class RCManager : MonoBehaviour
{
    public float moveSpeed;
    public float playerScale;

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

    // 데이터 로드를 위한 구조체 선언
    public struct
}
