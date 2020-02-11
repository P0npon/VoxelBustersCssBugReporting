﻿using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using VoxelBusters.NativePlugins;

public class SkmManager : NavigationParent
{
    [SerializeField]
    WebView webview;
    string dataPath;
    string url;

    public override Action<NavBarButton, bool> setEnableNavbarButton { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "SimpleHTMLWebsite");
        StartCoroutine("TransferAndUnzip");
    }

    IEnumerator TransferAndUnzip()
    {
        string fromPath = Path.Combine(Application.persistentDataPath, "SimpleHTMLWebsite.zip");
        string toPath = Path.Combine(Application.persistentDataPath, "SimpleHTMLWebsite");

        WWW www = new WWW(Path.Combine(Application.streamingAssetsPath, "SimpleHTMLWebsite.zip"));

        while (!www.isDone)
        {
            Debug.Log("www creation...");
        }

        File.WriteAllBytes(fromPath, www.bytes);

        byte[] result = null;

        yield return new WaitForSeconds(0.1f);

        ExtractZipFile(www.bytes, Application.persistentDataPath);


        int timeOut = 0;

        while (!Directory.Exists(toPath) && timeOut < 1000)
        {
            timeOut++;
        }
        url = Path.Combine(dataPath, "example.html");

        StartWebview();
    }

    public void ExtractZipFile(byte[] data, string outFolder)
    {
        ZipFile zf = null;
        try
        {
            //use MemoryStream!!!!
            using (var mstrm = new MemoryStream(data))
            {
                zf = new ZipFile(mstrm);

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;
                    }

                    String entryFileName = zipEntry.Name;
                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    Debug.Log("fullZipToPath = " + fullZipToPath + " , outfolder = " + outFolder + " entryFileName = " + entryFileName + " PDP = " + Application.persistentDataPath);

                    string directoryName = Path.GetDirectoryName(fullZipToPath);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);
        }
        finally
        {
            if (zf != null)
            {
                zf.IsStreamOwner = true;
                zf.Close();
            }
        }
    }


    public void StartWebview()
    {
        webview.Show();
        webview.LoadHTMLStringContentsOfFile(url,null);
        webview.Frame = new Rect(0, 0, Screen.width, Screen.height * 0.9f);
    }

    public override void Next()
    {
        throw new NotImplementedException();
    }

    public override void PlayPause()
    {
        throw new NotImplementedException();
    }

    public override void Previous()
    {
        throw new NotImplementedException();
    }
}
