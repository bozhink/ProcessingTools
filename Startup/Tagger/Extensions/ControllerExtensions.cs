namespace ProcessingTools.MainProgram.Extensions
{
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Contracts;

    using ProcessingTools.Infrastructure.Attributes;

    public static class ControllerExtensions
    {
        public static string GetDescriptionMessageForController(this ITaggerController controller)
        {
            var type = controller.GetType();

            return $"\n\t{type.GetDescriptionMessageForController()}\n";
        }

        public static string GetDescriptionMessageForController(this Type type)
        {
            string message = type.GetCustomAttribute<DescriptionAttribute>(false)?.Description;

            if (string.IsNullOrWhiteSpace(message))
            {
                var name = Regex.Replace((string)type.FullName, @".*?([^\.]+)\Z", "$1");
                name = Regex.Replace(name, @"Controller\Z", string.Empty);

                message = Regex.Replace(name, "(?=[A-Z])", " ").Trim();
            }

            return message;
        }
    }
}
