using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class StoreEthereumAddressMono : MonoBehaviour
{


    public string m_ethereumAddress;

    public UnityEvent<string> m_onEthereumAddressLoaded;
    public string m_subFolderName = "Default";
    void Awake()
    {
        LoadEthereumAddressStore();
    }

    
    public void OverrideEthereumAddress(string ethereumAddress)
    {

        EthereumAddressInPermaSubfolder.OverrideWithGivenAddress(m_subFolderName, ethereumAddress);
        LoadEthereumAddressStore();
    }

    [ContextMenu("Generate Random Public Private RSA Key")]
    private void LoadEthereumAddressStore()
    {
        EthereumAddressInPermaSubfolder.GetStoredEthereumAddress(m_subFolderName, out m_ethereumAddress);
        m_onEthereumAddressLoaded.Invoke(m_ethereumAddress);
    }

    [ContextMenu("Open folder with keys")]
    public void OpenFolderWithKeys()
    {
        Application.OpenURL(EthereumAddressInPermaSubfolder.GetDirectoryPathOfDevice(m_subFolderName));
    }

 

    public  void GetEthereumAddress(out string key)
    {
        if (string.IsNullOrEmpty(m_ethereumAddress))
            LoadEthereumAddressStore();

        key = m_ethereumAddress;
    }


    public void DebugLog(string message)
    {
        Debug.Log(message);

    }
    public void DebugLogEthereumAddress(string message)
    {
        Debug.Log("Ethereum Address: " + message);

    }
  
}


public static class EthereumAddressInPermaSubfolder
{


    public static void OverrideWithGivenAddress(string subFolderName, string addressEthereum)
    {
       
        string path = Path.Combine(Application.persistentDataPath, Path.Combine("EthereumAddress", subFolderName));
        string pathPublic = Path.Combine(path, "ETH_ADDRESS.txt");
        Directory.CreateDirectory(path);
        System.IO.File.WriteAllText(pathPublic, addressEthereum);

    }
    public static void GetStoredEthereumAddress(string subFolderName, out string addressEthereum)
    {
        string path = Path.Combine(Application.persistentDataPath, Path.Combine("EthereumAddress", subFolderName));
        string pathPublic = Path.Combine(path, "ETH_ADDRESS.txt");
        Directory.CreateDirectory(path);
        if (System.IO.File.Exists(pathPublic))
        {
            addressEthereum = System.IO.File.ReadAllText(pathPublic);
        }
        else
        {
            addressEthereum = "";
        }
    }

    public static string GetDirectoryPathOfDevice(string subFolderName = "Default")
    {
        return Path.Combine(Application.persistentDataPath, Path.Combine("KeyPair", subFolderName));
    }
}