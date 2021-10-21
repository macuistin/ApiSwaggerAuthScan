using System.Threading.Tasks;

namespace ApiSwaggerAuth.Console
{
    public interface ISwaggerScan
    {
        Task ScanAndOutputResponse(string url);
    }
}
