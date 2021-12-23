// <copyright file="PocoInstanceBuilderTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using NUnit.Framework;

    /// <summary>
    /// Tests for <see cref="PocoInstanceBuilder"/>.
    /// </summary>
    [TestFixture]
    public class PocoInstanceBuilderTests
    {
        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> constructor with valid type should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_ConstructorWithValidType_ShouldReturnValidObject()
        {
            // Arrange
            Type type = typeof(Type);

            // Act
            var instance = new PocoInstanceBuilder(type);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> constructor with null type should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_ConstructorWithNullType_ShouldThrowArgumentNullException()
        {
            // Arrange
            Type type = null;

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PocoInstanceBuilder(type);
            });

            Assert.AreEqual("type", ex.ParamName);
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> create instance with no parameters should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_CreateInstance_WithNoParameters_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";
            string property1Name = "ID";
            Type property1Type = typeof(int);
            string property2Name = "Name";
            Type property2Type = typeof(string);

            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);

            Type type = pocoTypeBuilder.CreateType();
            PocoInstanceBuilder pocoInstanceBuilder = new PocoInstanceBuilder(type);

            // Act
            var instance = pocoInstanceBuilder.CreateInstance();

            // Assert
            Assert.IsNotNull(instance);

            TestContext.WriteLine($"{instance}");
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> create instance with valid dictionary should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_CreateInstance_WithValidDictionary_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";

            string property1Name = "ID";
            Type property1Type = typeof(int);
            object property1Value = 1;

            string property2Name = "Name";
            Type property2Type = typeof(string);
            object property2Value = "My Name";

            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);

            Type type = pocoTypeBuilder.CreateType();
            PocoInstanceBuilder pocoInstanceBuilder = new PocoInstanceBuilder(type);

            // Act
            var instance = pocoInstanceBuilder.CreateInstance(new Dictionary<string, object>
            {
                { property2Name, property2Value },
                { property1Name, property1Value },
            });

            // Assert
            Assert.IsNotNull(instance);
            Assert.AreEqual(property1Value, pocoInstanceBuilder.GetValue(instance, property1Name));
            Assert.AreEqual(property2Value, pocoInstanceBuilder.GetValue(instance, property2Name));

            TestContext.WriteLine($"{instance}");
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> create instance with valid dictionary with referent types should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_CreateInstance_WithValidDictionaryWithReferentTypes_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";

            string property1Name = "ID";
            object property1Value = new { id = 1 };
            Type property1Type = property1Value.GetType();

            string property2Name = "Name";
            object property2Value = new { name = "My Name" };
            Type property2Type = property2Value.GetType();

            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);

            Type type = pocoTypeBuilder.CreateType();
            PocoInstanceBuilder pocoInstanceBuilder = new PocoInstanceBuilder(type);

            // Act
            var instance = pocoInstanceBuilder.CreateInstance(new Dictionary<string, object>
            {
                { property2Name, property2Value },
                { property1Name, property1Value },
            });

            // Assert
            Assert.IsNotNull(instance);
            Assert.AreSame(property1Value, pocoInstanceBuilder.GetValue(instance, property1Name));
            Assert.AreSame(property2Value, pocoInstanceBuilder.GetValue(instance, property2Name));

            TestContext.WriteLine($"{instance}");
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> create instance with null dictionary should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_CreateInstance_WithNullDictionary_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";

            string property1Name = "ID";
            object property1Value = new { id = 1 };
            Type property1Type = property1Value.GetType();

            string property2Name = "Name";
            object property2Value = new { name = "My Name" };
            Type property2Type = property2Value.GetType();

            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);

            Type type = pocoTypeBuilder.CreateType();
            PocoInstanceBuilder pocoInstanceBuilder = new PocoInstanceBuilder(type);

            // Act
            var instance = pocoInstanceBuilder.CreateInstance(null);

            // Assert
            Assert.IsNotNull(instance);
            Assert.IsNull(pocoInstanceBuilder.GetValue(instance, property1Name));
            Assert.IsNull(pocoInstanceBuilder.GetValue(instance, property2Name));

            TestContext.WriteLine($"{instance}");
        }

        /// <summary>
        /// <see cref="PocoInstanceBuilder"/> create instance with empty dictionary with referent types should return valid object.
        /// </summary>
        [Test(TestOf = typeof(PocoInstanceBuilder))]
        public void PocoInstanceBuilder_CreateInstance_WithEmptyDictionaryWithReferentTypes_ShouldReturnValidObject()
        {
            // Arrange
            PocoModuleBuilder pocoModuleBuilder = new PocoModuleBuilder();
            ModuleBuilder moduleBuilder = pocoModuleBuilder.ModuleBuilder;
            string typeName = "MyType";

            string property1Name = "ID";
            object property1Value = new { id = 1 };
            Type property1Type = property1Value.GetType();

            string property2Name = "Name";
            object property2Value = new { name = "My Name" };
            Type property2Type = property2Value.GetType();

            PocoTypeBuilder pocoTypeBuilder = new PocoTypeBuilder(moduleBuilder, typeName);
            pocoTypeBuilder.AddProperty(property1Name, property1Type);
            pocoTypeBuilder.AddProperty(property2Name, property2Type);

            Type type = pocoTypeBuilder.CreateType();
            PocoInstanceBuilder pocoInstanceBuilder = new PocoInstanceBuilder(type);

            // Act
            var instance = pocoInstanceBuilder.CreateInstance(new Dictionary<string, object>());

            // Assert
            Assert.IsNotNull(instance);
            Assert.IsNull(pocoInstanceBuilder.GetValue(instance, property1Name));
            Assert.IsNull(pocoInstanceBuilder.GetValue(instance, property2Name));

            TestContext.WriteLine($"{instance}");
        }
    }
}
