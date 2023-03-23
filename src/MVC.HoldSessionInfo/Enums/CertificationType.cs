using System.ComponentModel.DataAnnotations;

namespace MVC.HoldSessionInfo.Models
{
    public enum CertificationType
    {
        [Display(Name = "Type A")]
        TypeA,
        [Display(Name = "Type B")]
        TypeB,
        [Display(Name = "Type C")]
        TypeC,
    }
}