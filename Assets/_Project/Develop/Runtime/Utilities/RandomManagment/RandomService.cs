using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomService
{
    public char GetRandomCharFrom(string text)
    {
        int index = Random.Range(0, text.Length);
        return text[index];
    }

    public List<string> GenerateFrom(string text)
    {
        int quantity = Random.Range(3, 6);
        List<string> result = new List<string>();

        for (int i = 0; i < quantity; i++)
        {
            int index = Random.Range(0, text.Length);
            result.Add(text[index].ToString());
        }

        return result;
    }

    public List<string> GenerateFrom(string text, int quantity)
    {
        List<string> result = new List<string>();

        for (int i = 0; i < quantity; i++)
        {
            int index = Random.Range(0, text.Length);
            result.Add(text[index].ToString());
        }

        return result;
    }
}
