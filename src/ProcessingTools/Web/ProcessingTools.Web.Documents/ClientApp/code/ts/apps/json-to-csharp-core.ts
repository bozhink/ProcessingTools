export interface IProperty {
    name: string;
    type: string;
}

export interface IObject {
    name: string;
    properties: Array<IProperty>;
}

export class JsonToCSharpConverter {
    private objects: Array<IObject> = [];

    public generateCSharpClasses(obj: any): string {
        if (obj == null) {
            return null;
        }

        this.generateClass("result", obj);

        let resultContent: string = "using Newtonsoft.Json;\n\n";

        let objectIndex: number, numberOfObjects: number = this.objects.length;
        for (objectIndex = 0; objectIndex < numberOfObjects; objectIndex += 1) {
            let csharpClass: IObject = this.objects[objectIndex];

            resultContent += `public class ${this.getCSharpStyleName(csharpClass.name)}\n{\n\n`;

            let propertyIndex: number, numberOfProperties: number = csharpClass.properties.length;
            for (propertyIndex = 0; propertyIndex < numberOfProperties; propertyIndex += 1) {
                let property: IProperty = csharpClass.properties[propertyIndex];
                let type: string = property.type.indexOf("Child") >= 0 ? this.getCSharpStyleName(property.type) : property.type;
                let name: string = this.getCSharpStyleName(property.name);

                resultContent += `    /// <summary>\n`;
                resultContent += `    /// Gets or sets the ${name.replace(/([A-Z])/g, " $1").toLowerCase().trim()}.\n`;
                resultContent += `    /// </summary>\n`;
                resultContent += `    [JsonProperty("${property.name}")]\n`;
                resultContent += `    public ${type} ${name} { get; set; }\n\n`;
            }

            resultContent += `}\n\n`;
        }

        return resultContent;
    }

    private generateClass(name: string, obj: any): void {
        if (name == null || obj == null) {
            return;
        }

        // find or create class object
        let object: IObject, objectIndex: number, numberOfObjects: number;

        numberOfObjects = this.objects.length;
        for (objectIndex = 0; objectIndex < numberOfObjects; objectIndex += 1) {
            if (this.objects[objectIndex].name === name) {
                object = this.objects[objectIndex];
                break;
            }
        }

        if (object == null) {
            object = {
                name: name,
                properties: []
            };

            // push resultant object to the object store
            this.objects.push(object);
        }

        // create object properties if they are not present
        object.properties = object.properties || [];

        // merge known properties with new ones
        let properties: Array<IProperty> = object.properties;

        if (Array.isArray(obj)) {

            let property: IProperty = this.pushProperty(properties, {
                type: `${name}Child[]`,
                name: `${name}Items`
            });

            let itemIndex: number, numberOfItems: number = obj.length;

            for (itemIndex = 0; itemIndex < numberOfItems; itemIndex += 1) {
                this.generateClass(`${name}Child`, obj[itemIndex]);
            }
        } else {
            let propertyName: string;
            for (propertyName in obj) {
                if (obj[propertyName] != null) {
                    this.generateProperties(name, propertyName, obj[propertyName], properties);
                }
            }
        }
    }

    private generateProperties(className: string, propertyName: string, value: any, properties: Array<IProperty>): void {
        if (propertyName == null || value == null || properties == null || !Array.isArray(properties)) {
            return;
        }

        let property: IProperty = {
            name: propertyName,
            type: this.getCSharpPropertyType(className, propertyName, value)
        };

        if (property.type != null) {
            property = this.pushProperty(properties, property);

            if (typeof (value) === "object") {
                if (property.type.indexOf("[]") < 0) {
                    this.generateClass(property.type, value);
                } else {
                    // arrays are of type object
                    if (property.type.indexOf("Child") >= 0) {
                        // generate only classes for complex types.
                        let typeName: string, typeObject: any, i: number, len: number, prop: string;
                        if (Array.isArray(value)) {
                            len = value.length;

                            typeObject = {};
                            for (i = 0; i < len; i += 1) {
                                for (prop in value[i]) {
                                    if (value[i][prop] != null) {
                                        typeObject[prop] = value[i][prop];
                                    }
                                }
                            }

                            typeName = property.type.substring(0, property.type.length - 2);
                            this.generateClass(typeName, typeObject);
                        }

                    }
                }
            }
        }

    }

    private pushProperty(properties: Array<IProperty>, property: IProperty): IProperty {
        let p: IProperty, i: number, len: number;
        if (property == null || property.name == null || property.type == null || properties == null || !Array.isArray(properties)) {
            return null;
        }

        len = properties.length;
        for (i = 0; i < len; i += 1) {
            p = properties[i];
            if (p.name === property.name && p.type === property.type) {
                return p;
            }
        }

        properties.push(property);
        return property;
    }

    private getArrayItemType(className: string, name: string, array: Array<any>): string {
        let typeOrder: any = { "bool": 0, "int": 1, "decimal": 2, "char": 3, "string": 4 };
        let type: string, subtype: string, i: number, len: number;

        if (Array.isArray(array)) {
            len = array.length;
            for (i = 0; i < len; i += 1) {
                subtype = this.getCSharpPropertyType(className, name, array[i]);
                if (subtype.indexOf("Child") >= 0) {
                    return subtype;
                }

                if (type == null) {
                    type = subtype;
                } else {
                    type = typeOrder[type] > typeOrder[subtype] ? type : subtype;
                }
            }

            return type;
        }

        return null;
    }

    private getCSharpPropertyType(className: string, name: string, value: any): string {
        var type: string, itemType: string;
        if (Array.isArray(value)) {
            itemType = this.getArrayItemType(className, name, value);
            return `${itemType}[]`;
        }

        type = typeof (value);
        switch (type) {
            case "string":
                return `string`;

            case "number":
                // tslint:disable-next-line:no-bitwise
                return (value | 0) === value ? `int` : `decimal`;

            case "boolean":
                return `bool`;

            case "symbol":
                return `char`;

            case "object":
                return `${className}_${name}Child`;

            default:
                return null;
        }
    }

    private toFirstLetterUpperCase(value: string): string {
        let stringValue: string = (value || "").toString();

        switch (stringValue.length) {
            case 0:
                return stringValue;

            case 1:
                return stringValue.toUpperCase();

            default:
                return stringValue.substring(0, 1).toUpperCase() + stringValue.substring(1);
        }
    }

    private getCSharpStyleName(value: string): string {
        let stringValue: string = (value || "").toString();
        let nameParts: Array<string> = stringValue.split("_");
        let i: number, len: number = nameParts.length;
        let result: string = "";
        for (i = 0; i < len; i += 1) {
            if (nameParts[i] != null && nameParts[i].length > 0) {
                result += this.toFirstLetterUpperCase(nameParts[i]);
            }
        }

        return result;
    }
}
