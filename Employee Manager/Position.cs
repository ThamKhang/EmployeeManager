namespace Employee_Manager
{
    public class Position
    {
        // Properties
        public string Title { get; set; }
        public double Allowance { get; set; }

        // Constructor
        public Position(string title, double allowance)
        {
            Title = title;
            Allowance = allowance;
        }

        // Có thể thêm các phương thức hoặc logic bổ sung ở đây nếu cần
    }
}
