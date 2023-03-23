using MVC.HoldSessionInfo.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MVC.HoldSessionInfo.Extensions
{
    public static class CertificationTypeExtensions
    {
        public static string GetDisplayName(this CertificationType certificationType)
        {
            return certificationType.GetType()
                .GetMember(certificationType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
    }
}
