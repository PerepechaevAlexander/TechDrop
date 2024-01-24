using System.Net.Http.Headers;
using System.Text;
using MediatR;
using TechDrop.Logic.Queries;

namespace TechDrop.WebApi.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMediator _mediator;

    public AuthMiddleware(RequestDelegate next, IMediator mediator)
    {
        _next = next;
        _mediator = mediator;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var header))
        {
            var authHeader = AuthenticationHeaderValue.Parse(header);
            if (authHeader.Parameter != null)
            {
                var credBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credBytes).Split(':',2);
                var email = credentials[0];
                var password = credentials[1];

                var user = await _mediator.Send(new CheckUserQuery(email, password));
                
                if (user != null)
                {
                    context.Items["UserId"] = user.UserId;
                }
            }
        }
        
        await _next(context);
    }
}