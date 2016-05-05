namespace ProcessingTools.Tagger.Models
{
    using System;
    using Contracts;

    public class ControllerInfo : IControllerInfo
    {
        public Type ControllerType { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
