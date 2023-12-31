﻿using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;

namespace Assets.Game.Scripts.Levels.Model.Practices.Extensions
{
    public static class SetAnimatorParameterRequestsExtensions
    {
        public static SetAnimatorParameterRequests Initialize(this SetAnimatorParameterRequests requests, int count = 4)
        {
            requests.Initialize(count);
            return requests;
        }

        public static SetAnimatorParameterRequests Add(this SetAnimatorParameterRequests requests, string parameterName, float value)
        {
            requests.AddFloat(parameterName, value);
            return requests;
        }

        public static SetAnimatorParameterRequests Add(this SetAnimatorParameterRequests requests, string parameterName, bool value)
        {
            requests.AddBool(parameterName, value);
            return requests;
        }

        public static SetAnimatorParameterRequests Add(this SetAnimatorParameterRequests requests, string parameterName, int value)
        {
            requests.AddInt(parameterName, value);
            return requests;
        }

        public static SetAnimatorParameterRequests Add(this SetAnimatorParameterRequests requests, string parameterName)
        {
            requests.AddTrigger(parameterName);
            return requests;
        }
    }
}