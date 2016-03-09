namespace ProcessingTools.Infrastructure.Attributes
{
    using System;

    public class FileNameAttribute : Attribute
    {
        public FileNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
        }

        public string Name { get; private set; }
    }
}