var path = require("path");

module.exports = {
    target: "web",
    entry: {
        "bio-data-app": path.resolve(__dirname, "wwwroot/build/out/js/apps/bio-data-app.js"),
        "document-edit": path.resolve(__dirname, "wwwroot/build/out/js/apps/document-edit.js"),
        "document-preview": path.resolve(__dirname, "wwwroot/build/out/js/apps/document-preview.js"),
        "files-index": path.resolve(__dirname, "wwwroot/build/out/js/apps/files-index.js"),
        "json-to-csharp": path.resolve(__dirname, "wwwroot/build/out/js/apps/json-to-csharp.js"),
        "json-to-csharp-core": path.resolve(__dirname, "wwwroot/build/out/js/apps/json-to-csharp-core.js"),
        "qr-code-generator": path.resolve(__dirname, "wwwroot/build/out/js/apps/qr-code-generator.js"),
        "text-editor-monaco": path.resolve(__dirname, "wwwroot/build/out/js/apps/text-editor-monaco.js"),
    },
    output: {
        path: path.resolve(__dirname, "wwwroot/build/dist/js/apps"),
        filename: "[name].bundle.js"
    },
    mode: "production",
    cache: true,
    externals: /^(jquery|handlebars|interact\W?js|\$)$/i
};
