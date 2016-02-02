namespace ProcessingTools.MainProgram.Contracts
{
    using System;

    public interface IControllerInfo
    {
        Type ControllerType { get; set; }

        string Name { get; set; }

        string Description { get; set; }
    }
}
