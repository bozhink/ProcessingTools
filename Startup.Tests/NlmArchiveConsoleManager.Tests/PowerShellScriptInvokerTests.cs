namespace ProcessingTools.NlmArchiveConsoleManager.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PowerShellScriptInvokerTests
    {
        [TestMethod]
        public void PowerShellScriptInvoker_Invoke_ShouldGenerateCorrectOutput()
        {
            var invoker = new PowerShellScriptInvoker();
            invoker.Invoke();
        }
    }
}
