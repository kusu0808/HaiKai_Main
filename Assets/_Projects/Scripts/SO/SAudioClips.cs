using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace SO
{
    [CreateAssetMenu(fileName = "SAudioClips", menuName = "SO/SAudioClips")]
    public sealed class SAudioClips : AScriptableObjectInResourcesFolder<SAudioClips>
    {
        [SerializeField, Required, AssetsOnly]
        private AudioMixer _audioMixer;
        public AudioMixer AudioMixer => _audioMixer;

        [SerializeField, Required, AssetsOnly]
        private AudioMixerGroup _masterMixerGroup;
        public AudioMixerGroup MasterMixerGroup => _masterMixerGroup;

        [SerializeField, Required, AssetsOnly]
        private AudioMixerGroup _bgmMixerGroup;
        public AudioMixerGroup BGMMixerGroup => _bgmMixerGroup;

        [SerializeField, Required, AssetsOnly]
        private AudioMixerGroup _seMixerGroup;
        public AudioMixerGroup SEMixerGroup => _seMixerGroup;

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

            [SerializeField, Required, AssetsOnly, LabelText("環境音、山の風")]
            private AudioClip _natureSoundWindOfMountain;
            public AudioClip NatureSoundWindOfMountain => _natureSoundWindOfMountain;

            [SerializeField, Required, AssetsOnly, LabelText("環境音、水滴")]
            private AudioClip _natureSoundDropOfWater;
            public AudioClip NatureSoundDropOfWater => _natureSoundDropOfWater;

            [SerializeField, Required, AssetsOnly, LabelText("移動、割れた皿")]
            private AudioClip _walkOnBrokenDish;
            public AudioClip WalkOnBrokenDish => _walkOnBrokenDish;

            [SerializeField, Required, AssetsOnly, LabelText("移動、アスファルト")]
            private AudioClip _walkOnAsphalt;
            public AudioClip WalkOnAsphalt => _walkOnAsphalt;

            [SerializeField, Required, AssetsOnly, LabelText("移動、土")]
            private AudioClip _walkOnSoil;
            public AudioClip WalkOnSoil => _walkOnSoil;

            [SerializeField, Required, AssetsOnly, LabelText("移動、廊下")]
            private AudioClip _walkOnCorridor;
            public AudioClip WalkOnCorridor => _walkOnCorridor;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツがトイレのドアを叩く")]
            private AudioClip _yatsuKnockToiletDoor;
            public AudioClip YatsuKnockToiletDoor => _yatsuKnockToiletDoor;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツに追われている")]
            private AudioClip _chasedByYatsu;
            public AudioClip ChasedByYatsu => _chasedByYatsu;
        }

        [Serializable]
        public sealed class AudioClipsVoice
        {
            [SerializeField, Required, AssetsOnly, LabelText("娘の悲鳴")]
            private AudioClip _daughterScream;
            public AudioClip DaughterScream => _daughterScream;

            [SerializeField, Required, AssetsOnly, LabelText("娘が攫われる")]
            private AudioClip _daughterKidnapped;
            public AudioClip DaughterKidnapped => _daughterKidnapped;

            [SerializeField, Required, AssetsOnly, LabelText("娘そっちじゃない")]
            private AudioClip _daughterMistakenDirection;
            public AudioClip DaughterMistakenDirection => _daughterMistakenDirection;

            [SerializeField, Required, AssetsOnly, LabelText("娘お地蔵さん")]
            private AudioClip _daughterOjizou;
            public AudioClip DaughterOjizou => _daughterOjizou;

            [SerializeField, Required, AssetsOnly, LabelText("娘先に行って")]
            private AudioClip _daughterRequestGoing;
            public AudioClip DaughterRequestGoing => _daughterRequestGoing;

            [SerializeField, Required, AssetsOnly, LabelText("娘呼び止める")]
            private AudioClip _daughterCall;
            public AudioClip DaughterCall => _daughterCall;

            [SerializeField, Required, AssetsOnly, LabelText("鹿の鳴き声")]
            private AudioClip _deerCry;
            public AudioClip DeerCry => _deerCry;

            [SerializeField, Required, AssetsOnly, LabelText("鳥の鳴き声")]
            private AudioClip _birdCry;
            public AudioClip BirdCry => _birdCry;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツが娘の声真似をする")]
            private AudioClip _yatsuImitateDaughterVoice;
            public AudioClip YaTsuImitateDaughterVoice => _yatsuImitateDaughterVoice;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツが娘の声真似をする2")]
            private AudioClip _yatsuImitateDaughterVoiceSecond;
            public AudioClip YaTsuImitateDaughterVoiceSecond => _yatsuImitateDaughterVoiceSecond;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツが娘の声真似をする3")]
            private AudioClip _yatsuImitateDaughterVoiceSecondThird;
            public AudioClip YaTsuImitateDaughterVoiceSecondThird => _yatsuImitateDaughterVoiceSecondThird;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツ雄たけび")]
            private AudioClip _yatsuShoutingVoice;
            public AudioClip YaTsuShoutingVoice => _yatsuShoutingVoice;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツガラス踏んだ時")]
            private AudioClip _yatsuDamagedVoice;
            public AudioClip YaTsuDamagedVoice => _yatsuDamagedVoice;

        }

        [Serializable]
        public sealed class AudioClipsSE
        {
            [SerializeField, Required, AssetsOnly, LabelText("落下して着地した")]
            private AudioClip _grounded;
            public AudioClip Grounded => _grounded;

            [SerializeField, Required, AssetsOnly, LabelText("皿が割れる")]
            private AudioClip _dishBreak;
            public AudioClip DishBreak => _dishBreak;

            [SerializeField, Required, AssetsOnly, LabelText("鹿が飛び出す")]
            private AudioClip _deerJumpOut;
            public AudioClip DeerJumpOut => _deerJumpOut;

            [SerializeField, Required, AssetsOnly, LabelText("鹿が逃げていく")]
            private AudioClip _deerRunAway;
            public AudioClip DeerRunAway => _deerRunAway;

            [SerializeField, Required, AssetsOnly, LabelText("鳥が飛び立つ")]
            private AudioClip _birdFlyAway;
            public AudioClip BirdFlyAway => _birdFlyAway;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの気配、家の中(1回目)")]
            private AudioClip _feelingYatsuInHouse1;
            public AudioClip FeelingYatsuInHouse1 => _feelingYatsuInHouse1;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの気配、家の中(2回目)")]
            private AudioClip _feelingYatsuInHouse2;
            public AudioClip FeelingYatsuInHouse2 => _feelingYatsuInHouse2;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの気配、家の中(3回目)")]
            private AudioClip _feelingYatsuInHouse3;
            public AudioClip FeelingYatsuInHouse3 => _feelingYatsuInHouse3;

            [SerializeField, Required, AssetsOnly, LabelText("ヤツの気配、舞台")]
            private AudioClip _feelingYatsuInButai;
            public AudioClip FeelingYatsuInButai => _feelingYatsuInButai;

            [SerializeField, Required, AssetsOnly, LabelText("乗り物、バス出発")]
            private AudioClip _vehicleSoundBusBeginToMove;
            public AudioClip VehicleSoundBusBeginToMove => _vehicleSoundBusBeginToMove;

            [SerializeField, Required, AssetsOnly, LabelText("乗り物、バスのドアの開閉")]
            private AudioClip _vehicleSoundBusDoor;
            public AudioClip VehicleSoundBusDoor => _vehicleSoundBusDoor;

            [SerializeField, Required, AssetsOnly, LabelText("乗り物、車が通りすぎる")]
            private AudioClip _vehicleSoundPassingCar;
            public AudioClip VehicleSoundPassingCar => _vehicleSoundPassingCar;

            [SerializeField, Required, AssetsOnly, LabelText("アイテム入手")]
            private AudioClip _itemGetSound;
            public AudioClip ObtainItem => _itemGetSound;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムナイフでツタを切る")]
            private AudioClip _cutBigIvy;
            public AudioClip CutBigIvy => _cutBigIvy;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムナイフで鹿を切る")]
            private AudioClip _cutDeerNeck;
            public AudioClip CutDeerNeck => _cutDeerNeck;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムナイフで鎖を切る"), FormerlySerializedAs("_cutShionChain")]
            private AudioClip _cutDaugherChain;
            public AudioClip CutDaugherChain => _cutDaugherChain;

            [SerializeField, Required, AssetsOnly, LabelText("アイテム鍵を開ける音")]
            private AudioClip _keyOpen;
            public AudioClip KeyOpen => _keyOpen;

            [SerializeField, Required, AssetsOnly, LabelText("鹿が落下する")]
            private AudioClip _deerFall;
            public AudioClip DeerFall => _deerFall;

            [SerializeField, Required, AssetsOnly, LabelText("アイテム鹿の血すくう")]
            private AudioClip _cupGetBlood;
            public AudioClip ScoopDeerBlood => _cupGetBlood;

            [SerializeField, Required, AssetsOnly, LabelText("アイテム鹿の血かける")]
            private AudioClip _cupPourBlood;
            public AudioClip PourDeerBlood => _cupPourBlood;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムコップ壊す")]
            private AudioClip _cupBreaking;
            public AudioClip CupBreaking => _cupBreaking;

            [SerializeField, Required, AssetsOnly, LabelText("アイテムガラス片まく")]
            private AudioClip _setBreakedCup;
            public AudioClip ScatterGlassPiece => _setBreakedCup;

            [SerializeField, Required, AssetsOnly, LabelText("アクション穴に入る")]
            private AudioClip _playerGoThroughHole;
            public AudioClip PlayerGoThroughHole => _playerGoThroughHole;

            [SerializeField, Required, AssetsOnly, LabelText("アクション穴から出る")]
            private AudioClip _playerGetOutHole;
            public AudioClip PlayerGetOutHole => _playerGetOutHole;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（木製、スライド）")]
            private AudioClip _openWoodSlideDoor;
            public AudioClip OpenWoodSlideDoor => _openWoodSlideDoor;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（木製、観音開き）")]
            private AudioClip _openWoodKannonDoor;
            public AudioClip OpenWoodKannonDoor => _openWoodKannonDoor;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（鉄製、スライド）")]
            private AudioClip _openIronSlideDoor;
            public AudioClip OpenIronSlideDoor => _openIronSlideDoor;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（鉄製、観音開き）")]
            private AudioClip _openIronKannonDoor;
            public AudioClip OpenIronKannonDoor => _openIronKannonDoor;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（木製、開かない）")]
            private AudioClip _openWoodUnopenableDoor;
            public AudioClip OpenWoodUnopenableDoor => _openWoodUnopenableDoor;

            [SerializeField, Required, AssetsOnly, LabelText("アクションドアを開ける（鉄製、開かない）")]
            private AudioClip _openIronUnopenableDoor;
            public AudioClip OpenIronUnopenableDoor => _openIronUnopenableDoor;
        }

        [Serializable]
        public sealed class AudioClipsSERough
        {

        }
    }
}