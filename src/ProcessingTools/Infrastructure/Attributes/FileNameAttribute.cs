namespace ProcessingTools.Attributes
{
    using System;

    public class FileNameAttribute : Attribute
    {
        public FileNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        public string Name { get; private set; }
    }
}