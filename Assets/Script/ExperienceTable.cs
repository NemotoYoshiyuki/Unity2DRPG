using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceTable
{
    private static List<int> experienceTable = null;
    public static List<int> Get()
    {
        if (experienceTable == null) experienceTable = Create();
        return experienceTable;
    }

    private static List<int> Create()
    {
        List<int> table = new List<int>(new int[100]);
        float exp = 10;
        float expMod = 1.2f;
        for (int i = 1; i < 100; i++)
        {
            exp = exp * expMod;
            table[i] = (int)exp;
        }
        return table;
    }
}