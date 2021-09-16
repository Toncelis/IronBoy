using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Errors
{
    public enum ErrorEnum
    {
        CALLING_SINGLETON_CONSTRUCTOR = -1,

        NONPOSITIVE_MAX_HP = -2
    }

    public class ErrorManager
    {
        private static Dictionary<ErrorEnum, string> ErrorDescription = new Dictionary<ErrorEnum, string>
        {
            {ErrorEnum.CALLING_SINGLETON_CONSTRUCTOR, "Trying to instantiate singleton with public constructor"},
            {ErrorEnum.NONPOSITIVE_MAX_HP, "Setting character with nonpositive max hp" }
        };

        public static void ThrowError(ErrorEnum error, string extraInfo)
        {

            Debug.LogError($"Error : {(int)error}\n{ErrorDescription[error]}\n{extraInfo}\n");

#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                return;
            }
#endif

            Application.Quit((int)error);
        }
    }
}
