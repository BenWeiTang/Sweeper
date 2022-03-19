using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Minesweeper.Extensions
{
    public static class CustomLogger
    {
        private static string Color(this string str, string color)
        {
            return $"<color={color}>{str}</color>";
        }

        private static void DoLog(Action<string, Object> LogFunction, string prefix, Object obj, params object[] msg)
        {
#if UNITY_EDITOR
            var name = (obj ? obj.name : "NullObject").Color("lightblue");
            LogFunction($"{prefix}[{name}]: {String.Join("; ", msg)}\n", obj);
#endif
        }

        public static void Log(this Object obj, params object[] msg)
        {
            DoLog(Debug.Log, "", obj, msg);
        }

        public static void LogWarning(this Object obj, params object[] msg)
        {
            DoLog(Debug.LogWarning, "⚠️".Color("yellow"), obj, msg);
        }

        public static void LogError(this Object obj, params object[] msg)
        {
            DoLog(Debug.LogError, "<!>".Color("red"), obj, msg);
        }

        public static void LogSuccess(this Object obj, params object[] msg)
        {
            DoLog(Debug.Log, "☻".Color("green"), obj, msg);
        }
    }
}