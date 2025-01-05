using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class TimelineClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private GameObject _root;

        [SerializeField, Required, SceneObjectsOnly]
        private PlayableDirector _playableDirector;
    }
}