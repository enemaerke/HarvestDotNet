using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class ClientsApi : HarvestApi
  {
    public ClientsApi(HarvestApiSettings settings)
          : base(settings)
    {
    }

    public Task<ClientInfo> GetClientById(long clientID)
    {
        return Request<ClientInfo>("/clients/{0}".ToFormat(clientID));
    }
    public Task<List<ClientInfo>> GetClients(ClientFilter filter = null)
    {
        return Request<List<ClientInfo>>("/clients" + (filter == null ? "" : filter.GenerateQueryString()));
    }
  }

  public class ClientFilter
  {
    public DateTime? UpdatedSince { get; set; }

    internal string GenerateQueryString()
    {
        return FilterHelper.ComposeQueryString<ClientFilter>(
        this,
        new[]{
          new FilterHelper.QueryPart<ClientFilter>("updated_since", UpdatedSince.HasValue, (f) => f.UpdatedSince.Value.ToString()), 
       });
    }
  }
}