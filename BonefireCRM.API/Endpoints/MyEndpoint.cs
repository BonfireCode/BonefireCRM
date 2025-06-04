using BonefireCRM.API.Contrat;
using FastEndpoints;

namespace BonefireCRM.API.Endpoints
{
    public class MyEndpoint : Endpoint<Request, Response>
    {
        public override void Configure()
        {
            Post("/hello/world");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            await SendAsync(new ()
            {
                FullName = $"{r.FirstName} {r.LastName}",
                Message = "Welcome to FastEndpoints...",
            });
        }
    }
}
