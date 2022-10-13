using System.ComponentModel;

namespace AioCore.Web.Pages.MachineComponents.ViewModels;

public class MachineDetailModel
{
    [DisplayName("Tên thuê bao")]
    public string Name { get; set; } = default!;

    [DisplayName("Mã thiết bị")]
    public Guid Code { get; set; } = Guid.NewGuid();

}