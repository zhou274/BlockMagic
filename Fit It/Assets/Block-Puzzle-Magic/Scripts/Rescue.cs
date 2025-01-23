using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;
using UnityEngine.Analytics;


public class Rescue : MonoBehaviour 
{
	[SerializeField]
	private Button btnWatchVideo;

	[SerializeField]
	private Text txtRescueReason;
    public string clickid;
    private StarkAdManager starkAdManager;

    void OnEnable() 
	{
        AdManager.OnRewardedFinishedEvent += Instance_OnRewardedFinished;

		switch (GamePlayUI.Instance.currentGameOverReson) {
		case GameOverReason.OUT_OF_MOVES:
			txtRescueReason.SetLocalizedTextForTag ("txt-out-moves");
			break;
		case GameOverReason.BOMB_COUNTER_ZERO:
			txtRescueReason.SetLocalizedTextForTag ("txt-bomb-blast");
			break;
		case GameOverReason.TIME_OVER:
			txtRescueReason.SetLocalizedTextForTag ("txt-time-over");
			break;
		}


		if(btnWatchVideo != null)
		{
			//Init this with ad network's status of ad is available or not.
			//bool isAdsAvailable = false;

			//if(isAdsAvailable &&  GamePlay.Instance.isFreeRescueAvailable())
			//{
			//	btnWatchVideo.interactable = true;
			//	btnWatchVideo.GetComponent<CanvasGroup>().alpha = 1F;
			//} else {
			//	btnWatchVideo.interactable = false;
			//	btnWatchVideo.GetComponent<CanvasGroup>().alpha = 0.3F;
			//}
		}
    }

    void Instance_OnRewardedFinished(bool result)
    {
        if (result == true)
        {
            GamePlay.Instance.OnRescueDone(true);
            gameObject.Deactivate();
        }
    }


    void OnDisable() {
        AdManager.OnRewardedFinishedEvent -= Instance_OnRewardedFinished;
    }

    public void OnCloseButtonPressed()
	{
		if (InputManager.Instance.canInput ()) {
			AudioManager.Instance.PlayButtonClickSound ();
			GamePlay.Instance.OnGameOver();
			gameObject.Deactivate();
		}
        ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
    }

	public void OnRescueUsingWatchVideo()
	{
		if (InputManager.Instance.canInput ()) 
		{
			//CALL YOUR AD NETWORK VIDEO AD HERE TO RESCUE USING WATCH VIDEO.
		}
	}

	public void OnRescueUsingCoins()
	{
		//GamePlay.Instance.OnRescueDone(false);
		//gameObject.Deactivate();
		if (InputManager.Instance.canInput())
		{
			bool coinDeduced = CurrencyManager.Instance.deductBalance(200);

			if (coinDeduced)
			{
				GamePlay.Instance.OnRescueDone(false);
				gameObject.Deactivate();
			}
			else
			{
				Debug.Log("金币不足");
			}
			//else
			//{
			//	StackManager.Instance.shopScreen.Activate();
			//}
		}
	}
	public void OnRescueByWatch()
	{
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    GamePlay.Instance.OnRescueDone(false);
                    gameObject.Deactivate();


                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
        
    }
    public void getClickid()
    {
        var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
        if (launchOpt.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                if (kv.Value != null)
                {
                    Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                    if (kv.Key.ToString() == "clickid")
                    {
                        clickid = kv.Value.ToString();
                    }
                }
                else
                {
                    Debug.Log(kv.Key + "<-参数-> " + "null ");
                }
        }
    }

    public void apiSend(string eventname, string clickid)
    {
        TTRequest.InnerOptions options = new TTRequest.InnerOptions();
        options.Header["content-type"] = "application/json";
        options.Method = "POST";

        JsonData data1 = new JsonData();

        data1["event_type"] = eventname;
        data1["context"] = new JsonData();
        data1["context"]["ad"] = new JsonData();
        data1["context"]["ad"]["callback"] = clickid;

        Debug.Log("<-data1-> " + data1.ToJson());

        options.Data = data1.ToJson();

        TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
           response => { Debug.Log(response); },
           response => { Debug.Log(response); });
    }


    /// <summary>
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="closeCallBack"></param>
    /// <param name="errorCallBack"></param>
    public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
        }
    }
    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="errorCallBack"></param>
    /// <param name="closeCallBack"></param>
    public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
            mInterstitialAd.Load();
            mInterstitialAd.Show();
        }
    }
}
