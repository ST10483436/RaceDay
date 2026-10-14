namespace RaceDay.Api.Models
{// start of the namespace
    public class Category
    {// start of category class
        public int CategoryId { get; set; }
        public int EventId { get; set; }
        public string CategoryName { get; set; } = "";
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }


    }//end of category class

}//end of the namespace
