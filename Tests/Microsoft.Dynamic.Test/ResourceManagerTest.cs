using System.Globalization;
using System.Threading;
using Microsoft.Scripting;
using NUnit.Framework;

namespace Microsoft.Dynamic.Test
{
    [TestFixture]
    public class ResourceManagerTest {
        [Test]
        public void Test() {
            ResourceManager.Default.Initialize("IronPython");
            ResourceManager.Default.GetResource("NotNull","this field is required!!");
            ResourceManager.Default.GetResource("Overflow", "Overflow!!");
            ResourceManager.Default.SaveAs(CultureInfo.CurrentCulture);
        }
    }
}
