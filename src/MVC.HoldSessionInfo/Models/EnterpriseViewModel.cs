namespace MVC.HoldSessionInfo.Models;

public class EnterpriseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public IEnumerable<CertificationViewModel> Certifications { get; set; } = Array.Empty<CertificationViewModel>();
}
