using DbContext.Lib;
using Swagger.Lib.Abstractions;
using Swagger.Lib.Entity;
using Swagger.Lib.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swagger.Lib.Implemention
{
    public class MySqlSwaggerProjectRepository : ISwaggerProjectRepository
    {
        private readonly IDbRepository<ProjectInfo> _dbRepository;

        public MySqlSwaggerProjectRepository(IDbRepository<ProjectInfo> dbRepository)
        {
            _dbRepository = dbRepository;
        }


        public async Task<List<ProjectModel>> GetProject()
        {

            List<ProjectModel> pro_list = null;

            var list = await _dbRepository.GetListAsync();

            if (list != null && list.Count > 0)
            {
                pro_list = new List<ProjectModel>();
                list.ForEach(x =>
                {
                    pro_list.Add(new ProjectModel()
                    {
                        Name = x.Name,
                        RouteKey = x.RouteKey
                    });
                });
            }
            return pro_list;
        }

    }
}
