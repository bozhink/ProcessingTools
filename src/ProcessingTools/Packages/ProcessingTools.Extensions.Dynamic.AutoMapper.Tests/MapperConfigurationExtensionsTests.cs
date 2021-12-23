// <copyright file="MapperConfigurationExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.AutoMapper.Tests
{
    using global::AutoMapper;
    using NUnit.Framework;

    /// <summary>
    /// <see cref="MapperConfigurationExtensions"/> tests.
    /// </summary>
    [TestFixture]
    public class MapperConfigurationExtensionsTests
    {
        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestSource
        {
            /// <summary>
            /// Gets the ID of the test model.
            /// </summary>
            int Id { get; }

            /// <summary>
            /// Gets the name of the test model.
            /// </summary>
            string Name { get; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestDestination
        {
            /// <summary>
            /// Gets the ID of the test model.
            /// </summary>
            int Id { get; }

            /// <summary>
            /// Gets the name of the test model.
            /// </summary>
            string Name { get; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public class MyTestSource : IMyTestSource
        {
            /// <summary>
            /// Gets or sets the ID of the test model.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name of the test model.
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public class MyTestDestination : IMyTestDestination
        {
            /// <summary>
            /// Gets or sets the ID of the test model.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name of the test model.
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// CreateMapUsingProxy extension should work.
        /// </summary>
        [Test]
        public void AutoMapper_CreateMapUsingProxy_ShouldWork()
        {
            // Arrange
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMapUsingProxy<IMyTestSource, IMyTestDestination>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            MyTestSource testSource = new MyTestSource
            {
                Id = 1,
                Name = "123",
            };

            // Act
            IMyTestDestination testDestination = mapper.Map<IMyTestSource, IMyTestDestination>(testSource);

            // Assert
            Assert.AreEqual(testSource.Id, testDestination.Id);
            Assert.AreEqual(testSource.Name, testDestination.Name);
        }
    }
}
