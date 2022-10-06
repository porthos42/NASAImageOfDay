namespace BlastOff.Shared
{
    public class APODImage
    {
        public string Title { get; set; }
        public string Copyright { get; set; }
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
        public string Url { get; set; }
        public string HdUrl { get; set; }
        public bool IsNew => Date > DateTime.Today.AddDays(-3);
        public string PrettyDate => Date.ToString("MMMM dd, yyyy");

    }
}