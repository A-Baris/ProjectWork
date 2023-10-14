using Newtonsoft.Json;

namespace Restaurant.MVC.Utility
{
    public class SessionHelper
    {
        public static void SetJsonProduct(ISession session,string key,object value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }


        public static T GetProductFromJson<T>(ISession session,string key)
        {
            var result = session.GetString(key);
            if (result == null)
            {
                return default(T);
            }
            else
            {
                var deserialize=JsonConvert.DeserializeObject<T>(result);
                return deserialize;
            }

        }

    }
}
