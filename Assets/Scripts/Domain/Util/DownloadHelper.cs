using System;
using System.Collections;
using UnityEngine;

public class DownloadHelper : MonoBehaviour {

    public static void CallDownloadCsv(DataType type, string url, Action<DataType, string> onComplete){
        CoroutineHandler.StartStaticCoroutine(DownloadCsv(type, url, (_type, _response) => onComplete(_type, _response)));
    }

    private static IEnumerator DownloadCsv (DataType type, string url, Action<DataType, string> onComplete)
    {
        WWW response = new WWW(url);
        yield return response;
        if (!string.IsNullOrEmpty(response.error))
        {
            Debug.LogWarning("エラー:" + response.error);
        }
        else
        {
            onComplete(type, response.text);
        }
	}

}
