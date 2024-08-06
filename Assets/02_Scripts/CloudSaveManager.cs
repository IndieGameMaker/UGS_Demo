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
    [SerializeField] private Button multiDataSaveButton;

    [SerializeField] private Button singleDataLoadButton;
    [SerializeField] private Button multiDataLoadButton;

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

        multiDataSaveButton.onClick.AddListener(async () =>
        {
            await SaveMultiData<PlayerData>("PlayerDataAndItems", playerData);
        });

        singleDataLoadButton.onClick.AddListener(async () => await LoadData());
        multiDataLoadButton.onClick.AddListener(async () => await LoadData<PlayerData>("PlayerDataAndItems"));
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

    public async Task SaveMultiData<T>(string key, T saveData)
    {
        // 딕셔너리 자료형에 저장
        var data = new Dictionary<string, object>
        {
            {key, saveData}
        };

        // 저장
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("멀티 데이터 저장 완료");
    }

    // 싱글 데이터 로드
    #region 싱글데이터 로드
    /*
    HashSet<T>
    1. 중복 허용하지 않음
    2. 속도가 빠르다.
    3. TryGetValue 메소드로 값을 읽을수 있다.
    */

    public async Task LoadData()
    {
        var prams = new HashSet<string> { "player_name", "level", "xp" };
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(prams);

        if (data.TryGetValue("player_name", out var playerName))
        {
            Debug.Log("PlayerName : " + playerName.Value.GetAs<string>());
        }
        if (data.TryGetValue("level", out var level))
        {
            Debug.Log("Level : " + level.Value.GetAs<int>());
        }
    }
    #endregion

    // 멀티 데이터 로드
    #region 멀티 데이터 로드
    public async Task LoadData<T>(string key)
    {
        var pram = new HashSet<string> { key };
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(pram);

        playerData = new PlayerData();

        await Task.Delay(2000);

        if (data.TryGetValue(key, out var loadData))
        {
            playerData = loadData.Value.GetAs<PlayerData>();
        }
    }
    #endregion
}
