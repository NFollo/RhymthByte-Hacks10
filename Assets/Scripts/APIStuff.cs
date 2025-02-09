using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetImage : MonoBehaviour
{
    public RawImage image;
    private string link = "https://picsum.photos/2300/1300";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DownloadPicture());
    }

    IEnumerator DownloadPicture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(link);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        } else {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            image.texture = texture;
        }

    }
}