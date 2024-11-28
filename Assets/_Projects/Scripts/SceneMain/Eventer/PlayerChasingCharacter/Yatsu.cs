
using System;
using UnityEngine;

namespace Main.Eventer.PlayerChasingCharacter
{
    [Serializable]
    public sealed class Yatsu : APlayerChasingCharacter
    {
        protected override float InitSpeed => 0.5f;

        public bool IsSteppingOnGlassShard { get; set; } = false;

        protected override void RetargetPlayerWithoutNullCheck(Transform playerTransform)
        {
            if (IsSteppingOnGlassShard is true) return;

            _navMeshAgent.SetDestination(playerTransform.position);
            _navMeshAgent.transform.LookAt(playerTransform);
        }
    }
}
