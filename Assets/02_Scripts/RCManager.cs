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

        // Remote Config 이벤트 연결
        RemoteConfigService.Instance.FetchCompleted += (response) =>
        {
            Debug.Log("데이터 로드 완료");
            playerScale = RemoteConfigService.Instance.appConfig.GetFloat("player_scale");
            moveSpeed = RemoteConfigService.Instance.appConfig.GetFloat("move_speed");

            GameObject.Find("Mummy_Mon").transform.localScale = Vector3.one * playerScale;
        };

        await Task.Delay(3000);

        await GetRemoteConfigData();
    }

    // 데이터 로드를 위한 구조체 선언
    public struct userAttr { }
    public struct appAttr { }

    // 데이터 로드
    private async Task GetRemoteConfigData()
    {
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttr(), new appAttr());
    }
}
