namespace fitness_progress_service.Dto.Req
{
    public class ReqUserNutritionDto
    {
        public int UserNutritionID { get; set; }
        public int UserID { get; set; }
        public int NutritionID { get; set; }
        public int Qty { get; set; } = 0;
        public DateTime UserNutritionDate { get; set; } = DateTime.UtcNow;
    }
}
