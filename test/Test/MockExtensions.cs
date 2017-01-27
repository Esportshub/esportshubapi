using System.Collections.Generic;
using Moq;

namespace Test
{
    public static class MockExtensions
    {
        public static void ResetAll(List<Mock> list)
        {
            foreach (var mock in list)
            {
                mock.Reset();
            }
        }
    }
}