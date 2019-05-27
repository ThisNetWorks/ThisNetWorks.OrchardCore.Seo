using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;

namespace ThisNetWorks.OrchardCore.Seo.Robots
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageRobots = new Permission("ManageRobots", "Manage robots.txt");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { ManageRobots };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageRobots }
                }
            };
        }
    }
}