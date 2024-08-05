using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct PlayerData
{
    public string name;
    public int level;
    public int xp;
    public int gole;
    public List<ItemData> items;
}

[Serializable]
public struct ItemData
{
    public string name;
    public int value;
    public string icon;
}


public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private Button singleDataSaveButton;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

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

    void OnEnable()
    {
        singleDataSaveButton.onClick.AddListener(async () =>
        {
            await SaveSingleData();
        });
    }

    public async Task SaveSingleData()
    {
        // 저장할 데이터 선언
        var data = new Dictionary<string, object>
        {
            {"player_name", "zackiller"},
            {"level", 20},
            {"xp", 2000},
            {"gold", 100}
        };

        // 저장 메소드
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("데이터 저장 완료!");
    }

}
