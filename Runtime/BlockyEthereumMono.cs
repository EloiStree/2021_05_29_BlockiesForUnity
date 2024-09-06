using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//This code is an adaptation of this code
//https://github.com/ethereum/blockies/blob/master/blockies.js
//Modify by scelts for C# with Form app
//https://github.com/scelts/eth-blockies.net/blob/master/ETH_Identicons/Identicon.cs
//Modify by EloiStre for beeing used in Unity
//I am not at all the creator of this code. I just modified it.

public class BlockyEthereumMono : MonoBehaviour
{
    public string m_address;
    public uint m_defaultsize = 8;
    public uint m_defaultScale = 8;
    public Texture2D m_texture;
    public UnityEvent<Texture2D> m_onBlockyChanged;

    private void Awake()
    {
        SetAddress(m_address);
    }

    public void SetAddress(string address) {
        m_address = address;

        m_texture = BlockiesUtility.GetAsDefaultFor(m_address, m_defaultsize, m_defaultScale);
        m_onBlockyChanged.Invoke(m_texture);
    }
    public Texture2D GetTextureGenerated() {

        return m_texture;
    }
}


public class Blockies
{

    public Blockies(string addressAsSeed, int blockSize = 8)
    {
        m_sizeOfBlock = blockSize;
        createImageData(addressAsSeed);
    }

    public Texture2D GetTexture2D(int resolution = 64)
    {
        int Scale = resolution / m_sizeOfBlock;
        return CreateEthereumIcon(Scale);
    }


    private Int32[] m_randseed = new Int32[4];
    private Color[] m_iconPixels;
    private int m_sizeOfBlock;

    private void GenerateRandomSeedBasedOnAddress(string addressAsSeed)
    {
        char[] seedArray = addressAsSeed.ToCharArray();
        for (int i = 0; i < m_randseed.Length; i++)
            m_randseed[i] = 0;
        for (int i = 0; i < addressAsSeed.Length; i++)
            m_randseed[i % 4] = ((m_randseed[i % 4] << 5) - m_randseed[i % 4]) + seedArray[i];
    }

    private double GetRandomBasedOnSeed()
    {
        var t = m_randseed[0] ^ (m_randseed[0] << 11);

        m_randseed[0] = m_randseed[1];
        m_randseed[1] = m_randseed[2];
        m_randseed[2] = m_randseed[3];
        m_randseed[3] = (m_randseed[3] ^ (m_randseed[3] >> 19) ^ t ^ (t >> 8));
        return Convert.ToDouble(m_randseed[3]) / Convert.ToDouble((UInt32)1 << 31);
    }

    private double HUE2RGB(double p, double q, double t)
    {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1D / 6) return p + (q - p) * 6 * t;
        if (t < 1D / 2) return q;
        if (t < 2D / 3) return p + (q - p) * (2D / 3 - t) * 6;
        return p;
    }

    private Color HSL2RGB(double h, double s, double l)
    {
        double r, g, b;
        if (s == 0)
        {
            r = g = b = l; // achromatic
        }
        else
        {
            var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            var p = 2 * l - q;
            r = HUE2RGB(p, q, h + 1D / 3);
            g = HUE2RGB(p, q, h);
            b = HUE2RGB(p, q, h - 1D / 3);
        }
        return new Color((float)r, (float)g, (float)b);
    }

    private Color createColor()
    {
        var h = (GetRandomBasedOnSeed());
        var s = ((GetRandomBasedOnSeed() * 0.6) + 0.4);
        var l = ((GetRandomBasedOnSeed() + GetRandomBasedOnSeed() + GetRandomBasedOnSeed() + GetRandomBasedOnSeed()) * 0.25);
        return HSL2RGB(h, s, l);
    }

    private void createImageData(string addressAsSeed)
    {
        GenerateRandomSeedBasedOnAddress(addressAsSeed.ToLower());
        var mainColor = createColor();
        var bgColor = createColor();
        var spotColor = createColor();

        int width = m_sizeOfBlock;
        int height = m_sizeOfBlock;

        int mirrorWidth = width / 2;
        int dataWidth = width - mirrorWidth;
        double[] data = new double[width * height];
        for (int y = 0; y < height; y++)
        {
            double[] row = new double[dataWidth];
            for (int x = 0; x < dataWidth; x++)
            {
                row[x] = Mathf.Floor((float)(GetRandomBasedOnSeed() * 2.3));
            }
            Array.Copy(row, 0, data, y * width, dataWidth);
            Array.Copy(row.Reverse().ToArray(), 0, data, y * width + dataWidth, mirrorWidth);
        }

        m_iconPixels = new Color[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == 1)
            {
                m_iconPixels[i] = mainColor;
            }
            else if (data[i] == 0)
            {
                m_iconPixels[i] = bgColor;
            }
            else
            {
                m_iconPixels[i] = spotColor;
            }
        }
    }

    public Texture2D CreateEthereumIcon(int scale = 8)
    {
        Texture2D resultImage = new Texture2D(m_sizeOfBlock * scale, m_sizeOfBlock * scale);
        int height = resultImage.height - 1;
        for (int i = 0; i < m_iconPixels.Length; i++)
        {
            int x = i % m_sizeOfBlock;
            int y = i / m_sizeOfBlock;
            for (int xx = x * scale; xx < x * scale + scale; xx++)
            {
                for (int yy = y * scale; yy < y * scale + scale; yy++)
                {
                    resultImage.SetPixel(xx, height - yy, m_iconPixels[i]);
                }
            }
        }
        resultImage.Apply();
        return resultImage;
    }

  
}


public class BlockiesUtility
{
    public static Texture2D GetAsDefaultFor(string address)
    {
        Blockies blockies = new Blockies(address);
        return blockies.CreateEthereumIcon();
    }

    public static Texture2D GetAsDefaultFor(string address, uint blockSize, uint scale = 8)
    {
        Blockies blockies = new Blockies(address, (int)blockSize);
        return blockies.CreateEthereumIcon((int)scale);
    }
}