using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Windows;
using Caliburn.Micro;

namespace HarvestDotNet.TestApp
{
  public class Bootstrapper : Bootstrapper<ShellViewModel>
  {
    private CompositionContainer m_container;
    public Bootstrapper()
    {
    }

    protected override void Configure()
    {
      m_container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

      CompositionBatch batch = new CompositionBatch();

      batch.AddExportedValue<IWindowManager>(new WindowManager());
      batch.AddExportedValue<IEventAggregator>(new EventAggregator());
      batch.AddExportedValue(m_container);

      m_container.Compose(batch);
    }

    protected override object GetInstance(System.Type service, string key)
    {
      string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
      var exports = m_container.GetExportedValues<object>(contract);

      if (exports.Count() > 0)
      {
        return exports.First();
      }

      throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));

    }
  }
}