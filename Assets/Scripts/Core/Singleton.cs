using Core.Errors;

namespace Core.Patterns
{
    public class Singleton<T>
        where T : class, new()
    {
        private static T _instance;

        private static bool singletonInstantiation = false;
        protected static bool SingletonInstantiation => singletonInstantiation;
        protected void ThrowInstantiationError()
        {
            ErrorManager.ThrowError(ErrorEnum.CALLING_SINGLETON_CONSTRUCTOR, "Singleton type : " + typeof(T));
        }


        public static T GetInstance()
        {
            if (_instance == null)
            {
                singletonInstantiation = true;
                _instance = new T();
                singletonInstantiation = false;
            }

            return _instance;
        }
    }
}
