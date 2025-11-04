using System.Collections.Generic;
public class dialogs
{
    public string actor { get; set; }
    public List<dialog> dialog { get; set; }
}

    public class dialog
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public object lod_to { get; set; }
    }

    public class sub
    {
        public string lang { get; set; }
        public Structure structure { get; set; }
    }

    public class Structure
    {
        public List<dialogs> dialogs { get; set; }
    }