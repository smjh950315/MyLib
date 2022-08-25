namespace MyLib.Mvc
{
    public static class ModelHelper
    {
        public static Dictionary<string, string>? ModelToDictionary(object? model)
        {
            if (model == null) return null;
            Dictionary<string, string> modelDict = new Dictionary<string, string>();
            string[] modelPropertiesName = model.GetType().GetProperties().Select(p => p.Name).ToArray();
            foreach (string propertyName in modelPropertiesName)
            {
                
                string propertyValue = "";
                bool Tried = false;
                if (!Tried)
                {
                    try
                    {
                        propertyValue = model.GetType().GetProperties().Where(p => p.Name == propertyName).First().GetValue(model)?.ToString() ?? "null";
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
    }
}
