using MODEL;
using MODEL.Entitied;
using REPOSITORY.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repositories
{
    internal class FileRepository : GenericRepository<Files>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context) { }
    }
}
