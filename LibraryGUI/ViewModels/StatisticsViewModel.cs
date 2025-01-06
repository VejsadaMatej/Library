namespace LibraryGUI.ViewModels
{
    public class StatisticsViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalBorrows { get; set; }
        public List<CategoryStatistic> CategoryCounts { get; set; }
    }

    public class CategoryStatistic
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }
}
