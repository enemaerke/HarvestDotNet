using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class AccountApi : HarvestApi
  {
    public AccountApi(HarvestApiSettings settings) : base(settings)
    {
    }

    public Task<AccountRateStatus> GetAccountRateStatus()
    {
      return Request<AccountRateStatus>("/account/rate_limit_status");
    }

    public Task<AccountRateStatus> WhoAmI()
    {
      return Request<AccountRateStatus>("/account/who_am_i");
    }
  }
}