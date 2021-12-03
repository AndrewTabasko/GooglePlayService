using System.Threading.Tasks;
using GoogleApps.Interfaces;

namespace GoogleApps.Backend.Grpc
{
    public class GrpcClient
    {
        private Greeter.GreeterClient client;
        public GrpcClient(Greeter.GreeterClient client)
        {
            this.client = client;
        }


        public async Task SendGuid(HelloRequest message)
        {
            using (var call = client.LoadAppMetadataAsync(message))
            {
                await call.ResponseAsync;
            }
        }
    }
}
