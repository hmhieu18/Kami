using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
public class DataManager : MonoBehaviour
{
    // [SerializeField] private UserData userData = new UserData();

    public static void SaveIntoJson(UserData userData, string dir)
    {
        Debug.Log(dir);
        string userDataString = JsonUtility.ToJson(userData);
        Debug.Log(userDataString);
        string encryptedString = AesOperation.AESEncryption(userDataString);
        System.IO.File.WriteAllText(dir, encryptedString);
    }
    public static UserData LoadUserDataFromJson(string dir)
    {
        Debug.Log(dir);
        UserData userData = new UserData();
        if (System.IO.File.Exists(dir))
        {
            string data = AesOperation.AESDecryption(System.IO.File.ReadAllText(dir));
            userData = JsonUtility.FromJson<UserData>(data);
        }
        return userData;
    }
}
public static class AesOperation
{
    static string key = "A60A5770FE5E7AB200BA9CFC94E4E8B0";
    static string iv = "1234567887654321";

    //AES - Encription 
    public static string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    //AES -  Decryption
    public static string AESDecryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Convert.FromBase64String(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }
}

