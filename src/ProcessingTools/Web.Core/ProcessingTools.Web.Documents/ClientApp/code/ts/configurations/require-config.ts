export function RequireConfig (require: Require, pathToNodeModules: string): Require {
    return require.config({
        paths: {
            "vs": `${pathToNodeModules}/monaco-editor/min/vs`
        }
    });
}
