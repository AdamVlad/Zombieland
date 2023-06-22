﻿using System;
using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Requests;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            PlayerTagComponent,
            MonoLink<Animator>,
            SetAnimatorParameterRequests>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var animator = ref pools.Inc2.Get(entity).Value;
                ref var animatorParameterRequests = ref pools.Inc3.Get(entity);

                foreach (var request in animatorParameterRequests.Requests)
                {
                    SetParameter(animator, request);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetParameter(Animator animator, in AnimatorParameterRequest request)
        {
            switch (request.Type)
            {
                case AnimatorParameterType.Int:
                    animator.SetInteger(request.Hash, request.IntValue);
                    break;
                case AnimatorParameterType.Float:
                    animator.SetFloat(request.Hash, request.FloatValue);
                    break;
                case AnimatorParameterType.Bool:
                    animator.SetBool(request.Hash, request.BoolValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}