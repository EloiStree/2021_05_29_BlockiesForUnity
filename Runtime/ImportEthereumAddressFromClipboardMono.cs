using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class ImportEthereumAddressFromClipboardMono : MonoBehaviour
{

    public void GenerateVanishKey() { 
    
        Application.OpenURL("https://vanishkey.com/");
    }

    public string m_addressEthereum;
    public string m_privateKeyEthereum;

    public UnityEvent<string> m_onAddressEthereumDetected;
    public UnityEvent<string> m_onPrivateEthereumDetected;

    public string m_clipboardPreviousValue;
    public string m_clipboardCurrentValue;

    public void Start()
    {
        InvokeRepeating("CheckClipboard", 0, 1);
    }

    private void CheckClipboard()
    {
        if (Application.isFocused)
        {
            m_clipboardCurrentValue = GUIUtility.systemCopyBuffer;
            if (m_clipboardPreviousValue != m_clipboardCurrentValue)
            {
                m_clipboardPreviousValue = m_clipboardCurrentValue;
                if (m_clipboardCurrentValue.Length == 42 && m_clipboardCurrentValue.StartsWith("0x"))
                {
                    m_addressEthereum = m_clipboardCurrentValue;
                    m_onAddressEthereumDetected.Invoke(m_addressEthereum);
                }
                if (m_clipboardCurrentValue.Length == 40 && !m_clipboardCurrentValue.StartsWith("0x"))
                {
                    m_addressEthereum = m_clipboardCurrentValue;
                    m_onAddressEthereumDetected.Invoke(m_addressEthereum);
                }

                else if (m_clipboardCurrentValue.Length == 66 && m_clipboardCurrentValue.StartsWith("0x"))
                {
                    m_privateKeyEthereum = m_clipboardCurrentValue;
                    m_onPrivateEthereumDetected.Invoke(m_privateKeyEthereum);
                }
                else if (m_clipboardCurrentValue.Length == 64 && !m_clipboardCurrentValue.StartsWith("0x"))
                {
                    m_privateKeyEthereum = m_clipboardCurrentValue;
                    m_onPrivateEthereumDetected.Invoke(m_privateKeyEthereum);
                }
                else
                {
                }

            }
        }
    }
}
