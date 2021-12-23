// <copyright file="PocoTypeBuilderTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System;
    using System.Reflection.Emit;
    using NUnit.Framework;

    /// <summary>
    /// Tests for <see cref="PocoTypeBuilder"/>.
    /// </summary>
    [TestFixture]
    public class PocoTypeBuilderTests
    {
        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with valid module builder should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithValidModuleBuilder_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;

            // Act
            var instance = new PocoTypeBuilder(moduleBuilder);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with null module builder should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithNullModuleBuilder_ShouldThrowArgumentNullException()
        {
            // Arrange
            ModuleBuilder moduleBuilder = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoTypeBuilder(moduleBuilder);
            });

            Assert.AreEqual("moduleBuilder", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with valid module builder and valid type name should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithValidModuleBuilderAndValidTypeName_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";

            // Act
            var instance = new PocoTypeBuilder(moduleBuilder, typeName);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with null module builder and valid type name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithNullModuleBuilderAndValidTypeName_ShouldThrowArgumentNullException()
        {
            // Arrange
            ModuleBuilder moduleBuilder = null;
            string typeName = "MyType";

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoTypeBuilder(moduleBuilder, typeName);
            });

            Assert.AreEqual("moduleBuilder", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with null module builder and null type name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithNullModuleBuilderAndNullTypeName_ShouldThrowArgumentNullException()
        {
            // Arrange
            ModuleBuilder moduleBuilder = null;
            string typeName = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoTypeBuilder(moduleBuilder, typeName);
            });

            Assert.AreEqual("moduleBuilder", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with valid module builder and null type name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithValidModuleBuilderAndNullTypeName_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoTypeBuilder(moduleBuilder, typeName);
            });

            Assert.AreEqual("typeName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> constructor with valid module builder and empty type name should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_ConstructorWithValidModuleBuilderAndEmptyTypeName_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = string.Empty;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoTypeBuilder(moduleBuilder, typeName);
            });

            Assert.AreEqual("typeName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with valid property name and valid property type should work.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithValidPropertyNameAndValidPropertyType_ShouldWork()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = "ID";
            Type propertyType = typeof(int);

            // Act + Assert
            Assert.DoesNotThrow(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with null property name and valid property type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithNullPropertyNameAndValidPropertyType_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = null;
            Type propertyType = typeof(int);

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });

            Assert.AreEqual("propertyName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with empty property name and valid property type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithEmptyPropertyNameAndValidPropertyType_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = string.Empty;
            Type propertyType = typeof(int);

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });

            Assert.AreEqual("propertyName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with valid property name and null property type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithValidPropertyNameAndNullPropertyType_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = "ID";
            Type propertyType = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });

            Assert.AreEqual("propertyType", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with null property name and null property type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithNullPropertyNameAndNullPropertyType_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = null;
            Type propertyType = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });

            Assert.AreEqual("propertyName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> add property with empty property name and null property type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddProperty_WithEmptyPropertyNameAndNullPropertyType_ShouldThrowArgumentNullException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = string.Empty;
            Type propertyType = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                pocoTypeBuilder.AddProperty(propertyName, propertyType);
            });

            Assert.AreEqual("propertyName", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> create type with no properties should return valid type.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_CreateType_WithNoProperties_ShouldReturnValidType()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);

            // Act
            var type = pocoTypeBuilder.CreateType();

            // Assert
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsClass);
            Assert.IsFalse(type.IsAbstract);
            Assert.IsFalse(type.IsSealed);
            Assert.IsFalse(type.IsGenericType);
            Assert.AreEqual(typeName, type.Name);

            TestContext.WriteLine(type.Name);
            TestContext.WriteLine(type.FullName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> create type with one property should return valid type.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_CreateType_WithOneProperty_ShouldReturnValidType()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string propertyName = "ID";
            Type propertyType = typeof(int);

            // Act
            pocoTypeBuilder.AddProperty(propertyName, propertyType);
            var type = pocoTypeBuilder.CreateType();

            // Assert
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsClass);
            Assert.IsFalse(type.IsAbstract);
            Assert.IsFalse(type.IsSealed);
            Assert.IsFalse(type.IsGenericType);
            Assert.AreEqual(typeName, type.Name);
            Assert.AreEqual(1, type.GetProperties().Length);
            Assert.AreEqual(propertyName, type.GetProperties()[0].Name);
            Assert.AreEqual(propertyType, type.GetProperties()[0].PropertyType);

            TestContext.WriteLine(type.Name);
            TestContext.WriteLine(type.FullName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> create type with two properties should return valid type.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_CreateType_WithTwoProperties_ShouldReturnValidType()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            string property1Name = "ID";
            Type property1Type = typeof(int);
            string property2Name = "Name";
            Type property2Type = typeof(string);

            // Act
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);
            var type = pocoTypeBuilder.CreateType();

            // Assert
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsClass);
            Assert.IsFalse(type.IsAbstract);
            Assert.IsFalse(type.IsSealed);
            Assert.IsFalse(type.IsGenericType);
            Assert.AreEqual(typeName, type.Name);
            Assert.AreEqual(2, type.GetProperties().Length);
            Assert.AreEqual(property1Name, type.GetProperties()[0].Name);
            Assert.AreEqual(property1Type, type.GetProperties()[0].PropertyType);
            Assert.AreEqual(property2Name, type.GetProperties()[1].Name);
            Assert.AreEqual(property2Type, type.GetProperties()[1].PropertyType);

            TestContext.WriteLine(type.Name);
            TestContext.WriteLine(type.FullName);
        }

        /// <summary>
        /// <see cref="PocoTypeBuilder"/> Add Property After Create Type Should Throw <see cref="InvalidOperationException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoTypeBuilder))]
        public void PocoTypeBuilder_AddPropertyAfterCreateType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);

            string property1Name = "ID";
            Type property1Type = typeof(int);

            string property2Name = "Name";
            Type property2Type = typeof(string);

            // Act + Assert
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            var type = pocoTypeBuilder.CreateType();

            Assert.Throws<InvalidOperationException>(() =>
            {
                pocoTypeBuilder.AddProperty(property2Name, property2Type);
            });

            // Assert
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsClass);
            Assert.IsFalse(type.IsAbstract);
            Assert.IsFalse(type.IsSealed);
            Assert.IsFalse(type.IsGenericType);
            Assert.AreEqual(typeName, type.Name);

            Assert.AreEqual(1, type.GetProperties().Length);
            Assert.AreEqual(property1Name, type.GetProperties()[0].Name);
            Assert.AreEqual(property1Type, type.GetProperties()[0].PropertyType);
        }
    }
}
