using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISv3.Utils
{
    static class Mediator
    {
        private static Dictionary<string, Object> varDictionary = new Dictionary<string, Object>();
        private static Dictionary<string, Action> actionDictionary = new Dictionary<string, Action>();
        public static void registerVar(string key, Object obj)
        {
            varDictionary.Add(key, obj);
        }
        public static void registerAction(string key, Action action)
        {
            actionDictionary.Add(key, action);
        }
        public static Object getVar(string key)
        {
            Object obj;
            varDictionary.TryGetValue(key, out obj);
            return obj;
        }

        public static void performAction(string key)
        {
            Action action;
            actionDictionary.TryGetValue(key, out action);
            action.Invoke();
        }

        public static void removeVar(string key)
        {
            varDictionary.Remove(key);
        }

        public static void removeAction(string key)
        {
            actionDictionary.Remove(key);
        }
    }
}
