using System.Threading.Tasks;

namespace GoogleApps.Backned.Grpc
{
    public class GrpcClient
    {
        private AppDetails.AppDetailsClient client;
        public GrpcClient(AppDetails.AppDetailsClient client)
        {
            this.client = client;
        }


        public async Task SendGuid(AppGuid message)
        {
            using (var call = client.LoadAppDataAsync(message))
            {
                await call.ResponseAsync;
            }
        }
    }
}
