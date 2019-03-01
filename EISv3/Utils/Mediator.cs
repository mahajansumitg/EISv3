using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISv3.Utils
{
    static class Mediator
    {
        private static Dictionary<string, object> varDictionary = new Dictionary<string, object>();
        private static Dictionary<string, Action> actionDictionary = new Dictionary<string, Action>();
        public static void RegisterVar(string key, object obj)
        {
            if (varDictionary.ContainsKey(key)) varDictionary.Remove(key);
            varDictionary.Add(key, obj);
        }
        public static void RegisterAction(string key, Action action)
        {
            if (actionDictionary.ContainsKey(key)) actionDictionary.Remove(key);
            actionDictionary.Add(key, action);
        }
        public static object GetVar(string key)
        {
            varDictionary.TryGetValue(key, out object obj);
            return obj;
        }

        public static void PerformAction(string key)
        {
            actionDictionary.TryGetValue(key, out Action action);
            action.Invoke();
        }

        public static void RemoveVar(string key)
        {
            if(varDictionary.ContainsKey(key)) varDictionary.Remove(key);
        }

        public static void RemoveAction(string key)
        {
            if(actionDictionary.ContainsKey(key)) actionDictionary.Remove(key);
        }
    }
}
