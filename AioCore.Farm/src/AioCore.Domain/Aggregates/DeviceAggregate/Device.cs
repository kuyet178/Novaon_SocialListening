using AioCore.Types;

namespace AioCore.Domain.Aggregates.DeviceAggregate;

public class Device : DeviceCore
{
    public string DisplayName { get; set; } = default!;

    public void Update(string displayName)
    {
        DisplayName = string.IsNullOrEmpty(displayName) ? DisplayName : displayName;
    }

    public void Update(string ipAddress, string gateway, string netmask, 
        string dns1, string dns2, string server, int leaseDuration, string board, 
        string device, string model, string brand, string bootloader, string display, 
        string fingerPrint, string hardware, string host, string manufacturer, 
        string product, DateTime now, string type, string user, string buildNumber)
    {
        IPAddress = string.IsNullOrEmpty(ipAddress) ? IPAddress : ipAddress;
        Gateway = string.IsNullOrEmpty(gateway) ? Gateway : gateway;
        Netmask = string.IsNullOrEmpty(netmask) ? Netmask : netmask;
        Dns1 = string.IsNullOrEmpty(dns1) ? Dns1 : dns1;
        Dns2 = string.IsNullOrEmpty(dns2) ? Dns2 : dns2;
        Server = string.IsNullOrEmpty(server) ? Server : server;
        LeaseDuration = leaseDuration;
        Board = string.IsNullOrEmpty(board) ? Board : board;
        Device = string.IsNullOrEmpty(device) ? Device : device;
        Model = string.IsNullOrEmpty(model) ? Model : model;
        Brand = string.IsNullOrEmpty(brand) ? Brand : brand;
        Bootloader = string.IsNullOrEmpty(bootloader) ? Bootloader : bootloader;
        Display = string.IsNullOrEmpty(display) ? Display : display;
        FingerPrint = string.IsNullOrEmpty(fingerPrint) ? FingerPrint : fingerPrint;
        Hardware = string.IsNullOrEmpty(hardware) ? Hardware : hardware;
        Host = string.IsNullOrEmpty(host) ? Host : host;
        Manufacturer = string.IsNullOrEmpty(manufacturer) ? Manufacturer : manufacturer;
        Product = string.IsNullOrEmpty(product) ? Product : product;
        Timestamp = now;
        Type = string.IsNullOrEmpty(type) ? Type : type;
        User = string.IsNullOrEmpty(user) ? User : user;
        BuildNumber = string.IsNullOrEmpty(buildNumber) ? BuildNumber : buildNumber;
    }
}