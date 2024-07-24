namespace fitness_progress_service.Dto.Req
{
    public class ReqUserWorkoutDto
    {
        public int UserWorkoutID { get; set; }
        public int UserID { get; set; }
        public int WorkoutID { get; set; }
        public int WorkoutDuration { get; set; } = 0;
        public DateTime UserWorkoutDate { get; set; } = DateTime.UtcNow;
    }
}
