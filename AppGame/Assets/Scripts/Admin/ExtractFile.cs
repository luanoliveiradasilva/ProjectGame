using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ExtractFile : MonoBehaviour
{
    [Header("Name file CSV")]
    [Tooltip("csv file name")]
    [SerializeField] private string fileName;

    private readonly List<string[]> dataList = new();

    public void ExportDataToCSV()
    {
        foreach (var item in Player.instancePlayer.playerDataList)
        {
            string[] playerDataListArray =
            {
                    item.namePlayer,
                    item.playerScore.ToString()
            };

            dataList.Add(playerDataListArray);
        }

        string[][] output = new string[dataList.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = dataList[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder stringBuilder = new();

        for (int index = 0; index < length; index++)
            stringBuilder.AppendLine(string.Join(delimiter, output[index]));

        // save csv in documents in windows.
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //Combines local address plus file name
        string filePath = Path.Combine(documentsPath, fileName);

        StreamWriter outStream = File.CreateText(filePath);

        outStream.WriteLine(stringBuilder);
        outStream.Close();
    }
}

