using System.Threading.Tasks;
using System.Collections.Generic;

namespace UniFi.Net.Access // Updated namespace to reflect new folder structure
{
    /// <summary>
    /// Interface for interacting with the UniFi Access API.
    /// </summary>
    public interface IUniFiAccessClient
    {

    }











    /// <summary>
    /// Represents an invitation to UniFi Identity.
    /// </summary>
    public record IdentityInvitation(string UserId, string? Email);

    /// <summary>
    /// Represents a UniFi Identity resource.
    /// </summary>
    public record IdentityResource(string Id, string Name, string ResourceType, bool Deleted);
}