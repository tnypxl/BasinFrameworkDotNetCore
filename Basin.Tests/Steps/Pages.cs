using System;
using Basin.Tests.Pages;

namespace Basin.Tests.Steps
{
    public static class Pages
    {
        [ThreadStatic] public static HomePage Home;

        public static void Init()
        {
            Home = new HomePage();
        }
    }
}
