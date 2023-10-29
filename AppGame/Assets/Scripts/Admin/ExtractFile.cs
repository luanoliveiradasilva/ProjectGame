using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ExtractFile : MonoBehaviour
{
    // Lista de dados que você deseja exportar para o CSV
    public List<string[]> data = new();
    
    public string fileName = "exported_data.csv";

    public void ExportDataToCSV()
    {
        string[][] output = new string[data.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = data[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder stringBuilder = new();

        for (int index = 0; index < length; index++)
            stringBuilder.AppendLine(string.Join(delimiter, output[index]));

        // save csv in documents in windows.
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        string filePath = Path.Combine(documentsPath, fileName);

        StreamWriter outStream = File.CreateText(filePath);

        outStream.WriteLine(stringBuilder);
        outStream.Close();
    }
    
}

