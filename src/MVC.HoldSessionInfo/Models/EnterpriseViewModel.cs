namespace MVC.HoldSessionInfo.Models;

public class EnterpriseViewModel
{
    public string Name { get; set; }
    public string City { get; set; }
    public IEnumerable<CertificationViewModel> Certifications { get; set; }
}
