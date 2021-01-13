using System;
using System.Collections.Generic;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.Builders
{
    public interface ICompositeActivityBuilder : IActivityBuilder
    {
        IServiceProvider ServiceProvider { get; }
        IReadOnlyCollection<IActivityBuilder> Activities { get; }
        
        IActivityBuilder New<T>(
            Action<IActivityBuilder>? branch = default,
            IDictionary<string, IActivityPropertyValueProvider>? propertyValueProviders = default)
            where T : class, IActivity;

        IActivityBuilder New<T>(
            Action<ISetupActivity<T>>? setup,
            Action<IActivityBuilder>? branch = default) where T : class, IActivity;

        IActivityBuilder StartWith<T>(
            Action<ISetupActivity<T>>? setup,
            Action<IActivityBuilder>? branch = default) where T : class, IActivity;

        IActivityBuilder StartWith<T>(Action<IActivityBuilder>? branch = default)
            where T : class, IActivity;

        IActivityBuilder Add<T>(
            Action<ISetupActivity<T>>? setup,
            Action<IActivityBuilder>? branch = default) where T : class, IActivity;

        IActivityBuilder Add<T>(
            Action<IActivityBuilder>? branch = default,
            IDictionary<string, IActivityPropertyValueProvider>? propertyValueProviders = default)
            where T : class, IActivity;

        IConnectionBuilder Connect(
            IActivityBuilder source,
            IActivityBuilder target,
            string outcome = OutcomeNames.Done);
        
        IConnectionBuilder Connect(
            Func<IActivityBuilder> source,
            Func<IActivityBuilder> target,
            string outcome = OutcomeNames.Done);

        ICompositeActivityBlueprint Build(string activityIdPrefix = "activity");
    }
}