using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Logging helper class
/// </summary>

public class Logger
{

    public static void Log(params object[] args)
    {
        var sb = new StringBuilder();
        foreach (var arg in args) 
        {
            sb.AppendFormat("{0}   ", (arg == null ? "null" : arg.ToString()));
        }
        Debug.Log(sb.ToString());
    }


    public static string DeepDescription(object obj)
    {
        StringBuilder sb = new StringBuilder();
        AppendObjectDescription(sb, obj, 0, null);
        return sb.ToString();
    }

    private static void AppendObjectDescription(StringBuilder sb, object obj, int indent, string bullet)
    {
        sb.AppendFormat("{0}{1} [{2}] : ", StringOfSpaces(indent), bullet, obj == null ? "null" : obj.GetType().ToString());
        int collectionIndent = indent + 4;
        if (obj is IDictionary<object,object>) 
        {
            AppendObjectDescription(sb, obj as IDictionary<object,object>, collectionIndent);
        } 
        else if (obj is IDictionary) 
        {
            AppendObjectDescription(sb, obj as IDictionary, collectionIndent);
        } 
        else if (obj is IList<object>) 
        {
            AppendObjectDescription(sb, obj as IList<object>, collectionIndent);
        } 
        else if (obj is IList) 
        {
            AppendObjectDescription(sb, obj as IList, collectionIndent);
        } 
        else if (obj != null && obj is System.Delegate) 
        {
            var del = obj as System.Delegate;
            sb.AppendFormat("{0} {1}", del.Target.ToString(), del.Method.Name);
        } 
        else 
        {
            sb.AppendFormat("{0}", obj == null ? "null" : obj.ToString());
        }
        sb.AppendLine();
    } 

    private static void AppendObjectDescription(StringBuilder sb, IDictionary<object,object> dict, int indent)
    {
        sb.AppendLine();
        foreach (var kvp in dict) 
        {
            AppendObjectDescription(sb, kvp.Key, indent, "Key:");
            AppendObjectDescription(sb, kvp.Value, indent, "Val:");
        }
    }

    private static void AppendObjectDescription(StringBuilder sb, IDictionary dict, int indent)
    {
        sb.AppendLine();
        foreach (DictionaryEntry de in dict) 
        {
            AppendObjectDescription(sb, de.Key, indent, "Key:");
            AppendObjectDescription(sb, de.Value, indent, "Val:");
        }
    }

    private static void AppendObjectDescription(StringBuilder sb, IList<object> list, int indent)
    {
        sb.AppendLine();
        int count = 0;
        foreach (var o in list) 
        {
            AppendObjectDescription(sb, o, indent, string.Format("{0}:", count));
            count++;
        }
    }

    private static void AppendObjectDescription(StringBuilder sb, IList list, int indent)
    {
        sb.AppendLine();
        int count = 0;
        foreach (var o in list) 
        {
            AppendObjectDescription(sb, o, indent, string.Format("{0}:", count));
            count++;
        }
    }

    private static string StringOfSpaces(int numberSpaces)
    {
        if (numberSpaces < 1) 
        {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberSpaces; i++) 
        {
            sb.Append(" ");
        };
        return sb.ToString();
    }
}
