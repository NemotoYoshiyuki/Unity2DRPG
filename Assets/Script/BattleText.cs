using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BattleText
{
    private const string filePath = "Assets/Resources/BattleText.csv";
    private static Language language = Language.japan;
    private static Dictionary<string, string> dictionary;

    public enum Language
    {
        japan,
        english
    }

    public static string Get(string key)
    {
        if (dictionary == null)
        {
            dictionary = CsvToDictionary("key", language.ToString());
        }

        return dictionary[key];
    }

    private static Dictionary<string, string> CsvToDictionary(string key1, string key2)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string csv = GetCsvText();

        string[] lines = csv.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        List<string> headers = lines[0].Split(',').ToList();

        int key1Index = headers.FindIndex(x => x == key1);
        int key2index = headers.FindIndex(x => x == key2);

        foreach (var line in lines.Skip(1))
        {
            var value = line.Split(',');
            dictionary.Add(value[key1Index], value[key2index]);
        }
        return dictionary;
    }

    private static string GetCsvText(string filePath = filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new System.Exception("File dose not exit :" + filePath);
        }
        else if (Path.GetExtension(filePath) != ".csv")
        {
            throw new System.Exception("Not a .csv file :" + filePath);
        }
        return File.ReadAllText(filePath); ;
    }



    private String CsvToJson()
    {
        string csv = GetCsvText(filePath);
        string json = string.Empty;

        string[] lines = csv.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        string[] headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] line = lines[i].Split(',');
            string value = string.Empty;

            for (int j = 0; j < headers.Length; j++)
            {
                value += $"\"{headers[j]}\": \"{line[j]}\"";
                if (j != line.Length - 1) value += ",";
            }
            json += "{" + value + "}" + "\n";
            if (i != lines.Length - 1) json += ",";
        }

        //配列の場合ヘッダーを含める
        if (lines.Length >= 2)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(json);
            sb.Insert(0, "[");
            sb.Insert(sb.Length, "]");
            return sb.ToString();
        }
        return json;
    }

    //jsonを整形します
    private string JsonFormatting(string jsonText)
    {
        return jsonText;
    }

    //https://www.sejuku.net/blog/80617
}
