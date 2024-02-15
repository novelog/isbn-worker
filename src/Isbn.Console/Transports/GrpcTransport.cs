using Grpc.Net.Client;

namespace Isbn.Console.Transports;

public class GrpcTransport
{
    public GrpcChannel Channel { get; }
    public GrpcTransport(GrpcChannelOptions opts)
    {
        Channel = GrpcChannel.ForAddress("http://localhost:5096", opts);
    }
}
