﻿using System;
using UnityEngine;
using System.Collections;

public class AssetsManager : MonoBehaviour {
    
    public string BundleURL;
    public string AssetName;
    public int version;

    public IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;

            if (AssetName == "")
            {
                Instantiate(bundle.mainAsset);
            }
            else
            {   
                //get car
                GameObject gObj = (GameObject)bundle.LoadAsset(AssetName);
                //add input controller script and set params
                //InputController iCtrl = gObj.AddComponent<InputController>();
                //iCtrl.rotationSpeed = 15;
                //Instantiate(gObj);
            }
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
