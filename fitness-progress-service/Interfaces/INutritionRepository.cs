using fitness_db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_progress_service.Interfaces
{
    public interface INutritionRepository
    {
        public bool CreateNutrition(Nutrition Nutrition);
        public Nutrition UpdateNutrition(Nutrition Nutrition);
        public bool DeleteNutrition(Nutrition Nutrition);
        public ICollection<Nutrition> GetNutritions();
        public Nutrition GetNutrition(int nutritionId);
    }
}
