﻿using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace Driver.Application.Unit.Tests
{
    [ExcludeFromCodeCoverage]
    public class AutoFixtureBase
    {
        protected Fixture Fixture { get; }
        protected List<string> Logs { get; private set; } = null!;

        public AutoFixtureBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization()
            {
                ConfigureMembers = true
            });
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
