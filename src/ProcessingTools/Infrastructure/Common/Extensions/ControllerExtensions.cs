namespace ProcessingTools.Common.Extensions
{
    public static class ControllerExtensions
    {
        private const string ControllerNameSuffix = "Controller";

        public static string GetControllerName<T>()
        {
            var type = typeof(T);
            string name = type.Name;
            int suffixIndex = name.IndexOf(ControllerNameSuffix, 0);
            if (suffixIndex > 0)
            {
                name = name.Substring(0, suffixIndex);
            }

            return name;
        }
    }
}
