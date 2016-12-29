module.exports = function (require, pathToNodeModules) {
    return require.config({
        paths: {
            'vs': pathToNodeModules + '/monaco-editor/min/vs'
        }
    });
}