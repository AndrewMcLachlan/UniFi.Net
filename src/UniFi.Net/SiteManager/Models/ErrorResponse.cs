using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFi.Net.SiteManager.Models;
public record ErrorResponse : Response
{
    public required string Code { get; init; }

    public required string Message { get; init; }
}
