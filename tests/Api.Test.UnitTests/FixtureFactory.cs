using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Api.Test.UnitTests
{
    public static class FixtureFactory
    {
        private static Lazy<IFixture> _instance = new Lazy<IFixture>(CreateInstance);
        public static IFixture Instance => _instance.Value;

        public static IFixture CreateInstance()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());
            
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(3));

            return fixture;
        }
    }
}