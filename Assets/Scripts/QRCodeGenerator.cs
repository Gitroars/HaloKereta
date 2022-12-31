
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using TMPro;

//Reference: https://www.youtube.com/watch?v=6kTgBWvgT78
public class QRCodeGenerator : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageReceiver;

    private Texture2D _storeEncodedTexture;
    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions()
            {
                Height = height,
                Width = width
            }

        };
        return writer.Write(textForEncoding);
    }


}
