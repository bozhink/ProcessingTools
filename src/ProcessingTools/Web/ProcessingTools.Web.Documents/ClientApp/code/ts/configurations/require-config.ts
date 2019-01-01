export function RequireConfig (require: any, pathToNodeModules: string): any {
    return require.config({
        paths: {
            "vs": `${pathToNodeModules}/monaco-editor/min/vs`
        }
    });
}
