using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;

namespace HarvestDotNet.TestApp
{
  public class Bootstrapper : BootstrapperBase
  {
    private CompositionContainer m_container;

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

      var instance = exports.FirstOrDefault();
      if (instance == null)
      {
          throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
      }

      return instance;

    }
  }
}