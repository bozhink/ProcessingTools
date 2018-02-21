namespace ProcessingTools.Tagger.Interceptors
{
    using System;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Geo.Coordinates;
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    internal class LogParsedCoordinatesInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public LogParsedCoordinatesInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Target.GetType() == typeof(Coordinate2DParser))
            {
                if (invocation.Request.Arguments.Length != 4)
                {
                    throw new InvalidOperationException($"{nameof(LogParsedCoordinatesInterceptor)} requires invocation with 4 arguments");
                }

                string coordinateString = (string)invocation.Request.Arguments[0];
                string coordinateType = (string)invocation.Request.Arguments[1];
                ICoordinatePart latitude = (ICoordinatePart)invocation.Request.Arguments[2];
                ICoordinatePart longitude = (ICoordinatePart)invocation.Request.Arguments[3];

                invocation.Proceed();

                this.logger?.Log("{2} =\t{0};\t{3} =\t{1}\n", latitude.Value, longitude.Value, latitude.Type, longitude.Type, coordinateString, coordinateType);
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
