using System;
using General;
using UnityEngine;

namespace SO
{
    public abstract class AScriptableObjectInResourcesFolder<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string Path = typeof(T).Name;

        private static T _entity = null;
        public static T Entity
        {
            get
            {
                if (_entity == null)
                {
                    try
                    {
                        _entity = Resources.Load<T>(Path);

                        if (_entity == null)
                        {
                            $"Failed to load Scriptable Object from {Path}".Warn();
                        }
                    }
                    catch (Exception e)
                    {
                        $"Failed to load Scriptable Object from {Path}: {e}".Warn();
                    }
                }

                return _entity;
            }
        }
    }
}