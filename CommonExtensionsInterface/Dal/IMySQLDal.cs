using CommonExtensionsInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonExtensionsInterface.Dal
{
    interface IMySQLDal
    {
        List<ConnectionStringModel> GetConnections();
        ConnectionStringModel GetConnectionById(int id);
        List<Endpoint> GetEndpoints();
        Endpoint GetEndpointById(int id);

    }
}
