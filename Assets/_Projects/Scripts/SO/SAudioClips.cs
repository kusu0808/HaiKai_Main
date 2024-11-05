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
            [SerializeField, Required, AssetsOnly, LabelText("タイトル")]
            private AudioClip _title;
            public AudioClip Title => _title;

            [SerializeField, Required, AssetsOnly, LabelText("メイン(チャプター1)")]
            private AudioClip _mainChapter1;
            public AudioClip MainChapter1 => _mainChapter1;

            [SerializeField, Required, AssetsOnly, LabelText("メイン(チャプター2)")]
            private AudioClip _mainChapter2;
            public AudioClip MainChapter2 => _mainChapter2;

            [SerializeField, Required, AssetsOnly, LabelText("メイン(チャプター3)")]
            private AudioClip _mainChapter3;
            public AudioClip MainChapter3 => _mainChapter3;

            [SerializeField, Required, AssetsOnly, LabelText("水面が揺れる")]
            private AudioClip _waterSurfaceWave;
            public AudioClip WaterSurfaceWave => _waterSurfaceWave;

            [SerializeField, Required, AssetsOnly, LabelText("バスが停車中")]
            private AudioClip _busStop;
            public AudioClip BusStop => _busStop;
        }

        [Serializable]
        public sealed class AudioClipsVoice
        {
            [SerializeField, Required, AssetsOnly, LabelText("娘の息遣い")]
            private AudioClip _daughterBreath;
            public AudioClip DaughterBreath => _daughterBreath;

            [SerializeField, Required, AssetsOnly, LabelText("娘の悲鳴")]
            private AudioClip _daughterScream;
            public AudioClip DaughterScream => _daughterScream;

            [SerializeField, Required, AssetsOnly, LabelText("娘の抵抗する声")]
            private AudioClip _daughterResist;
            public AudioClip DaughterResist => _daughterResist;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの息遣い")]
            private AudioClip _yatsuBreath;
            public AudioClip YatsuBreath => _yatsuBreath;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツのうめき声")]
            private AudioClip _yatsuGroan;
            public AudioClip YatsuGroan => _yatsuGroan;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの咆哮")]
            private AudioClip _yatsuRoar;
            public AudioClip YatsuRoar => _yatsuRoar;

            // その他、各種ボイス
        }

        [Serializable]
        public sealed class AudioClipsSE
        {
            [SerializeField, Required, AssetsOnly, LabelText("決定")]
            private AudioClip _submit;
            public AudioClip Submit => _submit;

            [SerializeField, Required, AssetsOnly, LabelText("キャンセル")]
            private AudioClip _cancel;
            public AudioClip Cancel => _cancel;

            [SerializeField, Required, AssetsOnly, LabelText("ポーズ")]
            private AudioClip _pause;
            public AudioClip Pause => _pause;

            [SerializeField, Required, AssetsOnly, LabelText("決定できない")]
            private AudioClip _cannotSubmit;
            public AudioClip CannotSubmit => _cannotSubmit;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムを取得")]
            private AudioClip _getItem;
            public AudioClip GetItem => _getItem;

            [SerializeField, Required, AssetsOnly, LabelText("バスが発車する")]
            private AudioClip _busBeginMove;
            public AudioClip BusBeginMove => _busBeginMove;

            [SerializeField, Required, AssetsOnly, LabelText("風切り音")]
            private AudioClip _windCut;
            public AudioClip WindCut => _windCut;
        }

        [Serializable]
        public sealed class AudioClipsSERough
        {
            [SerializeField, Required, AssetsOnly, LabelText("道路の上を歩く")]
            private AudioClip _walkOnRoad;
            public AudioClip WalkOnRoad => _walkOnRoad;

            [SerializeField, Required, AssetsOnly, LabelText("道路の上を走る")]
            private AudioClip _runOnRoad;
            public AudioClip RunOnRoad => _runOnRoad;

            [SerializeField, Required, AssetsOnly, LabelText("小道の上を歩く")]
            private AudioClip _walkOnPathWay;
            public AudioClip WalkOnPathWay => _walkOnPathWay;

            [SerializeField, Required, AssetsOnly, LabelText("小道の上を走る")]
            private AudioClip _runOnPathWay;
            public AudioClip RunOnPathWay => _runOnPathWay;

            [SerializeField, Required, AssetsOnly, LabelText("家の中を歩く")]
            private AudioClip _walkInHouse;
            public AudioClip WalkInHouse => _walkInHouse;

            [SerializeField, Required, AssetsOnly, LabelText("家の中を走る")]
            private AudioClip _runInHouse;
            public AudioClip RunInHouse => _runInHouse;

            [SerializeField, Required, AssetsOnly, LabelText("洞窟の中を歩く")]
            private AudioClip _walkInCave;
            public AudioClip WalkInCave => _walkInCave;

            [SerializeField, Required, AssetsOnly, LabelText("洞窟の中を走る")]
            private AudioClip _runInCave;
            public AudioClip RunInCave => _runInCave;

            [SerializeField, Required, AssetsOnly, LabelText("小道に飛び降りる")]
            private AudioClip _jumpOffPathWay;
            public AudioClip JumpOffPathWay => _jumpOffPathWay;

            [SerializeField, Required, AssetsOnly, LabelText("娘の歩く音")]
            private AudioClip _daughterWalk;
            public AudioClip DaughterWalk => _daughterWalk;

            [SerializeField, Required, AssetsOnly, LabelText("娘の走る音")]
            private AudioClip _daughterRun;
            public AudioClip DaughterRun => _daughterRun;
        }
    }
}