using AdventuresUnknownSDK.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Core.Utils
{
    [InitializeOnLoad]
    public class LoadCoreObjects
    {
        static LoadCoreObjects()
        {
            CoreObject[] cObjs = Resources.LoadAll<CoreObject>("");
            Debug.LogFormat("{0} CoreObjects loaded", cObjs.Length);
        }
    }
}
