namespace ProcessingTools.Tagger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;
    using Models;

    public class ControllerInfoProvider
    {
        public ControllerInfoProvider()
        {
            this.ControllersInformation = new Dictionary<Type, IControllerInfo>();
        }

        public IDictionary<Type, IControllerInfo> ControllersInformation { get; private set; }

        public void ProcessInformation()
        {
            // Print controllers’ information
            string defaultControllerInterfaceName = typeof(ITaggerController).FullName;
            var controllerTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.FullName == defaultControllerInterfaceName));

            controllerTypes.Select(t =>
            {
                string controllerName = Regex.Match(t.FullName, @"(?<=\A.*?)([^\.]+)(?=Controller\Z)").Value;
                return new ControllerInfo
                {
                    ControllerType = t,
                    Name = controllerName,
                    Description = t.GetDescriptionMessageForController()
                };
            })
            .ToList()
            .ForEach(o =>
            {
                this.ControllersInformation.Add(o.ControllerType, o);
            });
        }
    }
}
