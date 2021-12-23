// <copyright file="PocoModuleBuilderTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    /// <summary>
    /// Tests for <see cref="PocoModuleBuilder"/>.
    /// </summary>
    [TestFixture]
    public class PocoModuleBuilderTests
    {
        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with default constructor should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithDefaultConstructor_ShouldReturnValidObject()
        {
            // Arrange + Act
            var instance = new PocoModuleBuilder();

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with default constructor should return object with valid assembly name.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithDefaultConstructor_ShouldReturnObjectWithValidAssemblyName()
        {
            // Arrange + Act
            var instance = new PocoModuleBuilder();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(instance.AssemblyName.Name));
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with default constructor should return object with valid properties.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithDefaultConstructor_ShouldReturnObjectWithValidProperties()
        {
            // Arrange + Act
            var instance = new PocoModuleBuilder();

            // Assert
            Assert.IsNotNull(instance.AssemblyName);
            Assert.IsNotNull(instance.AssemblyBuilder);
            Assert.IsNotNull(instance.ModuleBuilder);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid name should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidName_ShouldReturnValidObject()
        {
            // Arrange
            string name = "MyAssembly";

            // Act
            var instance = new PocoModuleBuilder(name);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid name should return object with valid assembly name.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidName_ShouldReturnObjectWithValidAssemblyName()
        {
            // Arrange
            string name = "MyAssembly";

            // Act
            var instance = new PocoModuleBuilder(name);

            // Assert
            Assert.AreEqual(name, instance.AssemblyName.Name);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid name should return object with valid properties.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidName_ShouldReturnObjectWithValidProperties()
        {
            // Arrange
            string name = "MyAssembly";

            // Act
            var instance = new PocoModuleBuilder(name);

            // Assert
            Assert.IsNotNull(instance.AssemblyName);
            Assert.IsNotNull(instance.AssemblyBuilder);
            Assert.IsNotNull(instance.ModuleBuilder);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with null name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithNullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            string name = null;

            // Act + Assert
            var ex = Assert.Catch<ArgumentNullException>(() =>
            {
                _ = new PocoModuleBuilder(name);
            });

            Assert.AreEqual("assemblyName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid assembly name should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidAssemblyName_ShouldReturnValidObject()
        {
            // Arrange
            AssemblyName assemblyName = new AssemblyName("MyAssembly");

            // Act
            var instance = new PocoModuleBuilder(assemblyName);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid assembly name should return object with valid assembly name.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidAssemblyName_ShouldReturnObjectWithValidAssemblyName()
        {
            // Arrange
            AssemblyName assemblyName = new AssemblyName("MyAssembly");

            // Act
            var instance = new PocoModuleBuilder(assemblyName);

            // Assert
            Assert.AreEqual(assemblyName.FullName, instance.AssemblyName.Name);
            Assert.AreSame(assemblyName, instance.AssemblyName);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with valid assembly name should return object with valid properties.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithValidAssemblyName_ShouldReturnObjectWithValidProperties()
        {
            // Arrange
            AssemblyName assemblyName = new AssemblyName("MyAssembly");

            // Act
            var instance = new PocoModuleBuilder(assemblyName);

            // Assert
            Assert.IsNotNull(instance.AssemblyName);
            Assert.IsNotNull(instance.AssemblyBuilder);
            Assert.IsNotNull(instance.ModuleBuilder);
        }

        /// <summary>
        /// <see cref="PocoModuleBuilder"/> with constructor with null assembly name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoModuleBuilder))]
        public void PocoModuleBuilder_WithConstructorWithNullAssemblyName_ShouldThrowArgumentNullException()
        {
            // Arrange
            AssemblyName name = null;

            // Act + Assert
            var ex = Assert.Catch<ArgumentNullException>(() =>
            {
                _ = new PocoModuleBuilder(name);
            });

            Assert.AreEqual("assemblyName", ex.ParamName);
        }
    }
}
