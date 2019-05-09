using Swagger.Lib.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swagger.Lib.Abstractions
{
    public interface ISwaggerProjectRepository
    {
        Task<List<ProjectModel>> GetProject();
    }
}
