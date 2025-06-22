using UniFi.Net.Network.Models;

namespace UniFi.Net.Network.Responses;
internal record GenerateVouchersResponse(IEnumerable<Voucher> Vouchers);

