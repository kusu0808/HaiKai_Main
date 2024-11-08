using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SAudioClips", menuName = "SO/SAudioClips")]
    public sealed class SAudioClips : ScriptableObject
    {
        [SerializeField, Required, LabelText("BGM")]
        private AudioClipsBGM _bgm;
        public AudioClipsBGM BGM => _bgm;

        [SerializeField, Required, LabelText("Voice")]
        private AudioClipsVoice _voice;
        public AudioClipsVoice Voice => _voice;

        [SerializeField, Required, LabelText("SE")]
        private AudioClipsSE _se;
        public AudioClipsSE SE => _se;

        [SerializeField, Required, LabelText("SERough")]
        private AudioClipsSERough _seRough;
        public AudioClipsSERough SERough => _seRough;

        [Serializable]
        public sealed class AudioClipsBGM
        {
            [SerializeField, Required, AssetsOnly, LabelText("割れた皿の上を移動する")]
            private AudioClip _moveOnBrokenDish;
            public AudioClip MoveOnBrokenDish => _moveOnBrokenDish;

            [SerializeField, Required, AssetsOnly, LabelText("橋が軋む")]
            private AudioClip _bridgeCreak;
            public AudioClip BridgeCreak => _bridgeCreak;

            [SerializeField, Required, AssetsOnly, LabelText("家の中で、廊下を歩く")]
            private AudioClip _walkOnCorridorInHouse;
            public AudioClip WalkOnCorridorInHouse => _walkOnCorridorInHouse;

            [SerializeField, Required, AssetsOnly, LabelText("家の中で、畳を歩く")]
            private AudioClip _walkOnTatamiInHouse;
            public AudioClip WalkOnTatamiInHouse => _walkOnTatamiInHouse;
        }

        [Serializable]
        public sealed class AudioClipsVoice
        {
            [SerializeField, Required, AssetsOnly, LabelText("娘の悲鳴")]
            private AudioClip _daughterScream;
            public AudioClip DaughterScream => _daughterScream;

            [SerializeField, Required, AssetsOnly, LabelText("鹿の鳴き声")]
            private AudioClip _deerCry;
            public AudioClip DeerCry => _deerCry;

            [SerializeField, Required, AssetsOnly, LabelText("鳥の鳴き声")]
            private AudioClip _birdCry;
            public AudioClip BirdCry => _birdCry;
        }

        [Serializable]
        public sealed class AudioClipsSE
        {

            [SerializeField, Required, AssetsOnly, LabelText("皿が割れる")]
            private AudioClip _dishBreak;
            public AudioClip DishBreak => _dishBreak;

            [SerializeField, Required, AssetsOnly, LabelText("植物を切り開く")]
            private AudioClip _cutBigIvy;
            public AudioClip CutBigIvy => _cutBigIvy;

            [SerializeField, Required, AssetsOnly, LabelText("鹿が飛び出す")]
            private AudioClip _deerJumpOut;
            public AudioClip DeerJumpOut => _deerJumpOut;

            [SerializeField, Required, AssetsOnly, LabelText("鹿が逃げていく")]
            private AudioClip _deerRunAway;
            public AudioClip DeerRunAway => _deerRunAway;

            [SerializeField, Required, AssetsOnly, LabelText("鳥が飛び立つ")]
            private AudioClip _birdFlyAway;
            public AudioClip BirdFlyAway => _birdFlyAway;

            [SerializeField, Required, AssetsOnly, LabelText("家の中でヤツの気配がする(1回目)")]
            private AudioClip _feelingYatsuInHouse1;
            public AudioClip FeelingYatsuInHouse1 => _feelingYatsuInHouse1;

            [SerializeField, Required, AssetsOnly, LabelText("家の中でヤツの気配がする(2回目)")]
            private AudioClip _feelingYatsuInHouse2;
            public AudioClip FeelingYatsuInHouse2 => _feelingYatsuInHouse2;

            [SerializeField, Required, AssetsOnly, LabelText("家の中でヤツの気配がする(3回目)")]
            private AudioClip _feelingYatsuInHouse3;
            public AudioClip FeelingYatsuInHouse3 => _feelingYatsuInHouse3;
        }

        [Serializable]
        public sealed class AudioClipsSERough
        {

        }
    }
}