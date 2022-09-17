namespace MyLib
{
    public static class ObjectHelper
    {
        public static Dictionary<string, string>? ClassToDictionary(object? obj)
        {
            if (obj == null) return null;
            Dictionary<string, string> modelDict = new Dictionary<string, string>();
            string[] modelPropertiesName = obj.GetType().GetProperties().Select(p => p.Name).ToArray();
            foreach (string propertyName in modelPropertiesName)
            {
                string propertyValue = "";
                bool Tried = false;
                if (!Tried)
                {
                    try
                    {
                        propertyValue = obj.GetType().GetProperties().Where(p => p.Name == propertyName).First().GetValue(obj)?.ToString() ?? "null";
                    }
                    catch (Exception? ex)
                    {
                        Tried = true;
                        propertyValue = string.Empty;
                    }
                }
                modelDict.Add(propertyName, propertyValue);
            }
            return modelDict;
        }
        public static dynamic? TryGetMemberValue(object? obj, string memberName)
        {
            if (obj == null) return null;
            try
            {
                dynamic? m = obj.GetType().GetProperties().Where(m => m.Name == memberName).FirstOrDefault()?.GetValue(obj);
                return m;
            }
            catch
            {
                return null;
            }
        }
        public static dynamic? TryGetObjectInList(dynamic? obj, int index)
        {
            if (obj == null) return null;
            try
            {
                return obj[index];
            }
            catch
            {
                return null;
            }
        }
    }
}
