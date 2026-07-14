using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PersonalFinanceTracker.Application.Abstractions.Authentication;

namespace PersonalFinanceTracker.Infrastructure.Authentication;

public sealed class UserIdentifierProvider : IUserIdentifierProvider
{
    public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("user_id")
                             ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

        UserId = new Guid(userIdClaim);
    }

    /// <inheritdoc />
    public Guid UserId { get; }
}