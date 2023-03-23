using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC.HoldSessionInfo.Models
{
    public class CertificationViewModel
    {
        public int Index { get; set; }

        [Required(ErrorMessage = "Ceritification type is required")]
        public CertificationType CertificationType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EnterpriseId { get; set; }
        public IEnumerable<SelectListItem> CertificationTypes { get; set; }
    }
}