using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GlobalGameManager
{
    private static string dir = Application.persistentDataPath + "/UserData.txt";
    public static UserData thisUser = new UserData();
    private static int level1ID = 3;
    public static void SaveFile()
    {
        DataManager.SaveIntoJson(thisUser, dir);
    }
    public static void LoadFile()
    {
        thisUser = DataManager.LoadUserDataFromJson(dir);
    }
    public static void UpdateCurLevel(int finishedSceneID)
    {
        int finishedSceneLevel = finishedSceneID - level1ID + 1;
        thisUser.curLevel = Math.Max(finishedSceneLevel + 1, thisUser.curLevel);
    }
}
[System.Serializable]
public class UserData
{
    public string username = "USER";
    public int numberOfCoins = 0;
    public int curLevel = 1;
    public int curMaxLives = 3;
    public int curBladeDam = 40;
    public int curFireDam = 20;
}
