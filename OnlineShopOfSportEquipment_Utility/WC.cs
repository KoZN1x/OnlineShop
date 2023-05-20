using System.Collections.ObjectModel;

namespace OnlineShopOfSportEquipment_Utility
{
    public static class WC
    {
        public const string ImagePath = @"\images\product\";
        public const string SessionCart = "ShoppingCartSession";

        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";

        public const string CategoryName = "Category";
        public const string TrainingTypeName = "TrainingType";

        public const string StatusActive = "Active";
        public const string StatusCompleted = "Completed";
        public const string StatusProcessing = "Processing";

        public static readonly IEnumerable<string> StatusList = new ReadOnlyCollection<string>(new List<string>
        {
            StatusActive, StatusCompleted, StatusProcessing
        });
    }
}
