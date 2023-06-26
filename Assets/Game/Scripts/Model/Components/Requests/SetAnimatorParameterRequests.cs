using System.Collections.Generic;
using Assets.Plugins.IvaLib;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components.Requests
{
    public enum AnimatorParameterType
    {
        Int,
        Float,
        Bool,
        Trigger
    }

    public struct AnimatorParameterRequest
    {
        public AnimatorParameterType Type;
        public int Hash;
        public int IntValue;
        public float FloatValue;
        public bool BoolValue;
    }

    public struct SetAnimatorParameterRequests
    {
        public void Initialize(int requestsCount = 4)
        {
            _requests = new FastList<AnimatorParameterRequest>(requestsCount);
        }

        public void AddFloat(string parameterName, float value)
        {
            _requests.Add(new AnimatorParameterRequest
            {
                Type = AnimatorParameterType.Float,
                Hash = Animator.StringToHash(parameterName),
                FloatValue = value
            });
        }

        public void AddBool(string parameterName, bool value)
        {
            _requests.Add(new AnimatorParameterRequest
            {
                Type = AnimatorParameterType.Bool,
                Hash = Animator.StringToHash(parameterName),
                BoolValue = value
            });
        }

        public void AddInt(string parameterName, int value)
        {
            _requests.Add(new AnimatorParameterRequest
            {
                Type = AnimatorParameterType.Int,
                Hash = Animator.StringToHash(parameterName),
                IntValue = value
            });
        }

        public void AddTrigger(string parameterName)
        {
            _requests.Add(new AnimatorParameterRequest
            {
                Type = AnimatorParameterType.Trigger,
                Hash = Animator.StringToHash(parameterName)
            });
        }

        public IEnumerable<AnimatorParameterRequest> Requests => _requests;

        private FastList<AnimatorParameterRequest> _requests;
    }
}