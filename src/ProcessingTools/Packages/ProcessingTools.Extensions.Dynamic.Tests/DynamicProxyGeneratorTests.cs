// <copyright file="DynamicProxyGeneratorTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Tests for <see cref="DynamicProxyGenerator"/>.
    /// </summary>
    [TestFixture]
    public class DynamicProxyGeneratorTests
    {
        /// <summary>
        /// Test category type.
        /// </summary>
        public enum TestCategoryType
        {
            /// <summary>
            /// Category 1.
            /// </summary>
            Category1 = 0,

            /// <summary>
            /// Category 2.
            /// </summary>
            Category2 = 1,

            /// <summary>
            /// Category 3.
            /// </summary>
            Category3 = 2,
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyNestedTestModel
        {
            /// <summary>
            /// Gets or sets value for the string property.
            /// </summary>
            string Prop { get; set; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModel
        {
            /// <summary>
            /// Gets or sets the ID of the test model.
            /// </summary>
            int Id { get; set; }

            /// <summary>
            /// Gets or sets the name of the test model.
            /// </summary>
            string Name { get; set; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModelWithInheritance1 : IMyTestModel
        {
            /// <summary>
            /// Gets or sets the price of the model.
            /// </summary>
            decimal Price { get; set; }

            /// <summary>
            /// Gets or sets nester test model.
            /// </summary>
            IMyNestedTestModel NestedModel { get; set; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModelWithInheritance : IMyTestModelWithInheritance1
        {
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModelWithGetters
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
        public interface IMyTestModelWithGettersWithInheritance2 : IMyTestModelWithGetters
        {
            /// <summary>
            /// Gets the category of the model.
            /// </summary>
            TestCategoryType Category { get; }

            /// <summary>
            /// Gets nester test model.
            /// </summary>
            IMyNestedTestModel NestedModel { get; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModelWithGettersWithInheritance1 : IMyTestModelWithGettersWithInheritance2
        {
            /// <summary>
            /// Gets the price of the model.
            /// </summary>
            decimal Price { get; }
        }

        /// <summary>
        /// Model interface for tests.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Test type")]
        public interface IMyTestModelWithGettersWithInheritance : IMyTestModelWithGettersWithInheritance1
        {
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor should work.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_ShouldWork()
        {
            // Arrange + Act
            IMyTestModel instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModel>();

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor with inheritance should work.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_WithInheritance_ShouldWork()
        {
            // Arrange + Act
            IMyTestModelWithInheritance instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithInheritance>();

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor for model only with getters should work.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_OnlyGetters_ShouldWork()
        {
            // Arrange + Act
            IMyTestModelWithGetters instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithGetters>();

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor for model only with getters with inheritance should work.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_OnlyGetters_WithInheritance_ShouldWork()
        {
            // Arrange + Act
            IMyTestModelWithGettersWithInheritance instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithGettersWithInheritance>();

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor should return valid instance.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_ShouldReturnValidInstance()
        {
            // Arrange
            int id = 1;
            string name = "123123";
            IMyTestModel instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModel>();

            // Act
            instance.Id = id;
            instance.Name = name;

            // Assert
            Assert.AreEqual(id, instance.Id);
            Assert.AreEqual(name, instance.Name);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor with inheritance should return valid instance.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_WithInheritance_ShouldReturnValidInstance()
        {
            // Arrange
            int id = 1;
            string name = "123123";
            IMyTestModelWithInheritance instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithInheritance>();

            // Act
            instance.Id = id;
            instance.Name = name;

            // Assert
            Assert.AreEqual(id, instance.Id);
            Assert.AreEqual(name, instance.Name);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor for model only with getters should return valid instance.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_OnlyGetters_ShouldReturnValidInstance()
        {
            // Arrange
            int id = 1;
            string name = "123123";
            IMyTestModelWithGetters instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithGetters>();

            // Act
            instance.GetType().GetProperty(nameof(IMyTestModelWithGetters.Id)).SetMethod.Invoke(instance, new object[] { id });
            instance.GetType().GetProperty(nameof(IMyTestModelWithGetters.Name)).SetMethod.Invoke(instance, new object[] { name });

            // Assert
            Assert.AreEqual(id, instance.Id);
            Assert.AreEqual(name, instance.Name);
        }

        /// <summary>
        /// <see cref="DynamicProxyGenerator"/>.GetFakeInstanceFor for model only with getters with inheritance should return valid instance.
        /// </summary>
        [Test(TestOf = typeof(DynamicProxyGenerator))]
        public void DynamicProxyGenerator_GetFakeInstanceFor_OnlyGetters_WithInheritance_ShouldReturnValidInstance()
        {
            // Arrange
            int id = 1;
            string name = "123123";
            IMyTestModelWithGettersWithInheritance instance = DynamicProxyGenerator.GetFakeInstanceFor<IMyTestModelWithGettersWithInheritance>();

            // Act
            instance.GetType().GetProperty(nameof(IMyTestModelWithGetters.Id)).SetMethod.Invoke(instance, new object[] { id });
            instance.GetType().GetProperty(nameof(IMyTestModelWithGetters.Name)).SetMethod.Invoke(instance, new object[] { name });

            // Assert
            Assert.AreEqual(id, instance.Id);
            Assert.AreEqual(name, instance.Name);
        }
    }
}
