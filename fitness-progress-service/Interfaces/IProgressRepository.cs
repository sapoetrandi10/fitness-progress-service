using fitness_db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_progress_service.Interfaces
{
    public interface IProgressRepository
    {
        public bool CreateProgress(Progress progress);
        public Progress UpdateProgress(Progress Progress);
        public bool DeleteProgress(Progress Progress);
        public ICollection<Progress> GetProgresses();
        public Progress GetProgress(int progressId);
    }
}
