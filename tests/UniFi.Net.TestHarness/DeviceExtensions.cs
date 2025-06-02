using System.Text;
using UniFi.Net.Network.Models;
using static System.Console;

namespace UniFi.Net.TestHarness;
public static class DeviceExtensions
{
    public static void PrettyPrint(this DeviceSummary device)
    {
        if (device == null) return;

        WriteLine("Device Summary");
        WriteLine("--------------");
        WriteLine($"Id:                  {device.Id}");
        WriteLine($"Name:                {device.Name}");
        WriteLine($"Model:               {device.Model}");
        WriteLine($"MAC Address:         {device.MacAddress}");
        WriteLine($"IP Address:          {device.IpAddress}");
        WriteLine($"State:               {device.State}");
        WriteLine($"Features:            {device.Features.ToPrettyString()}");
        WriteLine($"Interfaces:          {device.Interfaces.ToPrettyString()}");
        WriteLine();
    }

    public static void PrettyPrint(this Device device)
    {
        if (device == null) return;

        WriteLine("Device Details");
        WriteLine("--------------");
        WriteLine($"Id:                  {device.Id}");
        WriteLine($"Name:                {device.Name}");
        WriteLine($"Model:               {device.Model}");
        WriteLine($"Supported:           {device.Supported}");
        WriteLine($"MAC Address:         {device.MacAddress}");
        WriteLine($"IP Address:          {device.IpAddress}");
        WriteLine($"State:               {device.State}");
        WriteLine($"Firmware Version:    {device.FirmwareVersion}");
        WriteLine($"Firmware Updatable:  {device.FirmwareUpdatable}");
        WriteLine($"Adopted At:          {device.AdoptedAt:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"Provisioned At:      {device.ProvisionedAt:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"Configuration Id:    {device.ConfigurationId}");
        WriteLine($"Uplink Device Id:    {device.Uplink?.DeviceId}");
        WriteLine("Features:");
        WriteLine($"  Switching:         {device.Features?.Switching ?? "N/A"}");
        WriteLine($"  Access Point:      {device.Features?.AccessPoint ?? "N/A"}");
        WriteLine("Interfaces:");
        if (device.Interfaces?.Ports is { Count: > 0 })
        {
            WriteLine("  Ports:");
            foreach (var port in device.Interfaces.Ports)
            {
                WriteLine($"    - Idx:           {port.Idx}");
                WriteLine($"      State:         {port.State}");
                WriteLine($"      Connector:     {port.Connector}");
                WriteLine($"      Max Speed:     {port.MaxSpeedMbps} Mbps");
                WriteLine($"      Speed:         {port.SpeedMbps} Mbps");
                if (port.PoE is not null)
                {
                    WriteLine($"      PoE:");
                    WriteLine($"        Standard:    {port.PoE.Standard}");
                    WriteLine($"        Type:        {port.PoE.Type}");
                    WriteLine($"        Enabled:     {port.PoE.Enabled}");
                    WriteLine($"        State:       {port.PoE.State}");
                }
            }
        }
        if (device.Interfaces?.Radios is { Count: > 0 })
        {
            WriteLine("  Radios:");
            foreach (var radio in device.Interfaces.Radios)
            {
                WriteLine($"    - WLAN Standard: {radio.WlanStandard}");
                WriteLine($"      Frequency:     {radio.FrequencyGHz} GHz");
                WriteLine($"      Channel Width: {radio.ChannelWidthMHz} MHz");
                WriteLine($"      Channel:       {radio.Channel}");
            }
        }
        WriteLine();
    }

    public static string ToPrettyString(this IList<string> list)
    {
        StringBuilder stringBuilder = new();

        foreach (var item in list)
        {
            stringBuilder.AppendLine($"- {item}");
        }

        return stringBuilder.ToString().TrimEnd();
    }
}
