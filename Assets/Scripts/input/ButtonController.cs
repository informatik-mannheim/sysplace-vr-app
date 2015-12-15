using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

    public void onClickEvent()
    {
        //load model on the fly
        AssetsManager assetsManager = gameObject.GetComponent<AssetsManager>();
        StartCoroutine(assetsManager.DownloadAndCache());
    }
}
