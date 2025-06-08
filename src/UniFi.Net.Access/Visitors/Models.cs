using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFi.Net.Access.Visitors;

/// <summary>
/// Represents a summary of visitor details.
/// </summary>
public record VisitorSummary(string Id, string FirstName, string LastName, string Email);

/// <summary>
/// Represents detailed information about a visitor.
/// </summary>
public record VisitorDetails(string Id, string FirstName, string LastName, string Email, string Status);